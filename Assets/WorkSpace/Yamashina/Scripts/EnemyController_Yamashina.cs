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

    private void Start()
    {
        rigidBody2D_Enemy = GetComponent<Rigidbody2D>();
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 地面に着地した場合
        {
            rigidBody2D_Enemy.constraints |= RigidbodyConstraints2D.FreezePositionY;
            jumpFlag = false;
        }
    }
}
