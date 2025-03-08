using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Aim : MonoBehaviour
{
    [SerializeField, Header("バレットのプレハブ")]
    GameObject bullet;

    [SerializeField, Header("バレットのトランスフォーム")]
    Transform bulletTransform;

    [SerializeField, Header("プレイヤーのアニメーター")]
    Animator animator;


    [SerializeField, Header("クールタイム")]
    float coolTime = 2;

    [SerializeField, Header("クールタイム中かどうか")]
    private bool isCoolTime = false;

    [SerializeField, Header("効果音用のオーディオソース")] AudioSource audioSourceSE;
    [SerializeField, Header("投げた時の効果音")] AudioClip acThrow;


    float rotZ;

    private void Start()
    {
        audioSourceSE.volume = MultiAudio_Yamashina.ins.seSource.volume;

    }

    // Update is called once per frame
    void Update()
    {
        // マウスの座標を取得
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // マウスの方向を計算し160度の範囲に移動を制限する
        Vector3 rotation = mousePos - transform.position;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotZ = Mathf.Clamp(rotZ, -80, 80);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // 左クリックで弾を投げる
        if (Input.GetMouseButtonDown(0))
        {
            // クールタイム中でなければ弾を発射する
            if (!isCoolTime)
            {
                isCoolTime = true;
                StartCoroutine(ThrowAfterCoolTimer());
                // 効果音を再生
                audioSourceSE.PlayOneShot(acThrow);
            }
        }
    }


    /// <summary>
    /// 弾を投げる
    /// </summary>
    private void Throw()
    {
        //投げた回数をカウント
        GameManager_Yamashina.allCount++;
        MultiAudio_Yamashina.ins.PlaySEByName("SE_Throw");

        // 弾を生成する
        GameObject goBullet = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        // 弾の速度と発射角度を設定
        Rigidbody2D rbBullet = goBullet.GetComponent<Rigidbody2D>();
        Bullet_Kumagae bk = goBullet.GetComponent<Bullet_Kumagae>();
        Vector2 direction = new Vector2(Mathf.Cos(rotZ * Mathf.Deg2Rad), Mathf.Sin(rotZ * Mathf.Deg2Rad));
        rbBullet.velocity = direction * bk.GetSpeed();

        // 投げるアニメーションを再生する
        animator.SetTrigger("Throw");
    }


    /// <summary>
    /// クールタイム後に弾を投げる
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThrowAfterCoolTimer()
    {
        Throw();

        yield return new WaitForSeconds(coolTime);

        isCoolTime = false;
    }
}
