using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retur : MonoBehaviour
{
    void Start(){
      
    }

    void Update()
    {
     
    }
    //Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
         transform.Rotate (new Vector3(0, 0, 1));
    }
}
