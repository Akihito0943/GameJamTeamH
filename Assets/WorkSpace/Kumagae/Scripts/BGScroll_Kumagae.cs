using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll_Kumagae : MonoBehaviour
{
    // 背景スクロールを行うマテリアル
    [Header("マテリアル")]
    [SerializeField] Material material;

    private Vector3 lastCameraPos;
    
    // Start is called before the first frame update
    void Start()
    {
        // カメラの初期座標を取得
        lastCameraPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (material == null) return;

        Vector2 cameraMoveSpeed = Camera.main.transform.position - lastCameraPos;

        //material.SetTextureOffset("background", cameraMoveSpeed);

        lastCameraPos = Camera.main.transform.position;
    }
}
