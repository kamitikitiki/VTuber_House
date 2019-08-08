using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//車軸
[System.Serializable]
public class AxleInfo
{
    public WheelCollider WheelCollider_Left;        //左ホイールコライダー
    public WheelCollider WheelCollider_Right;       //右ホイールコライダー
    public Transform WheelTransform_Left;           //左タイヤボーン
    public Transform WheelTransform_Right;          //右タイヤボーン
    public bool motor;                              //モーターにトルクを加えるフラグ
    public bool steering;                           //ハンドルの角度を加えるフラグ
}

//ドライバーステータス
public enum DriverState { none, driver }

[System.Serializable]
public class SteeringWheel
{
    public Transform SteeringWheel_Left;
    public Transform SteeringWheel_Right;
}

//シート
[System.Serializable]
public class SeatInfo
{
    public Transform Neck;
    public Transform Spine;
    public Transform LeftReg;
    public Transform RightReg;
    public DriverState driver;
    //public SteeringWheel SteeringWheel;
}

public class CarController : MonoBehaviour
{

    public List<AxleInfo> axleInfos;
    public List<SeatInfo> seatInfos;
    public float maxMotorTorque;                    //トルクの最大数
    public float maxSteeringAngle;                  //ハンドルの最大回転角度
    public float Speed;                             //加速度
    public float Breaking;                          //ブレーキ値

    //public float motor;
    //public float steering;
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
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //motor = maxMotorTorque * Input.GetAxis("Vertical");
        //steering = maxSteeringAngle * Input.GetAxis("Horizontal");
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
            //ブレーキをかける
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
