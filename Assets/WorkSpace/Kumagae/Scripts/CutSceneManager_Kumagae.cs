using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager_Kumagae : MonoBehaviour
{

    [SerializeField, Header("カットシーンが終了したかどうか")]
    public static bool isCutSceneEnd = false;

    // アニメーターコンポーネント
    [SerializeField, Header("カットシーンのアニメーター")]
    Animator animator;

    [SerializeField, Header("プレーヤーのプレハブ")]
    GameObject player;

    [SerializeField, Header("敵のプレハブ")]
    GameObject enemy;

    [SerializeField, Header("ゲームオーバーのアニメションクリップ")]
    AnimationClip acGameOver;

    [SerializeField, Header("フェードインを行いシーンを切替えるキャンバス")]
    Canvas cvFadeIn;

    // Start is called before the first frame update
    void Start()
    {
        isCutSceneEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// カットシーンが終了したことを通知する
    /// </summary>
    public void SetCutSceneEnd()
    {
        isCutSceneEnd = true;
        animator.enabled = false;
    }

    /// <summary>
    /// プレイヤーと敵をアクティブにする
    /// </summary>
    public void PlayerAndEnemyActive()
    {
        player.SetActive(true);
        enemy.SetActive(true);
    }

    /// <summary>
    /// ゲームオーバー時のカットシーンを作成する
    /// </summary>
    public void StartCutSceneOfEnd()
    {
        // isCutSceneEnd = false;
        animator.enabled = true;
        animator.Play(acGameOver.name);
    }

    public void FadeInStart()
    {
        // シーンを切り替える
        cvFadeIn.gameObject.SetActive(true);
    }
}
