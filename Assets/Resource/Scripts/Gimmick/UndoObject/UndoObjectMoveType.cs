using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UndoObjectMoveType : UndoObjectInterface
{
    private int PANEL_SIZEX = 3;
    private int PANEL_SIZEY = 3;

    public GameObject block_cube;

    private Transform[,] blocks = new Transform[10,10];

    // Start is called before the first frame update
    private void OnEnable()
    {
        //PhotonView取得
        m_PhotonView = GetComponent<PhotonView>();

        //初期化処理はここへ
        //ブロックを生成する
        for(int x = 0; x < 10; x++)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {

        //オブジェクトの移動
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        //このif分は自分の管理しているオブジェクトかどうかチェック
        //自分の生成したPhotonViewオブジェクトの所持者は自分
        //自分の所持オブジェクトだけが座標の移動やtransformを変更できる
        if (m_PhotonView.IsMine)
        {
            transform.Translate(m_MoveSpped, 0, 0);
        }
    }
}