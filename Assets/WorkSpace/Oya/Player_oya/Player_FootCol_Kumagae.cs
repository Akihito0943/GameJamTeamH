using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FootCol_Kumagae : MonoBehaviour
{

    [SerializeField, Header("ジャンプの効果音")] AudioClip acJump;

    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;

    [SerializeField, Header("走る用のオーディオソース")] AudioSource audioSourceRun;

    [SerializeField, Header("土煙エフェクトオブジェクト")] GameObject goSmoke;

    [SerializeField] Animator animator;

    [SerializeField, Header("プレーヤーのプレハブ")]
    GameObject player;

    Player_jump pj;

    // Start is called before the first frame update
    void Start()
    {
        pj = player.GetComponent<Player_jump>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pj.isGround = true;
            animator.SetBool("isInair", true);
            audioSourceRun.Play();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 土煙エフェクトを出す
            goSmoke.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pj.isGround = false;
            animator.SetBool("isInair", false);
            audioSourceRun.Stop();
        }
    }

}
