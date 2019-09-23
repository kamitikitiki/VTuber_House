using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{

    public Transform PlayerTransform;
    public Vector3 StartPosition; 

    public Vector3 speed;

    private bool m_Shoot;

    // Start is called before the first frame update
    void Start()
    {
        m_Shoot = false;
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_Shoot == true)
        {
            //ボールの動作停止検知
            if (transform.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                m_Shoot = false;
                PlayerTransform.position = this.transform.position;
                StartPosition = transform.position;
                Debug.Log("Ball Stop");
            }
        }

        //ボール落下判定
        if (transform.position.y <= -30)
        {
            transform.position = StartPosition;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        //デバッグ用処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 velo = Vector3.zero;
            velo.x = speed.x;
            velo.z = speed.z;
            BallmoveStart(velo);
        }
    }

    //プレイヤーをボールの座標に合わせる
    public void SetPlayerPos()
    {
        PlayerTransform.position = this.transform.position;
    }

    //ボールを打つ
    public void BallmoveStart(Vector3 velo)
    {
        m_Shoot = true;
        transform.GetComponent<Rigidbody>().AddForce(velo);
    }
}
