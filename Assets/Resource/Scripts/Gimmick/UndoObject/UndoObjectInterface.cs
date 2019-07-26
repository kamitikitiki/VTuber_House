using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UndoObjectInterface : MonoBehaviour
{
    protected float m_MoveSpped = 0;
    protected float m_Height = 0;
    protected PhotonView m_PhotonView;
    protected bool ret = true;

    public void SetMoveSpeed(float speed)
    {
        m_MoveSpped = speed;
    }
    public void SetMoveheight(float height){
        m_Height = height;
    }
}
