using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemInterface : MonoBehaviourPunCallbacks
{

    ////////////////////////////////////////////////
    //継承先でも同期される変数

    //--アイテムが持たれているかどうかのフラグ true 持つ　false 持ってない
    private bool f_Have = false;
    virtual protected void IsChangeHave() { }
    public bool IsHave() { return f_Have; }
    public bool SetHave()
    {

            f_Have = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

        return true;

    }
    public void SetRelease()
    {
            f_Have = false;
           gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
}
