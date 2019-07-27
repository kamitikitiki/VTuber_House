﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboObject : MonoBehaviour
{
    public Transform m_Tubo;
    public Transform m_Body;
    public Transform m_Saki;

    //ハンマー移動座標
    public Transform m_TargetPos;

    //ツボとハンマーのジョイント
    private ConfigurableJoint m_TuboJoint;
    private ConfigurableJoint m_BodyJoint;

    //ハンマーの動く遊び
    //前回のtargetPosition
    public float MoveDis;
    private Vector3 m_BeforeTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        m_TuboJoint = m_Tubo.GetComponent<ConfigurableJoint>();
        m_BodyJoint = m_Body.GetComponent<ConfigurableJoint>();

        m_BeforeTargetPos = m_TargetPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        //前回動かした座標との距離
        float move_len = Vector3.Distance(m_BeforeTargetPos, m_TargetPos.position);
        //ハンマーとターゲットの距離
        float interval_len = Vector3.Distance(m_Saki.position, m_TargetPos.position);

        //ハンマーの速度
        float velo = 0;
        Vector3 v_Velo = Vector3.zero;

        //ターゲットが一定以上動いたら
        if ( interval_len >= 0.1)
        {
            //２点のどっちが近いか計算
            //ボディとハンマーの距離
            float saki_len = Vector3.Distance(m_Saki.position, m_Body.position);
            //ボディとターゲットの距離
            float target_len = Vector3.Distance(m_TargetPos.position, m_Body.position);

            m_BodyJoint.targetPosition = m_TargetPos.position - m_Saki.position;

            //ハンマーの速度
            //float velo = 0;

            if (target_len > saki_len)
                velo = 1.0f;
            else
                velo = -1.0f;

            if(Input.GetKey(KeyCode.Space))
            {
                velo = 0;
            }

            v_Velo.y = velo;

            m_BeforeTargetPos = m_TargetPos.position;
        }

        m_BodyJoint.targetVelocity = v_Velo;
        
    }
}