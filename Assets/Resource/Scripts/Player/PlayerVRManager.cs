using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;
using RootMotion.FinalIK;

public class PlayerVRManager : MonoBehaviourPunCallbacks
{

    public string ModelName;
    GameObject playerModel = null;

    public override void OnEnable()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            //左手取得とスクリプトOn
            transform.GetChild(0).GetComponent<SteamVR_Behaviour_Pose>().enabled = true;
            //右手取得とスクリプトOn
            transform.GetChild(1).GetComponent<SteamVR_Behaviour_Pose>().enabled = true;
            //頭の取得とスクリプトOn
            transform.GetChild(2).GetComponent<Camera>().enabled = true;

            //モデルオブジェクト作成
            Vector3 pos = Vector3.zero;
            Quaternion qua = new Quaternion(0, 0, 0, 1);
            playerModel = PhotonNetwork.Instantiate("PunPrefabs/" + ModelName, pos, qua);
        }

        Invoke("PlayerModelSettings", 3.0f);
    }

    private void PlayerModelSettings()
    {
        if(playerModel == null)
        {
            playerModel = transform.Find(ModelName + "(Clone)").gameObject;
        }

        //モデルに設定するオブジェクト取得
        GameObject hand_L = transform.GetChild(0).gameObject;
        GameObject hand_R = transform.GetChild(1).gameObject;
        GameObject camera = transform.GetChild(2).gameObject;

        //ラグドールにカメラ設定
        playerModel.GetComponent<RagdollManager>().Camera = camera;

        //IK設定
        VRIK ik = playerModel.GetComponent<VRIK>();
        ik.solver.spine.headTarget = camera.transform.GetChild(0);
        ik.solver.leftArm.target = hand_L.transform.GetChild(0);
        ik.solver.rightArm.target = hand_R.transform.GetChild(0);
    }
}
