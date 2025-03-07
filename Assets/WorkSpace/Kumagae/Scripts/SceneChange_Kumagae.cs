using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange_Kumagae : MonoBehaviour
{
    [SerializeField, Header("Fade用のキャンバス")]
    Canvas canvasFade;

    [SerializeField, Header("Fade用のキャンバスグループ")]
    CanvasGroup cgFade;

    [SerializeField, Header("フェードにかかる時間")]
    float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator ThrowAfterCoolTimer()
    {
        yield return new WaitForSeconds(fadeTime);
    }

}
