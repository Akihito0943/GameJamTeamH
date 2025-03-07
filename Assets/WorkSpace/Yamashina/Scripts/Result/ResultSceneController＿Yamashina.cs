using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneController＿Yamashina : MonoBehaviour
{
    [SerializeField, Header("リザルトイメージを表示させる場所")] private Image resultImage;
    [SerializeField, Header("リザルトの文字のイメージを表示させる場所")] private Image resultTextImage;

    [SerializeField, Header("ゲームリスタートボタン")] private Button restartButton;
    [SerializeField, Header("ゲームリスタートボタン")] private Button titleButton;

    [SerializeField, Header("経過時間のテキスト表示場所")] private Text elapsedTimeText;
    [SerializeField, Header("投げた回数のテキスト場所")] private Text throwCountText;

    [SerializeField, Header("プレイヤーが勝ったときのリザルト画像")] private Sprite VictoriousSprite; // 弾に当たった場合の画像
    [SerializeField, Header("プレイヤーが負けたときのリザルト画像")] private Sprite defeatedSprite;  // 逃げ切った場合の画像
    [SerializeField, Header("プレイヤーが勝ったときのリザルト画像")] private Sprite VictoriousTextSprite; // 弾に当たった場合の画像
    [SerializeField, Header("プレイヤーが負けたときのリザルト画像")] private Sprite defeatedTextSprite;  // 逃げ切った場合の画像
    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();

    }

    private void InitializeReferences()
    {
        int totalSeconds = Mathf.FloorToInt(Time.time); // 経過秒数を整数化
        int minutes = totalSeconds / 60;  // 分を計算
        int seconds = totalSeconds % 60;  // 余った秒を計算
        elapsedTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        throwCountText.text = GameManager_Yamashina.allCount.ToString();
        restartButton.onClick.AddListener(() => DestroyGameManager_WithSceneChange_Restart());
        titleButton.onClick.AddListener(() => DestroyGameManager_WithSceneChange());

        // GameManager の状態によって画像を変更
        switch (GameManager_Yamashina.GetState())
        {
            case GameManager_Yamashina.EnemyState.Defeated:
                resultImage.sprite = VictoriousSprite;
                resultTextImage.sprite = VictoriousTextSprite;

                break;
            case GameManager_Yamashina.EnemyState.Escaped:
                resultImage.sprite = defeatedSprite;
                resultTextImage.sprite = defeatedTextSprite;
                break;
            default:
                Debug.LogWarning("GameManagerのenemyStateが未設定です");
                break;
        }
    }

    void DestroyGameManager_WithSceneChange()
    {
        SceneTransitionManager_Yamashina.instance.NextSceneButton(0);
        GameManager_Yamashina.ChangeState(GameManager_Yamashina.EnemyState.None);


    }
    void DestroyGameManager_WithSceneChange_Restart()

    {
        SceneTransitionManager_Yamashina.instance.NextSceneButton(1);
        GameManager_Yamashina.ChangeState(GameManager_Yamashina.EnemyState.None);

    }
}
