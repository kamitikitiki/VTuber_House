using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand_Trigger : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;
    private GameObject HaveItem = null;
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
                ItemInterface src = HaveItem.GetComponent<ItemInterface>();
                src.SetRelease();
                Joint.connectedBody = null;
                HaveItem = null;
                releaseItem.velocity = SteamVR_Actions.default_Pose.GetVelocity(HandType);
                releaseItem.angularVelocity = SteamVR_Actions.default_Pose.GetAngularVelocity(HandType);
            }

            if (HaveItem != null)
            {
                if(SteamVR_Actions.default_Teleport.GetStateDown(HandType))
                {
                    ItemInterface src = HaveItem.GetComponent<ItemInterface>();
                    src.OnButton();
                }
            }
        }

        if(HandType == SteamVR_Input_Sources.LeftHand)
        {
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HaveItem")
        {
            if (SteamVR_Actions.default_GrabPinch.GetStateDown(HandType))
            {
                //アイテムを持った時の処理
                ItemInterface src = other.GetComponent<ItemInterface>();
                if(src.IsHave() == false)
                {
                    if(src.SetHave())
                    {
                        HaveItem = other.gameObject;
                        Joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
                    }
                }
            }
        }
    }
}