using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TItleManager_Yamashina : MonoBehaviour
{
    [Header("�p�l���̃I�u�W�F�N�g�̃Z�b�g�A�N�e�B�u�؂�ւ��p")]
    [Tooltip("�^�C�g����ʂ̃I�u�W�F�N�g������")]

    public GameObject mainPanel;

    [Tooltip("�N���W�b�g��ʂ̃I�u�W�F�N�g������")]
    public GameObject DescriptionPanel;

    [Tooltip("�I�v�V������ʂ̃I�u�W�F�N�g������")]
    public GameObject OptionPanel;
    [SerializeField, Header("�Q�[���X�^�[�g�̃{�^����ݒ�")] private Button StartButton;
    [Tooltip("�͂��߂���{�^���̃C�x���g�g���K�[������")]
    private EventTrigger eventTrigger_Start;
    // Start is called before the first frame update
    void Start()
    {
        eventTrigger_Start=StartButton.GetComponent<EventTrigger>();
        StartButton.onClick.AddListener(() =>
        {
            SceneTransitionManager_Yamashina.instance.NextSceneButton(1);
        });
    }

    private void Update()
    {
        
    }


}
