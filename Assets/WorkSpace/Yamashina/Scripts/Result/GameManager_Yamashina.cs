using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Yamashina : MonoBehaviour
{

    public static GameManager_Yamashina instance;

    public enum EnemyState
    {
        None,       // ���ݒ�
        Defeated,   // �e�ɓ������ē|���ꂽ
        Escaped     // �����؂���
    }

   
    static EnemyState enemyState = EnemyState.None;
    static EnemyState previousGameState_Enemy;
    [Header("�v���C���[���ւ����𓊂�����")]
    public static float allCount = 0;

    public static void ChangeState(EnemyState newState)
    {
        previousGameState_Enemy = enemyState; // ���݂̃X�e�[�g��O��̃X�e�[�g�Ƃ��ĕۑ�



        enemyState = newState;
        Debug.Log(newState);

    }
    public static EnemyState GetState()
    {
        return enemyState;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����Ńf�[�^��ێ�
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        allCount = 0;
    }
}


