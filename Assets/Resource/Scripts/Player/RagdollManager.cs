using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using Photon.Pun; 

public class RagdollManager : MonoBehaviour
{
    public GameObject PlayerRig;
    public GameObject Camera;
    public GameObject Head;
    public GameObject[] RagdollBone;

    Vector3 basePos = Vector3.zero;
    Quaternion baseRota;
    Photon.Pun.PhotonView m_PhotonView;

    private int m_OnRagdollCount;

    // Start is called before the first frame update
    void Start()
    {
        basePos = PlayerRig.transform.position;
        m_OnRagdollCount = 0;
    }

    private void OnEnable()
    {
        basePos = PlayerRig.transform.position;
        m_PhotonView = GetComponent<PhotonView>();
        m_OnRagdollCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PhotonView.IsMine)
        {
            if(m_OnRagdollCount >= 1)
            {
                m_OnRagdollCount--;
                if(m_OnRagdollCount == 0)
                {
                    m_PhotonView.RPC("OffRagdoll", RpcTarget.AllViaServer);
                }
            }

            if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                SetRagdoll(true, 0);
            }
        }
    }

    private void LateUpdate()
    {
        if (m_PhotonView.IsMine)
        {
            if (Head.GetComponent<Rigidbody>().isKinematic == false)
            {
                var trackingPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);

                var scale = transform.localScale;
                trackingPos = new Vector3(
                    trackingPos.x * scale.x,
                    trackingPos.y * scale.y,
                    trackingPos.z * scale.z
                );

                PlayerRig.transform.rotation = Head.transform.rotation;

                // 回転
                trackingPos = PlayerRig.transform.rotation * trackingPos;

                // 固定したい位置から hmd の位置を
                // 差し引いて実質 hmd の移動を無効化する
                PlayerRig.transform.position = Head.transform.position + basePos - trackingPos;
            }
        }
    }

    //外部からラグドールを設定する関数
    public void SetRagdoll(bool active, int onCount)
    {
        if(active)
        {
            m_PhotonView.RPC("OnRagdoll", RpcTarget.AllViaServer);
            m_OnRagdollCount = onCount;
        }
        else
        {
            m_PhotonView.RPC("OffRagdoll", RpcTarget.AllViaServer);
        }
    }

    //新規実装関数
    [PunRPC]
    private void OnRagdoll()
    {
        Head.GetComponent<Rigidbody>().useGravity = true;
        Head.GetComponent<Rigidbody>().isKinematic = false;
        Head.GetComponent<Collider>().enabled = true;
        for (int i = 0; i < RagdollBone.Length; i++)
        {
            RagdollBone[i].GetComponent<Rigidbody>().useGravity = true;
            RagdollBone[i].GetComponent<Rigidbody>().isKinematic = false;
            RagdollBone[i].GetComponent<Collider>().enabled = true;
        }

        GetComponent<VRIK>().enabled = false;


    }

    [PunRPC]
    private void OffRagdoll()
    {
        Head.GetComponent<Rigidbody>().useGravity = false;
        Head.GetComponent<Rigidbody>().isKinematic = true;
        Head.GetComponent<Collider>().enabled = false;
        for (int i = 0; i < RagdollBone.Length; i++)
        {
            RagdollBone[i].GetComponent<Rigidbody>().useGravity = false;
            RagdollBone[i].GetComponent<Rigidbody>().isKinematic = true;
            RagdollBone[i].GetComponent<Collider>().enabled = false;
        }

        GetComponent<VRIK>().enabled = true;
        PlayerRig.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        m_OnRagdollCount = 0;
    }
}
