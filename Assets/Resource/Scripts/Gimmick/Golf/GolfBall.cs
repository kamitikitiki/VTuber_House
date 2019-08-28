using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{

    public Transform PlayerTransform;

    private bool m_Shoot;

    public float vx;
    public float vz;

    // Start is called before the first frame update
    void Start()
    {
        m_Shoot = false;
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
                Debug.Log("Ball Stop");
            }
        }

        //デバッグ用処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 velo = Vector3.zero;
            velo.x = vx;
            velo.z = vz;
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
