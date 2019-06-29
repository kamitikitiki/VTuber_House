using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Camera_pun : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // データを送受信するメソッド
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
