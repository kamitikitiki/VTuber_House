using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 velo = Vector3.zero;
            velo.z = 300;
            BallmoveStart(velo);
        }
    }

    //ボールを打つ
    public void BallmoveStart(Vector3 velo)
    {
        transform.GetComponent<Rigidbody>().AddForce(velo);
    }
}
