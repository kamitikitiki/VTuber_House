using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemInterface : MonoBehaviourPunCallbacks, IPunObservable
{

    ////////////////////////////////////////////////
    //継承先でも同期される変数

    //--アイテムが持たれているかどうかのフラグ true 持つ　false 持ってない
    private bool f_Have = false;
    virtual protected void IsChangeHave() { }
    public bool IsHave() { return f_Have; }
    public bool SetHave()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            f_Have = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            return true;
        }
        else
        {
            GetComponent<PhotonView>().RequestOwnership();
            return false;
        }
    }
    public void SetRelease()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            f_Have = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    //--パッドボタンを押したときの変数
    protected int f_Button;
    virtual protected void IsChangeButton() { }

    virtual public void Init()
    {
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
        if (stream.IsWriting)
        {
            stream.SendNext(f_Have);
            stream.SendNext(f_Button);
        }
        else
        {
            bool have = (bool)stream.ReceiveNext();
            int button = (int)stream.ReceiveNext();

            if(f_Have != have) { f_Have = have; IsChangeHave(); }
            if (f_Button != button) { f_Button = button; IsChangeButton(); }

            if (f_Have == false && gameObject.GetComponent<Rigidbody>().useGravity == false)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            else if(gameObject.GetComponent<Rigidbody>().useGravity == true)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
}
