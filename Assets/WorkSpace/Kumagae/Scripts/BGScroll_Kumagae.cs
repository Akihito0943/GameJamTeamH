using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll_Kumagae : MonoBehaviour
{
    // 背景スクロールを行うイメージコンポーネント
    [Header("イメージ")]
    [SerializeField] RectTransform image;
    
    private Vector3 lastCameraPos;
    private Vector2 texOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの初期座標を取得
        lastCameraPos = Camera.main.transform.position;
        lastCameraPos.x -= 2;

        Debug.Log("画像の初期座標" + image.transform.position);
        Debug.Log("カメラの初期座標" + lastCameraPos);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("画像の初期座標" + image.transform.position);

        Vector2 cameraMoveSpeed = Camera.main.transform.position - lastCameraPos;

        image.transform.position -= new Vector3(cameraMoveSpeed.x, cameraMoveSpeed.y, 0);

        lastCameraPos = Camera.main.transform.position;
    }
}
