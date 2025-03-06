using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll_Kumagae : MonoBehaviour
{
    // �w�i�X�N���[�����s���C���[�W�R���|�[�l���g
    [Header("�C���[�W")]
    [SerializeField] Image image;

    private Vector3 lastCameraPos;
    private Vector2 texOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        // �J�����̏������W���擾
        lastCameraPos = Camera.main.transform.position;

        //image.transform.position = 
        //    new Vector3(Camera.main.transform.position.x, image.transform.position.y, image.transform.position.z);

        Debug.Log(image.transform.position.x);
        Debug.Log(lastCameraPos);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 cameraMoveSpeed = Camera.main.transform.position - lastCameraPos;

        image.transform.position -= new Vector3(cameraMoveSpeed.x, cameraMoveSpeed.y, 0);

        lastCameraPos = Camera.main.transform.position;
    }
}
