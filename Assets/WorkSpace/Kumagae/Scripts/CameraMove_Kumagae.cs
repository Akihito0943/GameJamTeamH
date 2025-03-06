using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_Kumagae : MonoBehaviour
{

    // プレーヤーオブジェクトのトランスフォームコンポーネント
    [Header("プレーヤートランスフォーム")]
    [SerializeField] Transform trPlayer;

    // 画面の左端からどれくらいプレーヤーを離すか
    [Header("画面の左端からのオフセット")]
    [SerializeField] float offset;

    // 画面の左端
    private Vector3 screenLeftEdge;

    // プレーヤーのスプライトレンダラー
    SpriteRenderer srPlayer;

    void Start()
    {
        // 画面の左端の座標を取得
        screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));

        // プレーヤーのスプライトレンダラーコンポーネントを取得
        srPlayer = trPlayer.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (trPlayer == null) return;

        // プレイヤーの幅（半分のサイズ）を取得
        float playerWidth = srPlayer.bounds.extents.x;

        // 新しいカメラの座標を設定
        float newCameraPosX = trPlayer.position.x - (screenLeftEdge.x + playerWidth + offset);

        // カメラの座標を更新
        transform.position = new Vector3(newCameraPosX, transform.position.y, transform.position.z);
    }
}
