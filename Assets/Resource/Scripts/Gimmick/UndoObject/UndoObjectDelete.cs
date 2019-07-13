using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoObjectDelete : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Death")
        {
            //自分自身のオブジェクトなら削除（自分の削除することで他も削除される)
            if (other.GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                Photon.Pun.PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
