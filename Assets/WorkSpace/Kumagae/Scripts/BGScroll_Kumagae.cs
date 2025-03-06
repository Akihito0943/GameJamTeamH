using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll_Kumagae : MonoBehaviour
{
    // �w�i�X�N���[�����s���}�e���A��
    [Header("�}�e���A��")]
    [SerializeField] Material material;

    private Vector3 lastCameraPos;
    
    // Start is called before the first frame update
    void Start()
    {
        // �J�����̏������W���擾
        lastCameraPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (material == null) return;

        Vector2 cameraMoveSpeed = Camera.main.transform.position - lastCameraPos;

        //material.SetTextureOffset("background", cameraMoveSpeed);

        lastCameraPos = Camera.main.transform.position;
    }
}
