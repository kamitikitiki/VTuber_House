using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallManeger : MonoBehaviour
{
    private Rigidbody rb;
    private Collider cd;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();
        rb.isKinematic = true;
        rb.useGravity = false;
        cd.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        cd.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.name == "Bat")
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            cd.isTrigger = false;
            Debug.Log("HIT");
        }
    }
}
