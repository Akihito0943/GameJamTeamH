using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController_Yamashina : MonoBehaviour
{

    // 移動を始める場所、終わりの場所、普段の移動速度、追跡中の移動速度、敵の索敵可能な範囲を設定

    [Tooltip("普段の移動速度")]
    [SerializeField] private float normalSpeed = 2f;

    private Rigidbody2D rigidBody2D_Enemy;

    [SerializeField, Header("ジャンプ力")]
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




