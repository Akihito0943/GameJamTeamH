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

    private bool jumpFlag = false;

    [SerializeField, Header("ジャンプ力")]
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
        if (collision.gameObject.CompareTag("Ground"))  // 地面に着地した場合
        {
            Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

            // Y軸の位置制約を再度適用（着地後に位置を固定）
            rigidBody2D_Enemy.constraints |= RigidbodyConstraints2D.FreezePositionY;

            // ジャンプフラグをリセットして次回ジャンプ可能にする
            jumpFlag = false;
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
   

    private void EnemyAction()
    {


        Vector3 vPosition = transform.position;

        vPosition.x += Time.deltaTime * normalSpeed;

        transform.position = vPosition;

    }
}




