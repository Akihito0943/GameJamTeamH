using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController_Yamashina : MonoBehaviour
{

    // �ړ����n�߂�ꏊ�A�I���̏ꏊ�A���i�̈ړ����x�A�ǐՒ��̈ړ����x�A�G�̍��G�\�Ȕ͈͂�ݒ�

    [Tooltip("���i�̈ړ����x")]
    [SerializeField] private float normalSpeed = 2f;

    private Rigidbody2D rigidBody2D_Enemy;

    private bool jumpFlag = false;

    [SerializeField, Header("�W�����v��")]
    private float enemyJumpPower;

    private void Start()
    {
        rigidBody2D_Enemy = GetComponent<Rigidbody2D>();

    }

    protected virtual void Update()
    {

        Debug.Log($"rigidBody2D_Enemy.gravityScale ; {rigidBody2D_Enemy.gravityScale}");
        EnemyAction();

      

      

    }


    private void MoveTowards(Vector3 target, float moveSpeed)
    {

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }


    


private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") )
        {
            Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
            Jump();

        }
        if (collision.gameObject.CompareTag("Ground"))  // �n�ʂɒ��n�����ꍇ
        {
            Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

            // Y���̈ʒu������ēx�K�p�i���n��Ɉʒu���Œ�j
            rigidBody2D_Enemy.constraints |= RigidbodyConstraints2D.FreezePositionY;

            // �W�����v�t���O�����Z�b�g���Ď���W�����v�\�ɂ���
            jumpFlag = false;
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
   

    private void EnemyAction()
    {


        Vector3 vPosition = transform.position;

        vPosition.x += Time.deltaTime * normalSpeed;

        transform.position = vPosition;

    }
}




