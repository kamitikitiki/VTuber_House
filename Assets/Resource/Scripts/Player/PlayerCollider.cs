using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollider : MonoBehaviour
{
    private bool m_IsInitialize = false;

    private Transform m_RootObject;
    private RagdollManager m_Ragdoll;
    private PhotonView m_View;

    private void MyStateInit()
    {
        m_RootObject = transform.root.transform;
        m_View = m_RootObject.GetComponent<PhotonView>();
        m_Ragdoll = m_RootObject.GetChild(1).GetComponent<RagdollManager>();
        m_IsInitialize = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        MyStateInit();
    }

    public void OnEnable()
    {
        if(m_IsInitialize == false)
            MyStateInit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(m_View.IsMine)
        {
            if (other.tag == "Death")
            {
                if(m_Ragdoll.IsRagdoll() == false)
                {
                    //運動企画用の設定
                    m_Ragdoll.SetRagdoll(true, 1200, false);
                }
            }
        }
    }
}
