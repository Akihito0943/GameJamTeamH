using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Kumagae : MonoBehaviour
{

    [Header("縦方向の速度")]
    [SerializeField] Vector2 speed;  // 弾の速度
    
    [Header("弾の寿命")]
    [SerializeField] float lifeTime = 5f;        // 弾の寿命

    void Start()
    {    
        // 指定時間後にオブジェクトを破壊
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 地面に接触したら弾を削除する
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
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
