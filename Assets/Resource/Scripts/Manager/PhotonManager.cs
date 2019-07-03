using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    string PlayerName;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if(canvas != null)
        {
            PlayerName = canvas.gameObject.transform.GetChild(0).GetComponent<InputField>().text;
            Destroy(canvas);
        }
        else
        {
            PlayerName = "VR_Player";
        }

        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("master");
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // マッチングが成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("join");
        ObjectInstanceCreate();
    }

    public void ObjectInstanceCreate()
    {
        if (PlayerName.Length != 0 && PlayerName != "camera")
        {
            Vector3 pos = Vector3.zero;
            Quaternion qua = new Quaternion(0, 0, 0, 1);
            string createVrName = "PunPrefabs/" + PlayerName;
            PhotonNetwork.Instantiate(createVrName, pos, qua);
        }
    }
}
