using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//UndoObjectInterfaceを継承してオブジェクト作成
public class Baund : UndoObjectInterface
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

        //このif分は自分の管理しているオブジェクトかどうかチェック
        //自分の生成したPhotonViewオブジェクトの所持者は自分
        //自分の所持オブジェクトだけが座標の移動やtransformを変更できる
        if(m_PhotonView.IsMine)
        {
            transform.Translate(m_MoveSpped,0, 0);
        }
    }
}
