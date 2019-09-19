using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallManeger : MonoBehaviour
{
    private Rigidbody rb;
    private Collider cd;
    private bool hit_flag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();
        rb.useGravity = false;
        hit_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        //rb.useGravity = false;
        hit_flag = false;
    }

    public bool IsHit()
    {
        return hit_flag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bat")
        {
            rb.useGravity = true;
            hit_flag = true;
            Debug.Log("HIT");
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.name == "Bat")
        {
            rb.useGravity = true;
            Debug.Log("HIT");
        }
    }
    */
}
