using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class foot_speed : UndoObjectInterface
{                         
    public string ButtonName;

    // Start is called before the first frame update


    public void setSpeed(string Name){
        if(Name == "higt"){
            ret = 3;
        } 
        if(Name == "mid"){
            ret = 2;
        }
        if(Name == "low"){
            ret = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           setSpeed(ButtonName);
        }
    }

}
