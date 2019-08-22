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
    public Transform Head;
    public Transform Neck;
    public Transform Spine;
    public Transform LeftFoot;
    public Transform RightFoot;
    public SeatState seatState;
    public bool OnSeatFlg;

    [SerializeField]//[HideInInspector]
    public SteeringWheel SteeringWheel;
}

//車のプレイヤー情報
public class CarPlayerInfo
{
    private Transform Neck;
    private Transform Spine;
    private Transform LeftFoot;
    private Transform RightFoot;
    private Transform LeftHand;
    private Transform RightHand;

    //仮
    //private Transform Head;
    //private FixedJoint JointHead;
    //private FixedJoint JointLeftHand;
    //private FixedJoint JointRightHand;
    //private FixedJoint JointLeftFoot;
    //private FixedJoint JointRightFoot;
    //

    public GameObject Player;
    public SeatState seatState;

    public CarPlayerInfo()
    {
        Neck = null;
        Spine = null;
        LeftFoot = null;
        RightFoot = null;
        LeftHand = null;
        RightHand = null;

        //Head = null;
        /*
        JointHead = null;
        JointLeftHand = null;
        JointRightHand = null;
        JointLeftFoot = null;
        JointRightFoot = null;
        */

        Player = null;
        seatState = SeatState.none;
    }

    public void SetState(GameObject player, SeatInfo seat)
    {
        //プレイヤーの各部位を車の座席にセットする

        Player = player;
        seatState = seat.seatState;

        //player.transform.GetChild(1).GetComponent<RagdollManager>().SetRagdoll(true, 0, false);

        VRIK vrik = player.transform.GetChild(1).GetComponent<VRIK>();

        //vrik.references.neck.SetPositionAndRotation(seat.Neck.position, seat.Neck.rotation);
        //vrik.references.spine.SetPositionAndRotation(seat.Spine.position, seat.Spine.rotation);
        //vrik.references.leftFoot.SetPositionAndRotation(seat.LeftFoot.position, seat.LeftFoot.rotation);
        //vrik.references.rightFoot.SetPositionAndRotation(seat.RightFoot.position, seat.RightFoot.rotation);

        
        Neck = vrik.references.neck;
        Spine = vrik.references.spine;
        LeftFoot = vrik.references.leftFoot;
        RightFoot = vrik.references.rightFoot;

        Neck.SetPositionAndRotation(seat.Neck.position, seat.Neck.rotation);
        Spine.SetPositionAndRotation(seat.Spine.position, seat.Spine.rotation);
        LeftFoot.SetPositionAndRotation(seat.LeftFoot.position, seat.LeftFoot.rotation);
        RightFoot.SetPositionAndRotation(seat.RightFoot.position, seat.RightFoot.rotation);
        
        //vrik.references.head.SetPositionAndRotation(seat.Head.position, seat.Head.rotation);
        //Head = vrik.references.head;
        //Head.SetPositionAndRotation(seat.Head.position, seat.Head.rotation);

        //JointHead.connectedBody = Head.GetComponent<Rigidbody>();
        //JointLeftFoot.connectedBody = LeftFoot.GetComponent<Rigidbody>();
        //JointRightFoot.connectedBody = RightFoot.GetComponent<Rigidbody>();

        //player.transform.GetChild(1).GetComponent<VRIK>().enabled = false;

        //ドライバーの時だけ
        if (seat.seatState == SeatState.driver)
        {
            //vrik.references.leftHand.SetPositionAndRotation(seat.SteeringWheel.SteeringWheel_Left.position, seat.SteeringWheel.SteeringWheel_Left.rotation);
            //vrik.references.rightHand.SetPositionAndRotation(seat.SteeringWheel.SteeringWheel_Right.position, seat.SteeringWheel.SteeringWheel_Right.rotation);
            
            LeftHand = vrik.references.leftHand;
            RightHand = vrik.references.rightHand;

            LeftHand.SetPositionAndRotation(seat.SteeringWheel.SteeringWheel_Left.position, seat.SteeringWheel.SteeringWheel_Left.rotation);
            RightHand.SetPositionAndRotation(seat.SteeringWheel.SteeringWheel_Right.position, seat.SteeringWheel.SteeringWheel_Right.rotation);

            //JointLeftHand.connectedBody = LeftHand.GetComponent<Rigidbody>();
            //JointRightHand.connectedBody = RightHand.GetComponent<Rigidbody>();
        }
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
    //private List<CarPlayerInfo> carplayerInfos;
    private CarPlayerInfo carplayerInfo;
    private int SeatCount;

    private void Start()
    {
        BreakingFlg = false;
        carplayerInfo = new CarPlayerInfo();

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
        //carplayerInfo.Player.transform.position = transform.position;
        //carplayerInfo.Player.transform.rotation = transform.rotation;

        /*
        if (SeatCount != seatInfos.Count)
        {
            //初期化


            SeatCount = seatInfos.Count;
        }
        */
    }

    private void OnTriggerStay(Collider other)
    {
        //コントローラーのキーを設定予定
        /*
        if (true)
        {
            if (other.transform.tag == "Player")
            {
                foreach (SeatInfo seatInfo in seatInfos)
                {
                    if (!seatInfo.OnSeatFlg && seatInfo.seatState == SeatState.driver)
                    {
                        //
                        CarPlayerSetting(other, seatInfo);

                        seatInfo.OnSeatFlg = true;
                    }
                }
            }
        }
        */
    }

    private void CarPlayerSetting(Collider other, SeatInfo seatInfo)
    {
        /*
        if (carplayerInfo.seatState == SeatState.none)
        {
            View.RequestOwnership();//なにこれ？
            carplayerInfo.SetState(other.transform.root.gameObject, seatInfo);

        }
        */

        /*
        foreach (CarPlayerInfo carplayerInfo in carplayerInfos)
        {
            if (carplayerInfo.seatState == SeatState.none)
            {
                View.RequestOwnership();//なにこれ？
                carplayerInfo.SetState(other.transform.root.gameObject, seatInfo.seatState);
            }
        }
        */
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
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        targetView.TransferOwnership(requestingPlayer);
    }
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        /*
        foreach (CarPlayerInfo carplayerInfo in carplayerInfos)
        {
            if (carplayerInfo.seatState == SeatState.none)
            {

            }
        }
        */
    }
}
