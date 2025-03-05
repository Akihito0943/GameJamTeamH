using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Kumagae : MonoBehaviour
{

    [Header("縦方向の速度")]
    [SerializeField] float verticalSpeed = 10f;  // 縦方向のスピード
    [Header("横方向の速度")]
    [SerializeField] float horizontalSpeed = 5f; // 横方向の速度
    [Header("弾の寿命")]
    [SerializeField] float lifeTime = 5f;     // 弾の寿命

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 初速を設定（斜め上方向に発射）
        Vector2 velocity = new Vector2(horizontalSpeed, verticalSpeed);
        rb.velocity = velocity;

        // 指定時間後にオブジェクトを破壊
        Destroy(gameObject, lifeTime);
    }

}
