using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_jump : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] public bool isGround = true;
    [Header("ジャンプ力")]
    [SerializeField] float jumpPower = 5.0f;

    [SerializeField] Animator animator;

    [SerializeField, Header("ジャンプの効果音")] AudioClip acJump;

    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;

    [SerializeField, Header("走る用のオーディオソース")] AudioSource audioSourceRun;

    [SerializeField, Header("土煙エフェクトオブジェクト")] GameObject goSmoke;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSourceRun.volume = MultiAudio_Yamashina.ins.seSource.volume;
        audioSourceSE.volume = MultiAudio_Yamashina.ins.seSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        JumpUpdate();
    }
    private void JumpUpdate()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            // ジャンプ力を適用
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            audioSourceSE.PlayOneShot(acJump);
            // 土煙エフェクトを止める
            goSmoke.SetActive(false);
        }
    }
    
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGround = true;
    //        animator.SetBool("isInair", true);
    //        audioSourceRun.Play();
    //    }

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        // 土煙エフェクトを出す
    //        goSmoke.SetActive(true);
    //    }

    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGround = false;
    //        animator.SetBool("isInair", false);
    //        audioSourceRun.Stop();
    //    }
    //}

}
