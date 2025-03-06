using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_throw : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;

    [SerializeField] float coolTime = 100;
    [SerializeField] float maxCoolTime = 100;
    private bool isCoolTime = false;
    [SerializeField] static float allCount = 0;


    // Update is called once per frame
    void Update()
    {
        if (coolTime < maxCoolTime)
        {
            coolTime += 0.5f;
        }
        Throw();
    }

    private void Throw()
    {
        if (coolTime >= maxCoolTime)
        {
            isCoolTime = true;
        }
        if (isCoolTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //ìäÇ∞ÇΩâÒêî
                allCount++;
                Debug.Log(allCount);
                //Transform transform = gameObject.transform;
                Instantiate(bullet, transform);
                animator.SetBool("isThrow", true);
                isCoolTime = false;
                coolTime = 0;

            }
            else
            {
                animator.SetBool("isThrow", false);
            }


        }
    }

}
