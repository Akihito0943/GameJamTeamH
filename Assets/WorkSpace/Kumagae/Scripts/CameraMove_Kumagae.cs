using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_Kumagae : MonoBehaviour
{

    // �v���[���[�I�u�W�F�N�g�̃g�����X�t�H�[���R���|�[�l���g
    [Header("�v���[���[�g�����X�t�H�[��")]
    [SerializeField] Transform trPlayer;

    // ��ʂ̍��[����ǂꂭ�炢�v���[���[�𗣂���
    [Header("��ʂ̍��[����̃I�t�Z�b�g")]
    [SerializeField] float offset;

    // ��ʂ̍��[
    private Vector3 screenLeftEdge;

    // �v���[���[�̃X�v���C�g�����_���[
    SpriteRenderer srPlayer;

    void Start()
    {
        // ��ʂ̍��[�̍��W���擾
        screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));

        // �v���[���[�̃X�v���C�g�����_���[�R���|�[�l���g���擾
        srPlayer = trPlayer.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (trPlayer == null) return;

        // �v���C���[�̕��i�����̃T�C�Y�j���擾
        float playerWidth = srPlayer.bounds.extents.x;

        // �V�����J�����̍��W��ݒ�
        float newCameraPosX = trPlayer.position.x - (screenLeftEdge.x + playerWidth + offset);

        // �J�����̍��W���X�V
        transform.position = new Vector3(newCameraPosX, transform.position.y, transform.position.z);
    }
}
