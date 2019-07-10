using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCamera : ItemInterface
{

    // Start is called before the first frame update
    void Start()
    {
        Init();
        f_Button = 1;
    }

    public override int OnButton()
    {
        f_Button = (f_Button + 1) % 2;

        DisplayChange();

        return 1;
    }

    override protected void IsChangeButton()
    {
        DisplayChange();
    }

    //ボタンの変更に応じたディスプレイ回転の処理
    void DisplayChange()
    {
        GameObject view = transform.GetChild(2).gameObject;
        transform.GetChild(2).transform.GetComponent<Photon.Pun.PhotonView>().RequestOwnership();
        Vector3 rotate = view.transform.eulerAngles;

        switch (f_Button)
        {
            case 0:
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
