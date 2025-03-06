using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_auto_run_oya : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float speed = 1.0f;

    [SerializeField, Header("加速力")] float accelPower = 5;           // 加速力
    [SerializeField, Header("減速力")] float brakePower = 0.5f;        // 減速力
    [SerializeField, Header("加速継続時間")] float accelDuration = 2f; // 加速継続時間

    // 元の速度を保持する変数
    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 障害物に当たったら移動速度減少させる
        if (collision.gameObject.tag == "Obstacle")
        {
            ChangeSpeed(brakePower);
        }

        // アイテムに当たったら移動速度増加させる
        if (collision.gameObject.tag == "Item")
        {
            ChangeSpeed(accelPower);
        }
    }

    /// <summary>
    /// 移動速度を変更する
    /// </summary>
    /// <param name="accelMultiplier">加速度</param>
    private void ChangeSpeed(float accelMultiplier)
    {
        // コルーチンを呼び出す
        StartCoroutine(Accelerate(accelMultiplier));
    }

    /// <summary>
    /// 一定時間速度を変更するエミュレータ
    /// </summary>
    /// <param name="accelMultiplier"></param>
    /// <returns></returns>
    private IEnumerator Accelerate(float accelMultiplier)
    {
        // 加速度分スピードを調整する
        speed *= accelMultiplier;

        // 一定時間待つ
        yield return new WaitForSeconds(accelDuration);

        // 元の移動速度に戻す
        speed = originalSpeed;
    }
}
