using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVFX_Kumagae : MonoBehaviour
{

    [SerializeField, Header("土煙エフェクトオブジェクト")] GameObject goSmoke;

    // 元の土煙エフェクトのスケール
    private Vector3 originalSmokeScale;

    // 変更するスケール
    private Vector3 smokeScale;

    // 係数
    private float t = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        originalSmokeScale = goSmoke.transform.localScale;
        smokeScale = goSmoke.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (t <= 1.0f)
        {
            t += Time.deltaTime;
            // 土煙エフェクトを小さくする
            goSmoke.transform.localScale = Vector3.Lerp(originalSmokeScale, smokeScale, t);
        }
    }

    /// <summary>
    /// スケールを設定する
    /// </summary>
    /// <param name="scale"></param>
    public void SetSmokeScale(Vector3 scale)
    {
        t = 0.0f;
        originalSmokeScale = goSmoke.transform.localScale;
        smokeScale = scale;
    }
}
