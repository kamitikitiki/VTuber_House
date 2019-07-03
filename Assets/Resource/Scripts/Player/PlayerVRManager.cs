using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class PlayerVRManager : MonoBehaviour
{
    void OnEnable()
    {
        if(GetComponent<PhotonView>().IsMine)
        {

            //左手取得とスクリプトOn
            transform.GetChild(0).GetComponent<SteamVR_Behaviour_Pose>().enabled = true;
            //右手取得とスクリプトOn
            transform.GetChild(1).GetComponent<SteamVR_Behaviour_Pose>().enabled = true;
            //頭の取得とスクリプトOn
            transform.GetChild(2).GetComponent<Camera>().enabled = true;
        }
    }
}
