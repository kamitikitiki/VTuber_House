using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Harituke : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    public float add_speed;

    //生成範囲値
    public int MaxSpeed;
    public int MinSpeed;
    public int MaxRotateFrameCount;
    public int MinRotateFrameCount;
    public int RotateEndCount;

    //pun
    private PhotonView m_View;

    //set player
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
    }

    // Update is called once per frame
    void Update()
    {
        if(m_RotateCount > 0)
        {
            if(m_NextRotateCount <= 0)
            {
                SetNextRotate();
            }

            transform.Rotate(m_NowRotate[0], m_NowRotate[1], m_NowRotate[2]);
            m_NextRotateCount--;
            //m_RotateCount--;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            if(other.transform.root.gameObject.GetComponent<PhotonView>().IsMine)
            {
                m_MoveFlag = 1;
                m_View.RequestOwnership();
            }
        }
    }

    //実装関数
    private void RotateStart()
    {
        m_RotateCount = RotateEndCount;
        m_MoveFlag = 1;
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

    //Takeover時に呼ばれる
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        throw new System.NotImplementedException();
    }
}

