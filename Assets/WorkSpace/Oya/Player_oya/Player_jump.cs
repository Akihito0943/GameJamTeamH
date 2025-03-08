using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_jump : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] public bool isGround = true;
    [Header("�W�����v��")]
    [SerializeField] float jumpPower = 5.0f;

    [SerializeField] Animator animator;

    [SerializeField, Header("�W�����v�̌��ʉ�")] AudioClip acJump;

    [SerializeField, Header("���ʉ��p�̃I�[�f�B�I�\�[�X")] AudioSource audioSourceSE;

    [SerializeField, Header("����p�̃I�[�f�B�I�\�[�X")] AudioSource audioSourceRun;

    [SerializeField, Header("�y���G�t�F�N�g�I�u�W�F�N�g")] GameObject goSmoke;

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
            // �W�����v�͂�K�p
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            audioSourceSE.PlayOneShot(acJump);
            // �y���G�t�F�N�g���~�߂�
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
    //        // �y���G�t�F�N�g���o��
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
