using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UndoObjectInterface : MonoBehaviour
{
    protected float m_MoveSpped = 0;
    protected PhotonView m_PhotonView;

    public void SetMoveSpeed(float speed)
    {
        m_MoveSpped = speed;
    }
}
