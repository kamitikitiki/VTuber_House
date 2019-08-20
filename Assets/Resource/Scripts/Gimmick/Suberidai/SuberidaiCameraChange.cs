using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuberidaiCameraChange : MonoBehaviour
{
    public Transform camera;
    public Transform camera_pos;

    public bool CameraLook;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sled")
        {
            camera.SetPositionAndRotation(camera_pos.position, camera_pos.rotation);
            camera.GetComponent<SledFollow>().changeFollowFlag(CameraLook);
        }
    }
}
