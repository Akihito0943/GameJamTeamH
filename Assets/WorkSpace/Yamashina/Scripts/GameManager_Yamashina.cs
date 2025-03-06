using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Yamashina : MonoBehaviour
{

    public static GameManager_Yamashina instance;

    public enum EnemyState
    {
        None,       // 未設定
        Defeated,   // 弾に当たって倒された
        Escaped     // 逃げ切った
    }

    static EnemyState enemyState = EnemyState.None;
    static EnemyState previousGameState_Enemy;


    public static void ChangeState(EnemyState newState)
    {
        previousGameState_Enemy = enemyState; // 現在のステートを前回のステートとして保存



        enemyState = newState;
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
            DontDestroyOnLoad(gameObject); // シーンをまたいでデータを保持
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


