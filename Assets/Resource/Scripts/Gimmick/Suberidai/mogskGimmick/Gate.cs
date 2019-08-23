using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public List<GameObject> Open_obs;
    public List<GameObject> Close_obs;

    public GameObject LeftGate;
    public GameObject RightGate;

    public float speed;
    public float movemax;

    public bool Gate_flag;
    public float move;

    private void Start()
    {
        move = 0;
        Gate_flag = false;
    }

    public void Update()
    {
        if(Gate_flag)
        {
            if(move <= movemax)
            {
                Vector3 lgp = LeftGate.transform.position;
                lgp.x += speed;
                LeftGate.transform.position = lgp;

                Vector3 rgp = RightGate.transform.position;
                rgp.x -= speed;
                RightGate.transform.position = rgp;

                move += speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject open_ob in Open_obs)
        {
            open_ob.SetActive(true);
        }
        foreach (GameObject close_ob in Close_obs)
        {
            close_ob.SetActive(false);
        }
        Gate_flag = true;

    }
}
