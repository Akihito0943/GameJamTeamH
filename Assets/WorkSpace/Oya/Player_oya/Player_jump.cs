using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_jump : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    private bool isGround = true;
    [Header("ジャンプ力")]
    [SerializeField]float jumpPower = 5.0f;

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpUpdate();
    }
    private void JumpUpdate()
    {
        if (isGround&&Input.GetKeyDown(KeyCode.Space))
        {
            {// ジャンプ開始
             // ジャンプ力を計算
                // ジャンプ力を適用
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            animator.SetBool("isInair",true);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            animator.SetBool("isInair", false);

        }
    }

}
