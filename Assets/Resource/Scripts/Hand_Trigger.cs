using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand_Trigger : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("ok");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "HaveItem")
        {
        }
    }
}