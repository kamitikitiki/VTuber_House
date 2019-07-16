using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboObject : MonoBehaviour
{
    public Transform m_Tubo;
    public Transform m_Body;
    public Transform m_Saki;

    //
    public Transform m_TargetPos;

    private ConfigurableJoint m_TuboJoint;
    private ConfigurableJoint m_BodyJoint;

    // Start is called before the first frame update
    void Start()
    {
        m_TuboJoint = m_Tubo.GetComponent<ConfigurableJoint>();
        m_BodyJoint = m_Body.GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        m_BodyJoint.targetPosition = m_TargetPos.position - m_Saki.position;
    }
}
