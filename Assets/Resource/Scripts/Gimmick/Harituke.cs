using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using RootMotion.FinalIK;

public class Harituke : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{

    private Transform chill;

    //生成範囲値
    public float Radius;
    public int MaxSpeed;
    public int MinSpeed;
    public int MaxRotateFrameCount;
    public int MinRotateFrameCount;
    public int RotateEndCount;

    //オブジェクト初期位置
    private Vector3 StartPosition;

    //起動準備カウント
    private int StartCount = 120;
    private int m_StartCount;
    private int m_DirayCount;

    //固定ポジションボーン
    public Transform m_SetHead;
    public Transform m_SetHand_L;
    public Transform m_SetHand_R;
    public Transform m_SetFoot_L;
    public Transform m_SetFoot_R;
    private FixedJoint m_JoinHead = null;
    private FixedJoint m_JoinHand_L = null;
    private FixedJoint m_JoinHand_R = null;
    private FixedJoint m_JoinFoot_L = null;
    private FixedJoint m_JoinFoot_R = null;

    //pun
    private PhotonView m_View;

    //set player
    private GameObject m_Player = null;
    private Transform m_Head = null;
    private Transform m_Hand_L = null;
    private Transform m_Hand_R = null;
    private Transform m_Foot_L = null;
    private Transform m_Foot_R = null;

    //move state
    private short m_MoveFlag;

    private int m_RotateCount;
    private int m_NextRotateCount;
    private int m_NextRotateCount2;

    private int[] m_NowRotate = { 0,0,0 };
    private int[] m_NowRotate2 = { 0, 0, 0 };


    // Start is called before the first frame update
    void Start()
    {
        //必要な値の取得
        m_View = GetComponent<PhotonView>();
        m_JoinHead = m_SetHead.transform.GetComponent<FixedJoint>();
        m_JoinHand_L = m_SetHand_L.transform.GetComponent<FixedJoint>();
        m_JoinHand_R = m_SetHand_R.transform.GetComponent<FixedJoint>();
        m_JoinFoot_L = m_SetFoot_L.transform.GetComponent<FixedJoint>();
        m_JoinFoot_R = m_SetFoot_R.transform.GetComponent<FixedJoint>();
        chill = transform.GetChild(0);
        StartPosition = transform.position;

        //数値の初期化
        StateInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_View.IsMine)
        {
            //起動開始
            if(m_MoveFlag == 1)
            {
                Vector3 mainMove = Vector3.zero;
                mainMove.y = 1.5f / StartCount;
                transform.position += mainMove;

                Vector3 chillMove = Vector3.zero;
                chillMove.z = Radius / StartCount;
                chill.position += chillMove;

                m_StartCount++;
                if (m_StartCount >= StartCount)
                {
                    m_MoveFlag = 2;
                    m_DirayCount = 120;
                    m_RotateCount = RotateEndCount;
                }

                Debug.Log("move1");
            }
            else if(m_MoveFlag == 2)
            {
                m_DirayCount--;
                if(m_DirayCount <= 0)
                {
                    m_MoveFlag = 3;
                }
                Debug.Log("move2");
            }
            else if(m_MoveFlag == 3)
            {
                if (m_RotateCount > 0)
                {
                    if (m_NextRotateCount <= 0)
                    {
                        SetNextRotate();
                    }
                    if (m_NextRotateCount2 <= 0)
                    {
                        SetNextRotate2();
                    }

                    transform.Rotate(m_NowRotate[0] / 10, m_NowRotate[1] / 10, m_NowRotate[2] / 10);
                    chill.Rotate(m_NowRotate2[0] / 10, m_NowRotate2[1] / 10, m_NowRotate2[2] / 10);
                    m_NextRotateCount--;
                    m_RotateCount--;
                }
                else
                {
                    m_MoveFlag = 4;
                    m_DirayCount = 120;
                    m_Player.transform.GetChild(1).GetComponent<RagdollManager>().SetRagdoll(true, 240);
                    m_JoinHead.connectedBody = null;
                    m_JoinHand_L.connectedBody = null;
                    m_JoinHand_R.connectedBody = null;
                    m_JoinFoot_L.connectedBody = null;
                    m_JoinFoot_R.connectedBody = null;
                }
                Debug.Log("move3");
            }
            else if (m_MoveFlag == 4)
            {
                m_DirayCount--;
                if (m_DirayCount <= 0)
                {
                    m_MoveFlag = 5;
                }
            }
            else if(m_MoveFlag == 5)
            {
                transform.position = StartPosition;
                transform.rotation = Quaternion.identity;
                chill.position = transform.position;
                chill.rotation = transform.rotation;
                StateInit();
            }
        }

        if(Input.GetKey(KeyCode.A))
        {
            m_MoveFlag = 1;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (m_MoveFlag == 0)
        {
            if (other.transform.tag == "Player")
            {
                if (other.transform.root.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    m_View.RequestOwnership();
                    m_Player = other.transform.root.gameObject;
                }
            }
        }
    }

    private void StateInit()
    {
        m_RotateCount = 0;
        m_MoveFlag = 0;
        m_StartCount = 0;
        m_NextRotateCount = 0;
        m_NextRotateCount2 = 0;
        m_StartCount = 0;
        m_DirayCount = 0;
        for (int i = 0; i < 3; i++)
        {
            m_NowRotate[i] = 0;
            m_NowRotate2[i] = 0;
        }
        m_Player = null;
        m_Hand_L = null;
        m_Hand_R = null;
        m_Foot_L = null;
        m_Foot_R = null;
        m_JoinHead.connectedBody = null;
        m_JoinHand_L.connectedBody = null;
        m_JoinHand_R.connectedBody = null;
        m_JoinFoot_L.connectedBody = null;
        m_JoinFoot_R.connectedBody = null;
    }

    private void RotateStart()
    {
        m_Player.transform.GetChild(1).GetComponent<RagdollManager>().SetRagdoll(true, 0);

        m_MoveFlag = 1;
        //プレイヤーの手と足を取得
        VRIK vrik = m_Player.transform.GetChild(1).GetComponent<VRIK>();
        m_Head = vrik.references.head;
        m_Hand_L = vrik.references.leftForearm;
        m_Hand_R = vrik.references.rightForearm;
        m_Foot_L = vrik.references.leftCalf;
        m_Foot_R = vrik.references.rightCalf;
        m_Head.SetPositionAndRotation(m_SetHead.position, m_SetHead.rotation);
        m_Hand_L.SetPositionAndRotation(m_SetHand_L.position, m_SetHand_L.rotation);
        m_Hand_R.SetPositionAndRotation(m_SetHand_R.position, m_SetHand_R.rotation);
        m_Foot_L.SetPositionAndRotation(m_SetFoot_L.position, m_SetFoot_L.rotation);
        m_Foot_R.SetPositionAndRotation(m_SetFoot_R.position, m_SetFoot_R.rotation);
        m_JoinHead.connectedBody = m_Head.GetComponent<Rigidbody>();
        m_JoinHand_L.connectedBody = m_Hand_L.GetComponent<Rigidbody>();
        m_JoinHand_R.connectedBody = m_Hand_R.GetComponent<Rigidbody>();
        m_JoinFoot_L.connectedBody = m_Foot_L.GetComponent<Rigidbody>();
        m_JoinFoot_R.connectedBody = m_Foot_R.GetComponent<Rigidbody>();

        m_StartCount = 0;
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

    private void SetNextRotate2()
    {
        m_NextRotateCount2 = Random.Range(MinRotateFrameCount, MaxRotateFrameCount);
        int dir = Random.Range(0, 2);

        int s = Random.Range(MinSpeed, MaxSpeed);
        if (m_NowRotate2[dir] > 0)
        {
            m_NowRotate2[dir] = -s;
        }
        else if (m_NowRotate2[dir] <= 0)
        {
            m_NowRotate2[dir] = s;
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

    int GetRotateCount() { return m_RotateCount; }
}

