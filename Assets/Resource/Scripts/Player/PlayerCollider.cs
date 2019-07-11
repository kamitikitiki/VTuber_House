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
        MyStateInit();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("test1");
        if(m_View.IsMine)
        {
            Debug.Log("test2");
            if (other.tag == "Death")
            {
                Debug.Log("test3");
                m_Ragdoll.SetRagdoll(true, 300);
            }
        }
    }
}
