using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TItleManager_Yamashina : MonoBehaviour
{

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

    


}
