using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harituke : MonoBehaviour
{
    public float add_speed;

    //生成範囲値
    public int MaxSpeed;
    public int MinSpeed;
    public int MaxRotateFrameCount;
    public int MinRotateFrameCount;
    public int RotateEndCount;

    private int m_RotateCount;
    private int m_NextRotateCount;

    private int[] m_NowRotate = { 0,0,0 };


    // Start is called before the first frame update
    void Start()
    {
        m_RotateCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //回転中
        if(m_RotateCount > 0)
        {
            if(m_NextRotateCount <= 0)
            {
                SetNextRotate();
            }

            transform.Rotate(m_NowRotate[0], m_NowRotate[1], m_NowRotate[2]);
            m_NextRotateCount--;
            //m_RotateCount--;
        }
    }

    private void RotateStart()
    {
        m_RotateCount = RotateEndCount;
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
}

