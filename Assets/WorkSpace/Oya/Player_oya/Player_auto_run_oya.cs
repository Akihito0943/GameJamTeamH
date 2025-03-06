using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_auto_run_oya : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float accelMultiplier = 2f; // �������̔{��
    [SerializeField] float accelDuration = 2f; // �������鎞�ԁi�b�j
    private float originalSpeed;
    private bool isAccelerating = false;
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            ChangeSpeed(0.5f);
        }
        if (collision.gameObject.tag == "Item")
        {
            ChangeSpeed(5);
        }

    }
    private void ChangeSpeed(float accelMultiplier)
    {
        StartCoroutine(Accelerate(accelMultiplier));

        if (!isAccelerating) // ���łɉ������łȂ���ΊJ�n
        {
        }
    }
    private IEnumerator Accelerate(float accelMultiplier)
    {
        isAccelerating = true;
        speed *= accelMultiplier; // ����

        yield return new WaitForSeconds(accelDuration); // �w�莞�ԑ҂�

        speed = originalSpeed; // ���̑��x�ɖ߂�
        isAccelerating = false;
    }
}
