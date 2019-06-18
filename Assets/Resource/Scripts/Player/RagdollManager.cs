using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using RootMotion.FinalIK;

public class RagdollManager : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Head;
    public GameObject[] RagdollBone;

    public void OnRagdoll()
    {
        Debug.Log("on");
        Head.GetComponent<Rigidbody>().useGravity = true;
        Head.GetComponent<Rigidbody>().isKinematic = false;
        Head.GetComponent<Collider>().enabled = true;
        for(int i = 0; i < RagdollBone.Length; i++)
        {
            RagdollBone[i].GetComponent<Rigidbody>().useGravity = true;
            RagdollBone[i].GetComponent<Rigidbody>().isKinematic = false;
            RagdollBone[i].GetComponent<Collider>().enabled = true;
        }

        GetComponent<VRIK>().enabled = false;
        
        
    }

    public void OffRagdoll()
    {
        Head.GetComponent<Rigidbody>().useGravity = false;
        Head.GetComponent<Rigidbody>().isKinematic = true;
        Head.GetComponent<Collider>().enabled = false;
        for (int i = 0; i < RagdollBone.Length; i++)
        {
            RagdollBone[i].GetComponent<Rigidbody>().useGravity = false;
            RagdollBone[i].GetComponent<Rigidbody>().isKinematic = true;
            RagdollBone[i].GetComponent<Collider>().enabled = false;
        }

        GetComponent<VRIK>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if(Head.GetComponent<Rigidbody>().isKinematic == true)
            {
                OnRagdoll();
            }
        }

        if (Head.GetComponent<Rigidbody>().isKinematic == false)
        {
            Camera.gameObject.transform.position = Head.gameObject.transform.position;
        }

    }
}
