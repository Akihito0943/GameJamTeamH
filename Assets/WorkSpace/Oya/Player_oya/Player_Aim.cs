using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Aim : MonoBehaviour
{
    [SerializeField, Header("�o���b�g�̃v���n�u")]
    GameObject bullet;

    [SerializeField, Header("�o���b�g�̃g�����X�t�H�[��")]
    Transform bulletTransform;

    [SerializeField, Header("�v���C���[�̃A�j���[�^�[")]
    Animator animator;


    [SerializeField, Header("�N�[���^�C��")]
    float coolTime = 2;

    [SerializeField, Header("�N�[���^�C�������ǂ���")]
    private bool isCoolTime = false;

    [SerializeField, Header("���ʉ��p�̃I�[�f�B�I�\�[�X")] AudioSource audioSourceSE;
    [SerializeField, Header("���������̌��ʉ�")] AudioClip acThrow;


    float rotZ;

    private void Start()
    {
        audioSourceSE.volume = MultiAudio_Yamashina.ins.seSource.volume;

    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�̍��W���擾
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // �}�E�X�̕������v�Z��160�x�͈̔͂Ɉړ��𐧌�����
        Vector3 rotation = mousePos - transform.position;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotZ = Mathf.Clamp(rotZ, -80, 80);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // ���N���b�N�Œe�𓊂���
        if (Input.GetMouseButtonDown(0))
        {
            // �N�[���^�C�����łȂ���Βe�𔭎˂���
            if (!isCoolTime)
            {
                isCoolTime = true;
                StartCoroutine(ThrowAfterCoolTimer());
                // ���ʉ����Đ�
                audioSourceSE.PlayOneShot(acThrow);
            }
        }
    }


    /// <summary>
    /// �e�𓊂���
    /// </summary>
    private void Throw()
    {
        //�������񐔂��J�E���g
        GameManager_Yamashina.allCount++;
        MultiAudio_Yamashina.ins.PlaySEByName("SE_Throw");

        // �e�𐶐�����
        GameObject goBullet = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        // �e�̑��x�Ɣ��ˊp�x��ݒ�
        Rigidbody2D rbBullet = goBullet.GetComponent<Rigidbody2D>();
        Bullet_Kumagae bk = goBullet.GetComponent<Bullet_Kumagae>();
        Vector2 direction = new Vector2(Mathf.Cos(rotZ * Mathf.Deg2Rad), Mathf.Sin(rotZ * Mathf.Deg2Rad));
        rbBullet.velocity = direction * bk.GetSpeed();

        // ������A�j���[�V�������Đ�����
        animator.SetTrigger("Throw");
    }


    /// <summary>
    /// �N�[���^�C����ɒe�𓊂���
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThrowAfterCoolTimer()
    {
        Throw();

        yield return new WaitForSeconds(coolTime);

        isCoolTime = false;
    }
}
