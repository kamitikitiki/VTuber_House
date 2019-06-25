using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour
{
    private bool KeyLock;

    // Start is called before the first frame update
    void Start()
    {
        KeyLock = false;
        Debug.Log("start");
        PhotonConnect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  PhotonConnect()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
        Debug.Log("connect");
    }


}
