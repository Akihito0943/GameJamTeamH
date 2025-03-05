using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController_Yamashina : MonoBehaviour
{
    [Tooltip("タイルマップの参照")]
    [SerializeField] private Tilemap tilemap;  // タイルマップの参照
    [SerializeField] private TileBase holeTile; // 穴のタイル

    private Rigidbody2D rigidBody2D_Enemy;
    private bool jumpFlag = false;

    [SerializeField, Header("ジャンプ力")]
    private float enemyJumpPower;
    [SerializeField] private float normalSpeed = 2f;

 private Animator animator;
    private void Start()
    {
        rigidBody2D_Enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }

    protected virtual void Update()
    {
        // エネミーアクションの呼び出し
        EnemyAction();
    }

    private void EnemyAction()
    {
        // 移動処理
        Vector3 vPosition = transform.position;
        vPosition.x += Time.deltaTime * normalSpeed;
        transform.position = vPosition;


        // エネミーの位置をタイルマップの座標に変換
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position);

        // 現在位置のタイルを取得
        TileBase currentTile = tilemap.GetTile(tilePosition);

        // 現在位置が穴のタイルかどうか判定
        if (currentTile == holeTile && !jumpFlag)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!jumpFlag)
        {
            rigidBody2D_Enemy.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rigidBody2D_Enemy.AddForce(Vector2.up * enemyJumpPower, ForceMode2D.Impulse);
            jumpFlag = true;  // ジャンプ後はフラグを立ててジャンプを1回だけにする
            animator.SetBool("jumpFlag",true);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 地面に着地した場合
        {            jumpFlag = true;  // ジャンプ後はフラグを立ててジャンプを1回だけにする

            rigidBody2D_Enemy.constraints |= RigidbodyConstraints2D.FreezePositionY;
            animator.SetBool("jumpFlag", false);
            jumpFlag = false;  // ジャンプ後はフラグを立ててジャンプを1回だけにする

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // ���݈ʒu�����̃^�C�����ǂ�������
            if (!jumpFlag)

            {
                Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
                Jump(); 
            }
        }
    }
}
