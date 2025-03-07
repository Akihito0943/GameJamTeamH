using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallOut_Kumagae : MonoBehaviour
{
    void Update()
    {
        // Y座標が-15以下になったらゲーム終了
        if(transform.position.y < -15)
        {
            Debug.Log("落ちた");
        }
    }
}
