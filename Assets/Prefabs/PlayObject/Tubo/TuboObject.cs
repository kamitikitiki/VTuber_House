using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboObject : MonoBehaviour
{
    public Transform m_Tubo;
    public Transform m_Body;
    public Transform m_Saki;

    public float MoveDis;

    //ハンマー移動座標
    public Transform m_TargetPos;

    //ツボとハンマーのジョイント
    private ConfigurableJoint m_TuboJoint;
    private ConfigurableJoint m_BodyJoint;

    //前回のtargetPosition
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
        m_BodyJoint.targetPosition = m_TargetPos.position - m_Saki.position;

        float velo = 0;
        if (m_TargetPos.position.y - m_Saki.position.y > 0)
            velo = 1.0f;
        else
            velo = -1.0f;

        Vector3 v_Velo = Vector3.zero;
        v_Velo.y = velo;
        m_BodyJoint.targetVelocity = v_Velo;
    }
}
