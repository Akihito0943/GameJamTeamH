using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TItleManager_Yamashina : MonoBehaviour
{
    [Header("パネルのオブジェクトのセットアクティブ切り替え用")]
    [Tooltip("タイトル画面のオブジェクトを入れる")]

    public GameObject mainPanel;

    [Tooltip("クレジット画面のオブジェクトを入れる")]
    public GameObject DescriptionPanel;

    [Tooltip("オプション画面のオブジェクトを入れる")]
    public GameObject OptionPanel;
    [SerializeField, Header("ゲームスタートのボタンを設定")] private Button StartButton;
    [Tooltip("はじめからボタンのイベントトリガーを入れる")]
    private EventTrigger eventTrigger_Start;
    // Start is called before the first frame update
    void Start()
    {
        eventTrigger_Start=StartButton.GetComponent<EventTrigger>();
        StartButton.onClick.AddListener(() =>
        {
            SceneTransitionManager_Yamashina.instance.NextSceneButton(1);
        });
    }

    private void Update()
    {
        
    }


}
