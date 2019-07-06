using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;
using Photon.Pun; 

public class RagdollManager : MonoBehaviour
{
    public GameObject PlayerRig;
    public GameObject Head;
    public GameObject[] RagdollBone;

    Vector3 basePos = Vector3.zero;
    Quaternion baseRota;
    Photon.Pun.PhotonView m_PhotonView;

    // Start is called before the first frame update
    void Start()
    {
        basePos = PlayerRig.transform.position;
    }

    private void OnEnable()
    {
        //basePos = Camera.transform.position;
        m_PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if(Head.GetComponent<Rigidbody>().isKinematic == true)
            {
                OnRagdoll();
                m_PhotonView.RPC("OnRagdoll", RpcTarget.AllViaServer);
            }
            else
            {
                OffRagdoll();
                m_PhotonView.RPC("OffRagdoll", RpcTarget.AllViaServer);
            }
        }
    }

    private void LateUpdate()
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

    //新規実装関数
    [PunRPC]
    public void OnRagdoll()
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
    public void OffRagdoll()
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
    }
}
