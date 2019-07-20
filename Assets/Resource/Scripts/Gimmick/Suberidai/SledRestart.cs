using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledRestart : MonoBehaviour
{

    public GameObject Root;
    public GameObject Sled;

    public Vector3 position;
    public Vector3 rotation;

    public Vector3 angVelo;
    public Vector3 Velo;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Transform tra = Root.transform;
            //tra.position = position;
            //tra.eulerAngles = rotation;
            Transform tra2 = Sled.transform;
            tra2.position = position;
            tra2.eulerAngles = rotation;

            Rigidbody rigid = Sled.transform.GetComponent<Rigidbody>();
            rigid.angularVelocity = angVelo;
            rigid.velocity = Velo;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sled")
        {
            Vector3 pos = other.transform.position;
            Vector3 rot = other.transform.eulerAngles;
            Vector3 av = other.GetComponent<Rigidbody>().angularVelocity;
            Vector3 v = other.GetComponent<Rigidbody>().velocity;
            Debug.Log(pos.x + ":" + pos.y + ":" + pos.z);
            Debug.Log(rot.x + ":" + rot.y + ":" + rot.z);
            Debug.Log(av.x + ":" + av.y + ":" + av.z);
            Debug.Log(v.x + ":" + v.y + ":" + v.z);
        }
    }
}
