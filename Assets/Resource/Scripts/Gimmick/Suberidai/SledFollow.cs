using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledFollow : MonoBehaviour
{
    public Transform sled;

    private bool f_SledFollow = true;

    // Update is called once per frame
    void Update()
    {
        if(f_SledFollow == true)
        {
            transform.LookAt(sled);
        }
    }

    public void changeFollowFlag(bool follow)
    {
        f_SledFollow = follow;
    }
}
