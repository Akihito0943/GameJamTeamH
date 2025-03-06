using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager_Yamashina : MonoBehaviour
{
    [SerializeField] public SceneInformation_Yamashina sceneInformation;
    [SerializeField] private GameObject fadePrefab; // フェード用プレハブ
    private Image fadeInstance; // 実際に使用するフェード用 Image
    [SerializeField] private SceneInformation_Yamashina.SCENE currentScene;  // 今のシーン                  // 今のシーン
    private bool isReloading = false; // リロード中かどうかを判定するフラグ
    [Header("フェード速度、 0.1 ～ 5.0の間で入力")]
    [SerializeField] private float fadeSpeed = 2.0f; // フェード速度
    public static SceneTransitionManager_Yamashina instance;

    



    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Managerオブジェクト全体を保持
                                           // 現在のシーンを取得して初期化
            SceneInformation_Yamashina.SCENE initialScene = (SceneInformation_Yamashina.SCENE)SceneManager.GetActiveScene().buildIndex;
            sceneInformation.SetCurrentScene(initialScene);

            SceneManager.sceneLoaded += OnSceneLoaded; // イベント登録

        }
        else
        {
            Destroy(gameObject); // 二重生成防止
        }
     

    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene Loaded: {scene.name}");

        // シーン名から `SCENE` Enum を取得して `currentScene` に設定
        if (Enum.TryParse(scene.name, out SceneInformation_Yamashina.SCENE loadedScene))
        {
            sceneInformation.SetCurrentScene(loadedScene);
        }
        InitializeReferences();
        PlayBGMForScene();


        isReloading = false; // シーン変更が完了したのでリセット
           //デバッグ用一旦シーン遷移が正しくできるか確認のため
      
    }
    public void ReloadCurrentScene()
    {
        if (isReloading) return; // リロード中なら処理をスキップ
        isReloading = true; // リロード中に設定
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().name));
    }
    public void InitializeReferences()
    {

        if (fadeInstance == null)
        {
            if (fadePrefab != null)
            {
                GameObject fadeObject = Instantiate(fadePrefab);
                fadeInstance = fadeObject.GetComponentInChildren<Image>(); // 子オブジェクトから Image を取得

                if (fadeInstance == null)
                {
                    Debug.LogError("fadePrefab に Image コンポーネントがありません。");
                }
                else
                {
                    Debug.Log("fadeInstance が正常に設定されました。");
                }

                // Canvas の設定
                Canvas fadeCanvas = fadeObject.GetComponent<Canvas>();
                if (fadeCanvas != null)
                {
                    fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    fadeCanvas.sortingOrder = 100;
                }
                fadeInstance.gameObject.SetActive(false);
                DontDestroyOnLoad(fadeObject);
            }
            else
            {
                Debug.LogError("フェード用プレハブが設定されていません。");
            }
        }
    }
    public void SetCurrentScene(int sceneIndex) { currentScene = (SceneInformation_Yamashina.SCENE)sceneIndex; }

    private void PlayBGMForScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string bgmName = "";

        switch (sceneName)
        {
            case string name when name == sceneInformation.GetSceneName(SceneInformation_Yamashina.SCENE.Title):
                bgmName = "BGM_title"; // タイトル画面のBGM名
                break;

            case string name when name == sceneInformation.GetSceneName(SceneInformation_Yamashina.SCENE.Main):

                bgmName = "BGM_stage";
                break;
            case string name when name == sceneInformation.GetSceneName(SceneInformation_Yamashina.SCENE.Result):
                bgmName = "BGM_result"; // ステージ1のBGM名
                break;
       
           

          
            default:
                Debug.LogWarning($"No BGM assigned for the scene '{sceneName}'.");
                return; // BGMが指定されていない場合は終了
        }

        if (!string.IsNullOrEmpty(bgmName))
        {
            if (MultiAudio_Yamashina.ins.bgmSource != null)
            {
                MultiAudio_Yamashina.ins.PlayBGM_ByName(bgmName); // BGMを再生

            }

           


            Debug.Log(bgmName);
        }
    }


    ///// <summary>
    ///// シーンを遷移させる
    ///// </summary>
    ///// <param name="scene"></param>

    public void SceneChange(SceneInformation_Yamashina.SCENE scene)
    {
        if (isReloading) return; // シーン変更中なら処理をスキップ
        isReloading = true; // シーン変更中に設定

        Debug.Log($"SceneChange called: newScene = {scene}, currentScene = {sceneInformation.currentScene}");

        // 先に `UpdateScene` を呼び出して `previousScene` を適切に設定
        sceneInformation.UpdateScene(scene);

        StartCoroutine(FadeOut(sceneInformation.GetSceneName(scene))); // シーン遷移

    }




    //ボタンでシーン遷移する場合
    public void NextSceneButton(int index)
    {
        GoToNextScene(index);
    }

    public void GoToNextScene(int index)
    {
        SceneChange((SceneInformation_Yamashina.SCENE)index);
    }



    // <summary>
    // 画面を明るくする
    // <summary>    / <returns
    private IEnumerator FadeIn()
    {
        fadeInstance.gameObject.SetActive(true);
        Color fadeColor = fadeInstance.color; // 一時変数を使用
        fadeColor.a = 1; // 最初は完全に不透明
        fadeInstance.color = fadeColor;
        float speed = 1.0f / fadeSpeed; // インスペクターから設定された速度を使用


        // 徐々に透明にする処理
        while (fadeInstance.color.a > 0)
        {
            fadeColor.a -= Time.unscaledDeltaTime * speed; // アルファ値を減少
            fadeInstance.color = fadeColor; // 更新
            yield return null;
        }

        // 完全に透明になったら非アクティブ化
        fadeInstance.gameObject.SetActive(false);
        isReloading = false; // リロードが完了したのでフラグをリセット
        sceneInformation.UpdateScene((SceneInformation_Yamashina.SCENE)SceneManager.GetActiveScene().buildIndex);

    }

    // <summary>
    // 画面を暗くする
    // </summary>
    // <returns></returns>
    private IEnumerator FadeOut(string stageName)
    {
        if (fadeInstance == null)
        {
            Debug.LogError("fadeInstance=null");
        }
        fadeInstance.gameObject.SetActive(true);
        Color fadeColor = fadeInstance.color; // 一時変数を使用
        fadeColor.a = 0; // 最初は完全に透明
        fadeInstance.color = fadeColor;
        float speed = 1.0f / fadeSpeed; // インスペクターから設定された速度を使用

        // 徐々に不透明にする処理
        while (fadeInstance.color.a < 1)
        {
            fadeColor.a += Time.unscaledDeltaTime * speed; // アルファ値を増加
            fadeInstance.color = fadeColor; // 更新
            yield return null;
        }
        
        // **シーン遷移処理**
        if (sceneInformation.GetNextSceneInt() >sceneInformation.sceneCount.Length)
        {
            // 最後のシーンの後はタイトルへ
            stageName = sceneInformation.GetSceneName(SceneInformation_Yamashina.SCENE.Title);
        }
        // シーンの非同期読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stageName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        


        StartCoroutine(FadeIn()); // フェードイン開始
        PlayBGMForScene();

    }




    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }










}

