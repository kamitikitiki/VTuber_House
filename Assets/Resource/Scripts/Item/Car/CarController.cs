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
    public float speed;

    private Rigidbody rb;

    //public float SteeringAngle;
    //public float rotateY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //対応する視覚的なホイールを見つけます
    //Transformを正しく適用します。
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        /*
        if(collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);
        */


        //Vector3 position;
        //Quaternion rotation;
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        //visualWheel.transform.position = position;
        //visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        //float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        

        motor = maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(0.0f, 0.0f, motor) * speed, ForceMode.Impulse);
        //rb.AddTorque(transform.up * motor * 2, ForceMode.Impulse);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if(axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;



                //タイヤの回転
                /*
                foreach (GameObject WheelModelPosition in axleInfo.WheelModelPositions)
                {
                    //現在の回転角度を0～360から-180～180に変換
                    rotateY = (transform.eulerAngles.y > 180) ?
                        transform.eulerAngles.y - 360 : transform.eulerAngles.y;

                    //角度制限
                    float angleY; //= Mathf.Clamp(steering,-maxSteeringAngle,maxSteeringAngle);
                    angleY = steering + rotateY;

                    angleY = (angleY < 0) ? angleY + 360 : angleY;

                    WheelModelPosition.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angleY, transform.eulerAngles.z);
                }
                */
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
