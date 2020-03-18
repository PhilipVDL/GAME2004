using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 aimVec;
    private Transform aimTarget;
    public float speed = 10f;

    void Start()
    {
        Destroy(this.gameObject, 5f);
        if(aimTarget == null)
        {
            aimTarget = GameObject.FindWithTag("Player").transform;
        }
        aimVec = aimTarget.position - transform.position;
    }

    void Update()
    {
        transform.position += aimVec * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
