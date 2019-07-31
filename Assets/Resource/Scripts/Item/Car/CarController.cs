using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    public List<GameObject> WheelModelPositions;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float motor;
    public float steering;
    public float SteeringAngle;

    //対応する視覚的なホイールを見つけます
    //Transformを正しく適用します。
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if(collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        //float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        SteeringAngle = Input.GetAxis("Horizontal");

        motor = maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * SteeringAngle;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if(axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
                //タイヤの回転
                foreach (GameObject WheelModelPosition in axleInfo.WheelModelPositions)
                {
                    var AngleTarget = Quaternion.Euler(new Vector3(0, steering, 0));
                    var Now_rot = WheelModelPosition.transform.rotation;
                    if(Quaternion.Angle(Now_rot,AngleTarget) <= 1)
                    {
                        WheelModelPosition.transform.rotation = AngleTarget;
                    }
                    else
                    {
                        //WheelModelPosition.transform.RotateAround(WheelModelPosition.transform.position,)
                    }
                }
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}
