using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem fireEffect;  // ���G�t�F�N�g
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // Animator�擾
    }

    void Update()
    {
    }
}
