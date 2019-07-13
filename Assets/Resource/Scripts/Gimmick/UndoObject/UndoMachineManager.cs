using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UndoMachineManager : MonoBehaviour
{
    //オブジェクトの生成位置
    public Transform CREATEPOSITION_PLAYER1;
    public Transform CREATEPOSITION_PLAYER2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateUndoObject("UndoNormal");
        }
    }

    //外部呼出しによるオブジェクト生成
    //createObjectName : 生成するオブジェクトの名前、Resources/UndoPrefabsに入ってるオブジェクトの名前
    public void CreateUndoObject(string createObjectName)
    {
        string createVrName = "UndoPrefabs/" + createObjectName;

        //この関数でネットワーク同期している全員にオブジェクト生成する
        GameObject obj1 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER1.position, CREATEPOSITION_PLAYER1.rotation);
        GameObject obj2 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER2.position, CREATEPOSITION_PLAYER2.rotation);

        //オブジェクト速度設定
        obj1.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.01f);
        obj2.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.01f);
    }


    //
}
