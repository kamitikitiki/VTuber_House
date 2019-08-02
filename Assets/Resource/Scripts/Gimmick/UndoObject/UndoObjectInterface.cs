using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UndoObjectInterface : MonoBehaviour
{
    protected float m_MoveSpped = 0;
    protected float m_Height = 0;
    protected PhotonView m_PhotonView;
    protected int speed_value;
    protected int ret = 2;

    public void SetMoveSpeed(int speed)
    {
        if(speed == 1){
            m_MoveSpped = 0.03f;
        }else if(speed == 2){
            m_MoveSpped = 0.05f;
        }else if(speed == 3){
            m_MoveSpped = 0.07f;
        }
    }
    public void SetMoveheight(float height){
        m_Height = height;
    }
}
