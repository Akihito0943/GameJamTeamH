using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll_Kumagae : MonoBehaviour
{
    // 背景スクロールを行うイメージコンポーネント
    [Header("イメージ")]
    [SerializeField] Image image;
    
    // 1フレーム前のカメラの座標
    private Vector3 lastCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの初期座標を取得
        lastCameraPos = Camera.main.transform.position;

        // カメラムーブコンポーネントからオフセット値を取得し、オフセット分の差を修正
        lastCameraPos.x -= Camera.main.GetComponent<CameraMove_Kumagae>().GetOffset();
    }

    // Update is called once per frame
    void Update()
    {
        // カメラの移動速度を計算する
        Vector2 cameraMoveSpeed = Camera.main.transform.position - lastCameraPos;

        // 画像の座標を更新する
        image.transform.position -= new Vector3(cameraMoveSpeed.x, cameraMoveSpeed.y, 0);

        // 座標を更新する
        lastCameraPos = Camera.main.transform.position;
    }
}
