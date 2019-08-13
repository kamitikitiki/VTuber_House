using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using RootMotion.FinalIK;
using UnityEngine;

//車軸情報
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

//シートステータス
public enum SeatState { none, driver, front, backleft, backright}

//ハンドル
[System.Serializable]
public class SteeringWheel
{
    public Transform SteeringWheel_Left;
    public Transform SteeringWheel_Right;
}

//シート情報
[System.Serializable]
public class SeatInfo
{
    public Transform Neck;
    public Transform Spine;
    public Transform LeftFoot;
    public Transform RightFoot;
    public SeatState seatState;
    public bool OnSeatFlg;

    [SerializeField]//[HideInInspector]
    private SteeringWheel SteeringWheel;
}

//車のプレイヤー情報
public class CarPlayerInfo
{
    private GameObject Player;
    private Transform Neck;
    private Transform Spine;
    private Transform LeftFoot;
    private Transform RightFoot;

    public SeatState seatState;

    public CarPlayerInfo()
    {
        Player = null;
        Neck = null;
        Spine = null;
        LeftFoot = null;
        RightFoot = null;

        seatState = SeatState.none;
    }

    public void SetState(GameObject player, SeatState seat)
    {
        Player = player;
        seatState = seat;

        VRIK vrik = player.transform.GetChild(1).GetComponent<VRIK>();
        Neck = vrik.references.neck;
        Spine = vrik.references.spine;
        LeftFoot = vrik.references.leftFoot;
        RightFoot = vrik.references.rightFoot;
    }

    //public VRIK 
}

public class CarController : MonoBehaviourPunCallbacks//, IPunOwnershipCallbacks
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
    private PhotonView View;
    private List<CarPlayerInfo> carplayerInfos;
    private int SeatCount;

    private void Start()
    {
        BreakingFlg = false;

        rb = GetComponent<Rigidbody>();
        View = GetComponent<PhotonView>();
        SeatCount = seatInfos.Count;
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

    public void Update()
    {
        if(SeatCount != seatInfos.Count)
        {
            //初期化


            SeatCount = seatInfos.Count;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //コントローラーのキーを設定予定
        if (true)
        {
            if (other.transform.tag == "Player")
            {
                foreach (SeatInfo seatInfo in seatInfos)
                {
                    if (!seatInfo.OnSeatFlg)
                    {
                        //
                        foreach (CarPlayerInfo carplayerInfo in carplayerInfos)
                        {
                            if (carplayerInfo.seatState == SeatState.none)
                            {
                                View.RequestOwnership();//なにこれ？
                                carplayerInfo.SetState(other.transform.root.gameObject, seatInfo.seatState);
                            }
                        }

                        seatInfo.OnSeatFlg = true;
                    }
                }
            }
        }
    }

    //WheelColliderのTransformをタイヤ（描画ボーン）のTransformに適用する
    public void ApplyLocalPositionToVisuals(WheelCollider wc, Transform wt)
    {
        wc.GetWorldPose(out Vector3 position, out Quaternion rotation);

        wt.transform.position = position;
        wt.transform.rotation = rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }




    //いるかわからん
    private void SetState(CarPlayerInfo carplayerInfo)
    {
        //ラグドールの設定

        //プレイヤーの各部位を取得
         
    }

    //わからん
    //public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    //public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
}
