using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCamera : ItemInterface
{

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override int OnButton()
    {
        GameObject view = transform.GetChild(1).gameObject;
        Quaternion rotate = view.transform.rotation;
        if (rotate.y >= 250)
        {
            view.SetActive(false);
        }
        else if (view.activeInHierarchy == false)
        {
            view.SetActive(true);
            Vector3 setRot = Vector3.zero;
            setRot.y = 180;
            view.transform.eulerAngles = setRot;
        }
        else
        {
            view.transform.Rotate(0, rotate.y += 180, 0);
        }
        
        view.transform.rotation = rotate;
        return 1;
    }
}
