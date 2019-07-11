using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;
using RootMotion.FinalIK;

public class PlayerVRManager : MonoBehaviourPunCallbacks
{
    private bool m_IsInitialize = false;

    public GameObject MainMesh;
    public GameObject Wear;
    public GameObject SubMesh;

    //pun
    private PhotonView m_PhotonView;

    //コントローラー
    private SteamVR_Action_Vector2 m_TachPad;

    private void MyStateInit()
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_TachPad = SteamVR_Actions.default_TachPad;
        if (m_PhotonView.IsMine)
        {
            MainMesh.layer = LayerMask.NameToLayer("MyPlayer");
            Wear.layer = LayerMask.NameToLayer("MyPlayer");
            SubMesh.layer = LayerMask.NameToLayer("SubMesh");
        }
        m_IsInitialize = true;
    }

    public void Start()
    {
        MyStateInit();
        m_IsInitialize = true;

        Debug.Log("start");
    }

    public override void OnEnable()
    {
        if (m_IsInitialize == false)
        {
            MyStateInit();
        }

        if (m_PhotonView.IsMine)
        {
            //左手取得とスクリプトOn
            transform.GetChild(0).GetComponent<SteamVR_Behaviour_Pose>().enabled = true;
            //右手取得とスクリプトOn
            transform.GetChild(1).GetComponent<SteamVR_Behaviour_Pose>().enabled = true;
            //頭の取得とスクリプトOn
            transform.GetChild(2).GetComponent<Camera>().enabled = true;
        }
    }

    void Update()
    {
        if (m_PhotonView.IsMine)
        {
            if(SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                Vector2 tachPos = m_TachPad.GetAxis(SteamVR_Input_Sources.LeftHand);
                Vector3 movePos = Vector3.zero;
                movePos.x = tachPos.x * 0.1f;
                movePos.z = tachPos.y * 0.1f;
                transform.position += movePos;
            }
            if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                Vector3 moveRotate = transform.GetChild(2).transform.eulerAngles;
                moveRotate.x = 0;
                moveRotate.z = 0;
                transform.eulerAngles = moveRotate;
            }
        }
    }
}
