using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider WheelCollider_Left;
    public WheelCollider WheelCollider_Right;
    public Transform WheelTransform_Left;
    public Transform WheelTransform_Right;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;                    //トルクの最大数
    public float maxSteeringAngle;                  //ハンドルの最大回転角度
    public float Speed;                             //加速度
    public float Breaking;                          //ブレーキ値

    public float motor;
    public float steering;
    public bool BreakingFlg;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        BreakingFlg = false;
    }

    //WheelColliderのTransformをタイヤ（描画ボーン）のTransformに適用する
    public void ApplyLocalPositionToVisuals(WheelCollider wc,Transform wt)
    {
        wc.GetWorldPose(out Vector3 position, out Quaternion rotation);

        wt.transform.position = position;
        wt.transform.rotation = rotation * Quaternion.Euler(0.0f,180.0f,0.0f);
    }

    public void FixedUpdate()
    {
        //float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        motor = maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        BreakingFlg = Input.GetKey(KeyCode.Space);

        rb.AddForce(new Vector3(0.0f, 0.0f, motor) * Speed, ForceMode.Impulse);
        //rb.AddTorque(transform.up * motor * 2, ForceMode.Impulse);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            //ハンドルの角度を渡す
            if (axleInfo.steering)
            {
                axleInfo.WheelCollider_Left.steerAngle = steering;
                axleInfo.WheelCollider_Right.steerAngle = steering;
            }
            //モータのトルク（回転数）を渡す
            if (axleInfo.motor)
            {
                axleInfo.WheelCollider_Left.motorTorque = motor;
                axleInfo.WheelCollider_Right.motorTorque = motor;
            }
            //ブレーキを渡す
            if(BreakingFlg)
            {
                axleInfo.WheelCollider_Left.brakeTorque = Breaking;
                axleInfo.WheelCollider_Right.brakeTorque = Breaking;
            }
            else
            {
                axleInfo.WheelCollider_Left.brakeTorque = 0;
                axleInfo.WheelCollider_Right.brakeTorque = 0;
            }

            ApplyLocalPositionToVisuals(axleInfo.WheelCollider_Left,axleInfo.WheelTransform_Left);
            ApplyLocalPositionToVisuals(axleInfo.WheelCollider_Right,axleInfo.WheelTransform_Right);
        }
    }
}
