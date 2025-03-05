using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_auto_run_oya : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float accelMultiplier = 2f; // 加速時の倍率
    [SerializeField] float accelDuration = 2f; // 加速する時間（秒）

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
        if (!isAccelerating) // すでに加速中でなければ開始
        {
            StartCoroutine(Accelerate(accelMultiplier));
        }
    }
    private IEnumerator Accelerate(float accelMultiplier)
    {
        isAccelerating = true;
        float originalSpeed = speed;
        speed *= accelMultiplier; // 加速

        yield return new WaitForSeconds(accelDuration); // 指定時間待つ

        speed = originalSpeed; // 元の速度に戻す
        isAccelerating = false;
    }
}
