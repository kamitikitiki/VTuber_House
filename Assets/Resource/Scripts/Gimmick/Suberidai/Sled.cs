using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sled : MonoBehaviour
{
    public bool IsDebug;
    private float m_MoveLength;
    private Vector3 m_LatePos;

    private Transform m_RidePlayer;

    // Start is called before the first frame update
    void Start()
    {
        m_MoveLength = 0;
        m_LatePos = transform.position;
        m_RidePlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDebug == true)
        {
            Vector3 moveLen = m_LatePos - transform.position;
            m_MoveLength += Mathf.Abs(moveLen.x);
            m_MoveLength += Mathf.Abs(moveLen.y);
            m_MoveLength += Mathf.Abs(moveLen.z);
            m_LatePos = transform.position;
            Debug.Log(m_MoveLength + "m");
        }

        if(m_RidePlayer != null)
        {
            m_RidePlayer.position = transform.position;
            m_RidePlayer.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_RidePlayer == null)
        {
            if (other.tag == "Player")
            {
                if (other.transform.root.root.GetComponent<PhotonView>().IsMine)
                {
                    m_RidePlayer = other.transform.root.root;
                }
            }
        }
    }
}
