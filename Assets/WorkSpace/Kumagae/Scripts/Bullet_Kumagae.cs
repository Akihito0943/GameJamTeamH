﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Kumagae : MonoBehaviour
{

    [Header("縦方向の速度")]
    [SerializeField] Vector2 speed;  // 弾の速度
    
    [Header("弾の寿命")]
    [SerializeField] float lifeTime = 5f;        // 弾の寿命

    [SerializeField, Header("アイテム取得時のエフェクト")]
    GameObject goFlashEffect;

    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;
    [SerializeField, Header("アイテムに当たった時の効果音")] AudioClip acItem;


    private Player_auto_run_oya player_Auto_Run_Oya;
    void Start()
    {    
        // 指定時間後にオブジェクトを破壊
        Destroy(gameObject, lifeTime);
        player_Auto_Run_Oya =FindAnyObjectByType<Player_auto_run_oya>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 地面に接触したら弾を削除する
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            player_Auto_Run_Oya.ChangeSpeed(player_Auto_Run_Oya.GetAccelPower());

            // アイテムに当たった時の効果音再生            
            audioSourceSE.PlayOneShot(acItem);

            // 取得時のエフェクトを出す
            var g = Instantiate(goFlashEffect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(g, 1.0f);
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// 弾の速度を取得する
    /// </summary>
    /// <returns>弾の速度</returns>
    public Vector2 GetSpeed()
    {
        return speed;
    }
}
