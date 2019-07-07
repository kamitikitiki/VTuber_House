using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_2disp : MonoBehaviour
{
    private int m_Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        m_Count = 180;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_Count < 0) { }
        else if (m_Count == 0)
        {
            if (GetComponent<Camera>().enabled == false)
            {
                GetComponent<Camera>().enabled = true;
            }
            m_Count--;
        }
        else if(m_Count >= 1)
        {
            m_Count--;
        }
    }
}
