using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemInterface : MonoBehaviourPunCallbacks, IPunObservable
{

    ////////////////////////////////////////////////
    //継承先でも同期される変数

    //--アイテムが持たれているかどうかのフラグ true 持つ　false 持ってない
    public bool f_Have;
    public bool IsHave() { return f_Have; }
    public void SetHave()
    {
        f_Have = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
    public void SetRelease()
    {
        f_Have = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;

    }

    //--ボタンを押したときの変数
    protected int f_Button;

    virtual public void Init()
    {
        f_Have = false;
    }

    virtual public int OnButton()
    {
        return 1;
    }

    void Update()
    {

    }

    // データを送受信するメソッド
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        stream.SendNext(f_Have);
        stream.SendNext(f_Button);

        if(stream.IsReading == true)
        {
            f_Have = (bool)stream.ReceiveNext();
            f_Button = (int)stream.ReceiveNext();
        }
        */

        if(f_Have == false)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
