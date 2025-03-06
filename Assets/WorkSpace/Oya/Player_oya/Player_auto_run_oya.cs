using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_auto_run_oya : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField,Header("加速時の倍率")] float accelMultiplier = 2f; // 加速時の倍率
    
    [SerializeField,Header("加速,減速する時間")] float accelDuration = 2f; // 加速する時間（秒）
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
        //Debug.Log(speed.ToString());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
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

        if (!isAccelerating) // すでに加速中でなければ開始
        {
        }
    }
    private IEnumerator Accelerate(float accelMultiplier)
    {
        isAccelerating = true;
        speed *= accelMultiplier; // 加速

        yield return new WaitForSeconds(accelDuration); // 指定時間待つ

        speed = originalSpeed; // 元の速度に戻す
        isAccelerating = false;
    }
}
