using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallOut_Kumagae : MonoBehaviour
{

    [SerializeField, Header("フェードインを行いシーンを切替えるキャンバス")]
    Canvas cvFadeIn;

    [SerializeField, Header("落下時の効果音")] AudioClip acFall;

    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;

    private bool isOneShot = false;



    private void Start()
    {
        audioSourceSE.volume = MultiAudio_Yamashina.ins.seSource.volume;
    }

    void Update()
    {
        // Y座標が-15以下になったらゲーム終了
        if(transform.position.y < -15)
        {
            Debug.Log("落ちた");

            if (!isOneShot)
            {
                audioSourceSE.PlayOneShot(acFall);
                isOneShot = true;
            }

            // シーンを切り替える
            cvFadeIn.gameObject.SetActive(true);
        }
    }
}
