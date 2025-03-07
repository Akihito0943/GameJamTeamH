using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_auto_run_oya : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float speed = 1.0f;

    [SerializeField, Header("加速力")] float accelPower = 5;           // 加速力
    [SerializeField, Header("減速力")] float brakePower = 0.5f;        // 減速力
    [SerializeField, Header("加速継続時間")] float accelDuration = 0.5f; // 加速継続時間


    [SerializeField, Header("走る用のオーディオソース")] AudioSource audioSourceRun;

    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;
    [SerializeField, Header("障害物に当たった時の効果音")] AudioClip acObstacle;
    [SerializeField, Header("アイテムに当たった時の効果音")] AudioClip acItem;

    [SerializeField, Header("アイテム取得時のエフェクト")]
    GameObject goFlashEffect;


    // 煙エフェクトをコントロールする
    private SmokeVFX_Kumagae smoke;

    // 元の速度を保持する変数
    private float originalSpeed;

    // 元の土煙エフェクトのスケール
    private Vector3 originalSmokeScale;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;

        // コンポーネント取得
        smoke = GetComponent<SmokeVFX_Kumagae>();
        originalSmokeScale = new Vector3(0.6f,0.6f,0.6f);
        
        // 足音をループ再生する
        audioSourceRun.Play();
        audioSourceRun.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CutSceneManager_Kumagae.isCutSceneEnd) return;

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 障害物に当たったら移動速度減少させる
        if (collision.gameObject.tag == "Obstacle")
        {
            ChangeSpeed(brakePower);
            // 障害物に当たった時のアニメーションを再生する
            GetComponent<Animator>().SetBool("IsObstacleHit", true);
            // 足音を遅くする
            audioSourceRun.pitch = 0.8f;
            // 障害物に当たった時の効果音再生            
            audioSourceSE.PlayOneShot(acObstacle);
            // 煙エフェクトを小さくする
            smoke.SetSmokeScale(new Vector3(0.35f, 0.35f, 0.35f));
        }

        // アイテムに当たったら移動速度増加させる
        if (collision.gameObject.tag == "Item")
        {
             ChangeSpeed(accelPower);
            // アイテムに当たった時の効果音再生            
            audioSourceSE.PlayOneShot(acItem);
            // 取得時のエフェクトを出す
            var g = Instantiate(goFlashEffect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(g, 1.0f);
            Destroy(collision.gameObject);
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
        if (speed == originalSpeed)
        {
            speed *= accelMultiplier;
        }
        // 一定時間待つ
        yield return new WaitForSeconds(accelDuration);
        // 元の移動速度に戻す
        speed = originalSpeed;

        GetComponent<Animator>().SetBool("IsObstacleHit", false);
        // 足音を通常速度に戻す
        audioSourceRun.pitch = 1.0f;
        // 煙エフェクトを通常サイズにする
        smoke.SetSmokeScale(originalSmokeScale);
    }
}
