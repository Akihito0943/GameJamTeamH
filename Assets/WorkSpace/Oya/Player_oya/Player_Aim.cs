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

    [SerializeField, Header("�ւ����𓊂�����")]
    static float allCount = 0;

    [SerializeField, Header("�N�[���^�C��")]
    float coolTime = 2;

    [SerializeField, Header("�N�[���^�C�������ǂ���")]
    private bool isCoolTime = false;


    // Update is called once per frame
    void Update()
    {
        // �}�E�X�̍��W���擾
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // �}�E�X�̕������v�Z��160�x�͈̔͂Ɉړ��𐧌�����
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
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
            }
        }
    }


    /// <summary>
    /// �e�𓊂���
    /// </summary>
    private void Throw()
    {
        //�������񐔂��J�E���g
        allCount++;
        Debug.Log(allCount);

        // �e�𐶐�����
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);

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
