using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_auto_run_oya : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float accelMultiplier = 2f; // �������̔{��
    [SerializeField] float accelDuration = 2f; // �������鎞�ԁi�b�j

    private bool isAccelerating = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
            ChangeSpeed(20);
        }

    }
    private void ChangeSpeed(float accelMultiplier)
    {
        if (!isAccelerating) // ���łɉ������łȂ���ΊJ�n
        {
            StartCoroutine(Accelerate(accelMultiplier));
        }
    }
    private IEnumerator Accelerate(float accelMultiplier)
    {
        isAccelerating = true;
        float originalSpeed = speed;
        speed *= accelMultiplier; // ����

        yield return new WaitForSeconds(accelDuration); // �w�莞�ԑ҂�

        speed = originalSpeed; // ���̑��x�ɖ߂�
        isAccelerating = false;
    }
}
