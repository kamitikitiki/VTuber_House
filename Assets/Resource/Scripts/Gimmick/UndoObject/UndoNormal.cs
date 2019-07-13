using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//UndoObjectInterfaceを継承してオブジェクト作成
public class UndoNormal : UndoObjectInterface
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        //PhotonView取得
        m_PhotonView = GetComponent<PhotonView>();

        //初期化処理はここへ
    }

    // Update is called once per frame
    void Update()
    {

        //オブジェクトの移動
        transform.Translate(m_MoveSpped, 0, 0);
    }
}
