using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame_Yamashina : MonoBehaviour
{
    //�Q�[���I��:�{�^������Ăяo��
    public void EndGame()
    {


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        //Application.quitting += VolumeSave;


#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }

}