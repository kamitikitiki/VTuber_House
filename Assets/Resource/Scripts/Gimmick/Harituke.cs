using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using RootMotion.FinalIK;

public class Harituke : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    public float add_speed;

    //生成範囲値
    public int MaxSpeed;
    public int MinSpeed;
    public int MaxRotateFrameCount;
    public int MinRotateFrameCount;
    public int RotateEndCount;

    //固定ポジションボーン
    public Transform m_SetHand_L;
    public Transform m_SetHand_R;
    public Transform m_SetFoot_L;
    public Transform m_SetFoot_R;
    private FixedJoint m_JoinHand_L = null;
    private FixedJoint m_JoinHand_R = null;
    private FixedJoint m_JoinFoot_L = null;
    private FixedJoint m_JoinFoot_R = null;

    //pun
    private PhotonView m_View;

    //set player
    private GameObject m_Player = null;
    private Transform m_Hand_L = null;
    private Transform m_Hand_R = null;
    private Transform m_Foot_L = null;
    private Transform m_Foot_R = null;

    //move state
    private short m_MoveFlag;

    private int m_RotateCount;
    private int m_NextRotateCount;
    private int m_DireyCount;

    private int[] m_NowRotate = { 0,0,0 };


    // Start is called before the first frame update
    void Start()
    {
        m_RotateCount = 0;
        m_MoveFlag = 0;
        m_DireyCount = 0;
        m_View = GetComponent<PhotonView>();
        m_JoinHand_L = m_SetHand_L.transform.GetComponent<FixedJoint>();
        m_JoinHand_R = m_SetHand_R.transform.GetComponent<FixedJoint>();
        m_JoinFoot_L = m_SetFoot_L.transform.GetComponent<FixedJoint>();
        m_JoinFoot_R = m_SetFoot_R.transform.GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_View.IsMine)
        {
            //起動開始
            if(m_MoveFlag == 1)
            {
                m_JoinHand_L.connectedBody = m_Hand_L.GetComponent<Rigidbody>();
                m_JoinHand_R.connectedBody = m_Hand_R.GetComponent<Rigidbody>();
                m_JoinFoot_L.connectedBody = m_Foot_L.GetComponent<Rigidbody>();
                m_JoinFoot_R.connectedBody = m_Foot_R.GetComponent<Rigidbody>();
                m_Player.transform.GetChild(1).GetComponent<RagdollManager>().SetRagdoll(true, 0);
            }
            if (m_RotateCount > 0)
            {
                if (m_NextRotateCount <= 0)
                {
                    SetNextRotate();
                }

                transform.Rotate(m_NowRotate[0], m_NowRotate[1], m_NowRotate[2]);
                m_NextRotateCount--;
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            m_View.RequestOwnership();
            Debug.Log("req");
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log(m_View.IsMine);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            if(other.transform.root.gameObject.GetComponent<PhotonView>().IsMine)
            {
                m_View.RequestOwnership();
                m_Player = other.transform.root.gameObject;
            }
  
        }
    }

    //実装関数
    private void StateInit()
    {
        m_RotateCount = 0;
        m_MoveFlag = 0;
        m_DireyCount = 0;
        m_Player = null;
        m_Hand_L = null;
        m_Hand_R = null;
        m_Foot_L = null;
        m_Foot_R = null;
}

    private void RotateStart()
    {
        //m_RotateCount = RotateEndCount;
        m_MoveFlag = 1;
        //プレイヤーの手と足を取得
        VRIK vrik = m_Player.transform.GetChild(1).GetComponent<VRIK>();
        m_Hand_L = vrik.references.leftCalf;
        m_Hand_R = vrik.references.rightCalf;
        m_Foot_L = vrik.references.leftForearm;
        m_Foot_R = vrik.references.rightForearm;
        m_Hand_L.SetPositionAndRotation(m_SetHand_L.position, m_SetHand_L.rotation);
        m_Hand_R.SetPositionAndRotation(m_SetHand_R.position, m_SetHand_R.rotation);
        m_Foot_L.SetPositionAndRotation(m_SetFoot_L.position, m_SetFoot_L.rotation);
        m_Foot_R.SetPositionAndRotation(m_SetFoot_R.position, m_SetFoot_R.rotation);
    }

    private void SetNextRotate()
    {
        m_NextRotateCount = Random.Range(MinRotateFrameCount, MaxRotateFrameCount);
        int dir = Random.Range(0, 2);

        int s = Random.Range(MinSpeed, MaxSpeed);
        if (m_NowRotate[dir] > 0)
        {
            m_NowRotate[dir] = -s;
        }
       else if (m_NowRotate[dir] <= 0)
        {
            m_NowRotate[dir] = s;
        }
    }

    //Request時に呼ばれる
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        //オブジェクト未使用時に渡す
        if(m_MoveFlag == 0)
        {
            targetView.TransferOwnership(requestingPlayer);
        }
    }

    //オーナー変更時に呼ばれる
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (m_View.IsMine == true)
        {
            if (m_MoveFlag == 0)
            {
                if (m_Player != null)
                {
                    //ギミック起動
                    RotateStart();
                }
            }
        }
    }
}

