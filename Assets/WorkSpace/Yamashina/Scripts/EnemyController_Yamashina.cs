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

    [SerializeField, Header("�W�����v��")]
    private float enemyJumpPower;

    private void Start()
    {
        rigidBody2D_Enemy = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {


        EnemyAction();





    }


    private void MoveTowards(Vector3 target, float moveSpeed)
    {

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor") && !collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        }
    }




    private void Jump()
    {
        Vector2 upVector = Vector2.up;

        rigidBody2D_Enemy.velocity = upVector;
        rigidBody2D_Enemy.AddForce(transform.up * enemyJumpPower, ForceMode2D.Force);

    }




    private void EnemyAction()
    {
        //Jump();

        Vector3 vPosition = transform.position;

        vPosition.x += Time.deltaTime * normalSpeed;

        transform.position = vPosition;

    }
}




