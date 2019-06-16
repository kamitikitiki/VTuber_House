using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCamera : ItemInterface
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int OnButton()
    {

        GameObject view = transform.GetChild(1).gameObject;
        Quaternion rotate = view.transform.rotation;
        if (rotate.y >= 250)
        {
            view.SetActive(false);
        }
        else if (view.active == false)
        {
            view.SetActive(true);
            view.transform.Rotate(rotate.x, 90, rotate.z);
        }
        else
        {
            view.transform.Rotate(rotate.x, rotate.y += 180, rotate.z);
        }
        
        view.transform.rotation = rotate;
        return 1;
    }
}
