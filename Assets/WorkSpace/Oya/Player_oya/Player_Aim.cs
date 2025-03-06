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

    [SerializeField, Header("輪っかを投げた回数")]
    static float allCount = 0;

    [SerializeField, Header("クールタイム")]
    float coolTime = 2;

    [SerializeField, Header("クールタイム中かどうか")]
    private bool isCoolTime = false;


    // Update is called once per frame
    void Update()
    {
        // マウスの座標を取得
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // マウスの方向を計算し160度の範囲に移動を制限する
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
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
            }
        }
    }


    /// <summary>
    /// 弾を投げる
    /// </summary>
    private void Throw()
    {
        //投げた回数をカウント
        allCount++;
        Debug.Log(allCount);

        // 弾を生成する
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);

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
