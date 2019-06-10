using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand_Trigger : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;
    private FixedJoint Joint = null;

    // Start is called before the first frame update
    void Start()
    {
        Joint = gameObject.GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Joint.connectedBody != null)
        {
            SteamVR_TrackedObject tc = gameObject.GetComponent<SteamVR_TrackedObject>();
            
            if (SteamVR_Actions.default_GrabPinch.GetStateUp(HandType))
            {
                Rigidbody hand = GetComponent<Rigidbody>();
                Rigidbody releaseItem = Joint.connectedBody;
                Joint.connectedBody = null;
                releaseItem.velocity = SteamVR_Actions.default_Pose.GetVelocity(HandType);
                releaseItem.angularVelocity = SteamVR_Actions.default_Pose.GetAngularVelocity(HandType);
                //releaseItem.maxAngularVelocity = 
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HaveItem")
        {
            if (SteamVR_Actions.default_GrabPinch.GetStateDown(HandType))
            {
                Debug.Log("grab");
                Joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
            }
        }
    }
}