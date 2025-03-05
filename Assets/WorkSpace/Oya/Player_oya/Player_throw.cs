using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_throw : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             //Transform transform = gameObject.transform;
            Instantiate(bullet,transform);
            animator.SetBool("isThrow", true);
        }
        else
        {
            animator.SetBool("isThrow", false);
        }


    }
    
}
