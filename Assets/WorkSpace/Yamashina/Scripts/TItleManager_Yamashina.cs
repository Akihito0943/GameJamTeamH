using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TItleManager_Yamashina : MonoBehaviour
{

    [SerializeField, Header("�Q�[���X�^�[�g�̃{�^����ݒ�")] private Button StartButton;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            SceneTransitionManager_Yamashina.instance.NextSceneButton(1);
        });
    }

   
}
