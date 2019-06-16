using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RagdollManager : MonoBehaviour
{
    public GameObject root = null;
    public GameObject root_rag = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if(root.active == true)
            {
                root.active = false;
                root_rag.active = true;
            }
            else
            {
                root.active = true;
                root_rag.active = false;
            }
        }
    }
}
