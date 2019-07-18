using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UndoMachineManager : MonoBehaviour
{
    //オブジェクトの生成位置
    public Transform CREATEPOSITION_PLAYER1;
    public Transform CREATEPOSITION_PLAYER2;
    public Transform CREATEPOSITION_PLAYER3;
    public Transform CREATEPOSITION_PLAYER4;
    public Transform CREATEPOSITION_PLAYER5;
    public Transform CREATEPOSITION_PLAYER6;
    public Transform CREATEPOSITION_PLAYER7;
    public Transform CREATEPOSITION_PLAYER8;
    public Transform CREATEPOSITION_PLAYER9;
    public Transform CREATEPOSITION_PLAYER10;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //外部呼出しによるオブジェクト生成
    //createObjectName : 生成するオブジェクトの名前、Resources/UndoPrefabsに入ってるオブジェクトの名前
    //外部でこのスクリプトを呼び出すといい
    public void CreateUndoObject(string createObjectName,string ButtonName)
    {
        string createVrName = "UndoPrefabs/" + createObjectName;


        

        //この関数でネットワーク同期している全員にオブジェクト生成する
        if(ButtonName == "kill"){
            GameObject obj1 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER1.position, CREATEPOSITION_PLAYER1.rotation);
            GameObject obj2 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER2.position, CREATEPOSITION_PLAYER2.rotation);
            obj1.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
            obj2.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
        }
        //TOP
        if(ButtonName == "Top"){
            GameObject obj3 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER3.position, CREATEPOSITION_PLAYER3.rotation);
            GameObject obj4 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER4.position, CREATEPOSITION_PLAYER4.rotation);
            obj3.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
            obj4.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
        }
        //UNDER
        if(ButtonName == "Under"){
            GameObject obj5 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER5.position, CREATEPOSITION_PLAYER5.rotation);
            GameObject obj6 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER6.position, CREATEPOSITION_PLAYER6.rotation);
            obj5.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
            obj6.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
        }
        //
        if(ButtonName == "left"){
            GameObject obj7 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER7.position, CREATEPOSITION_PLAYER7.rotation);
            GameObject obj8 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER8.position, CREATEPOSITION_PLAYER8.rotation);
            obj7.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
            obj8.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
        }
        if(ButtonName == "right"){
            GameObject obj9 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER9.position, CREATEPOSITION_PLAYER9.rotation);
            GameObject obj10 = PhotonNetwork.Instantiate(createVrName, CREATEPOSITION_PLAYER10.position, CREATEPOSITION_PLAYER10.rotation);
            obj9.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
            obj10.GetComponent<UndoObjectInterface>().SetMoveSpeed(0.05f);
        }

        //オブジェクト速度設定

    }


    //ネットワークオブジェクトの作り方Unityの方のUndoNormal参考にしてね
    //ネットワークでみんなに生成、同期をしたいオブジェクトはPhotonViewスクリプトをつける
    //座標の移動を同期させたい場合はPhotonTransformViewをコンポーネントに追加、さらにそれをPhotonViewのObservedComponentsに追加する
    //PhotonTransformViewのSynchronize OptionsのPositionとRotationにチェックを入れる
    //基本的な移動をしたいだけならUndoNormalをコンポーネントに追加する
    //プレイヤーが当たったら倒れるオブジェクトはtagをDeathに変更する
    //それ以外の動きをさせたいならUndoObjectInterfaceを継承した新しいスクリプトを作成する
}
