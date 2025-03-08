using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hukidashi_Kumagae : MonoBehaviour
{
    [SerializeField, Header("敵")]
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.x);
    }
}
