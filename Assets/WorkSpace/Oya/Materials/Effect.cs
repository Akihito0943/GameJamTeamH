using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem fireEffect;  // 炎エフェクト
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // Animator取得
    }

    void Update()
    {
    }
}
