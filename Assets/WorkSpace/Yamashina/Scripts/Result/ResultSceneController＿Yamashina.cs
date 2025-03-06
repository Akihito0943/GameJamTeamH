using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneController＿Yamashina : MonoBehaviour
{
    [SerializeField, Header("リザルトイメージを表示させる場所")] private Image resultImage;
    [SerializeField] private Text elapsedTimeText;
    [SerializeField] private Text throwCountText;

    [SerializeField, Header("プレイヤーが勝ったときのリザルト画像")] private Sprite VictoriousSprite; // 弾に当たった場合の画像
    [SerializeField, Header("プレイヤーが負けたときのリザルト画像")] private Sprite defeatedSprite;  // 逃げ切った場合の画像
    // Start is called before the first frame update
    void Start()
    {
        elapsedTimeText.text = "";
        throwCountText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeText.text = Mathf.FloorToInt(Time.time).ToString();
        throwCountText.text = "";
        // GameManager の状態によって画像を変更
        switch (GameManager_Yamashina.GetState())
        {
            case GameManager_Yamashina.EnemyState.Defeated:
                resultImage.sprite = VictoriousSprite;

                break;
            case GameManager_Yamashina.EnemyState.Escaped:
                resultImage.sprite = defeatedSprite;
                break;
            default:
                Debug.LogWarning("GameManagerのenemyStateが未設定です");
                break;
        }
    }
}
