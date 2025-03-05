using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController_Yamashina : MonoBehaviour
{
    [Tooltip("�^�C���}�b�v�̎Q��")]
    [SerializeField] private Tilemap tilemap;  // �^�C���}�b�v�̎Q��
    [SerializeField] private TileBase holeTile; // ���̃^�C��

    private Rigidbody2D rigidBody2D_Enemy;
    private bool jumpFlag = false;

    [SerializeField, Header("�W�����v��")]
    private float enemyJumpPower;
    [SerializeField] private float normalSpeed = 2f;

    private void Start()
    {
        rigidBody2D_Enemy = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        // �G�l�~�[�A�N�V�����̌Ăяo��
        EnemyAction();
    }

    private void EnemyAction()
    {
        // �ړ�����
        Vector3 vPosition = transform.position;
        vPosition.x += Time.deltaTime * normalSpeed;
        transform.position = vPosition;

        // �G�l�~�[�̈ʒu���^�C���}�b�v�̍��W�ɕϊ�
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position);

        // ���݈ʒu�̃^�C�����擾
        TileBase currentTile = tilemap.GetTile(tilePosition);

        // ���݈ʒu�����̃^�C�����ǂ�������
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
            jumpFlag = true;  // �W�����v��̓t���O�𗧂ĂăW�����v��1�񂾂��ɂ���
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // �n�ʂɒ��n�����ꍇ
        {
            rigidBody2D_Enemy.constraints |= RigidbodyConstraints2D.FreezePositionY;
            jumpFlag = false;
        }
    }
}
