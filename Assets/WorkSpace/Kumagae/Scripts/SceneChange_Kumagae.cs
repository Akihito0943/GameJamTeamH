using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Kumagae : MonoBehaviour
{
    [SerializeField, Header("Fade用のキャンバス")]
    Canvas canvasFade;

    [SerializeField, Header("Fade用のキャンバスグループ")]
    CanvasGroup cgFade;

    [SerializeField, Header("フェードにかかる時間")]
    float fadeTime;

    [SerializeField, Header("セットすれば次のシーンに遷移する(Inの時のみ)")]
    string nextScene;

    private float timer = 0.0f;

    enum FadeType
    {
        In,
        Out,
    }

    [SerializeField,Header("フェードインかアウトか")]
    FadeType type= FadeType.Out;

    // Update is called once per frame
    void Update()
    {
        if(type == FadeType.Out)
        {
            FadeOut();
        }
        else if(type == FadeType.In)
        {
            FadeIn();
        }
    }

    /// <summary>
    /// フェードアウトを行う
    /// </summary>
    void FadeOut()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fadeTime); // 0～1に制限
        cgFade.alpha = Mathf.Lerp(1, 0, t);
        if (t >= 1.0f)
        {
            canvasFade.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// フェードインを行う
    /// </summary>
    void FadeIn()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fadeTime); // 0～1に制限
        cgFade.alpha = Mathf.Lerp(0, 1, t);

        if(nextScene!= null)
        {
            if(t >= 1.0f)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
