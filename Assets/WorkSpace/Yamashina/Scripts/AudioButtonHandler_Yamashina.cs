using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioButtonHandler_Yamashina : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler

{
    public string clickSEName = "";
    public string hoverSEName = "";

    // BGMを再生する
   

    public void OnPointerClick(PointerEventData eventData)
    {
        MultiAudio_Yamashina.ins.PlaySEByName(hoverSEName);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MultiAudio_Yamashina.ins.PlaySEByName(hoverSEName);
    }

    // SEを再生する
    public void PlaySE()
    {
        //SEの場合再生
        MultiAudio_Yamashina.ins.PlaySEByName(clickSEName);
    }

   
}
