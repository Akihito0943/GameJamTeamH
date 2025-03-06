using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Aim : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform bulletTransform;
    
    [SerializeField] Animator animator;
    [SerializeField] static float allCount = 0;



    [SerializeField] float coolTime = 100;
    [SerializeField] float maxCoolTime = 100;
    private bool isCoolTime = false;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Clamp(rotZ, -80, 80);

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

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
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
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
