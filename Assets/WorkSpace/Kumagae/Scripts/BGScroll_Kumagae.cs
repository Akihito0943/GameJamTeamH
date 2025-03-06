using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll_Kumagae : MonoBehaviour
{
    // �w�i�X�N���[�����s���C���[�W�R���|�[�l���g
    [Header("�C���[�W")]
    [SerializeField] Image image;
    
    // 1�t���[���O�̃J�����̍��W
    private Vector3 lastCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        // �J�����̏������W���擾
        lastCameraPos = Camera.main.transform.position;

        // �J�������[�u�R���|�[�l���g����I�t�Z�b�g�l���擾���A�I�t�Z�b�g���̍����C��
        lastCameraPos.x -= Camera.main.GetComponent<CameraMove_Kumagae>().GetOffset();
    }

    // Update is called once per frame
    void Update()
    {
        // �J�����̈ړ����x���v�Z����
        Vector2 cameraMoveSpeed = Camera.main.transform.position - lastCameraPos;

        // �摜�̍��W���X�V����
        image.transform.position -= new Vector3(cameraMoveSpeed.x, cameraMoveSpeed.y, 0);

        // ���W���X�V����
        lastCameraPos = Camera.main.transform.position;
    }
}
