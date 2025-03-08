using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController_Yamashina : MonoBehaviour
{
    [Tooltip("タイルマップの参照")]
    [SerializeField, Header("ヒエラルキー上のHole_Layerをドラッグアンドドロップ")] private Tilemap tilemap;  // タイルマップの参照
    [SerializeField] private TileBase holeTile; // 穴のタイル
    private Animator animator;
    private Rigidbody2D rigidBody2D_Enemy;
    private bool enemyActionStarted = false; // アクション開始フラグ
    [SerializeField] private float enemyRunningStartTime;


    [SerializeField, Header("ジャンプ力")]
    private float enemyJumpPower;
    private bool jumpFlag = false;

    [SerializeField, Header("走るスピード")]
    private float normalSpeed = 2f;
    private bool IsAttacked = false;

    [SerializeField, Header("走る用のオーディオソース")] AudioSource audioSourceRun;
    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;
    [SerializeField, Header("ジャンプの効果音")] AudioClip acJump;
    [SerializeField, Header("ヒットの効果音")] AudioClip acHIt;

    [SerializeField, Header("カットシーンのオブジェクト")]
    GameObject goCutScene;

    [SerializeField, Header("フェードインを行いシーンを切替えるキャンバス")]
    Canvas cvFadeIn;

    // 一時停止中のフラグ
    private bool isPaused = false;
    private void Start()
    {
        rigidBody2D_Enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.enabled = false;
        // エネミーアクションの呼び出し
        Invoke("StartEnemyAction", enemyRunningStartTime);
    }

    protected virtual void Update()
    {
        //if (!CutSceneManager_Kumagae.isCutSceneEnd) return;

        if (enemyActionStarted)
        {
            EnemyAction();
        }
    }
    // enemyRunningStartTime秒後に呼ばれるメソッド
    private void StartEnemyAction()
    {
        // アニメーションを再開し、敵の動作を開始
        animator.enabled = true;
        enemyActionStarted = true;

        // 足音をループ再生する
        audioSourceRun.Play();
        audioSourceRun.loop = true;
    }
    private void EnemyAction()
    {
        // 一時停止中は動かさない
        if (isPaused)
        {
            // 足音をループ再生する
            audioSourceRun.Stop();
            return;
        }

        // 移動処理
        Vector3 vPosition = transform.position;
        vPosition.x += Time.deltaTime * normalSpeed;
        transform.position = vPosition;


        // エネミーの位置をタイルマップの座標に変換
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position);
        tilePosition.y = tilePosition.y - 3;

        // 現在位置のタイルを取得
        TileBase currentTile = tilemap.GetTile(tilePosition);

        // 現在位置が穴のタイルかどうか判定
        if (currentTile == holeTile && !jumpFlag)
        {
            Jump();
        }
        if (IsAttacked && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animator.IsInTransition(0))
        {
            IsAttacked = false;
            animator.SetBool("IsAttacked", false);


        }
        // 敵の現在位置をビューポート座標に変換
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // viewPos.x が 0 未満または 1 を超えていたら、背景の左右の端に到達しているとみなせる
        if (viewPos.x < 0 || viewPos.x > 1)
        {
            GameManager_Yamashina.ChangeState(GameManager_Yamashina.EnemyState.Escaped);

            //SceneTransitionManager_Yamashina.instance.GoToNextScene(SceneTransitionManager_Yamashina.instance.sceneInformation.GetNextSceneInt());
        }
    }

    private void Jump()
    {
        if (!jumpFlag)
        {
            rigidBody2D_Enemy.AddForce(Vector2.up * enemyJumpPower, ForceMode2D.Impulse);
            jumpFlag = true;  // ジャンプ後はフラグを立ててジャンプを1回だけにする
            animator.SetBool("jumpFlag", true);
            // 足音を止める
            audioSourceRun.Stop();
            // ジャンプ音を再生
            audioSourceSE.PlayOneShot(acJump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 地面に着地した場合
        {
            jumpFlag = true;  // ジャンプ後はフラグを立ててジャンプを1回だけにする

            Debug.Log("着地した");
            animator.SetBool("jumpFlag", false);
            // 足音を再び再生する
            audioSourceRun.Play();
            audioSourceRun.loop = true;
            jumpFlag = false;  // ジャンプ後はフラグを立ててジャンプを1回だけにする
        }

        // エンドtagオブジェクトに当たると敵を非アクティブにし、カットシーン用の画像でアニメションを行う
        if (collision.gameObject.CompareTag("End"))
        {
            gameObject.SetActive(false);
            goCutScene.GetComponent<CutSceneManager_Kumagae>().StartCutSceneOfEnd();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 障害物に当たった時の処理
            if (!jumpFlag)
            {
                Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
                Jump();
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 輪っかが当たった時の処理
            if (!IsAttacked)
            {
                IsAttacked = true;

                animator.SetBool("IsAttacked", true);
                GameManager_Yamashina.ChangeState(GameManager_Yamashina.EnemyState.Defeated);

                isPaused = true;

                // ヒット音を再生
                audioSourceSE.PlayOneShot(acHIt);

                // シーンを切り替える
                cvFadeIn.gameObject.SetActive(true);
            }
        }
    }
}

