using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class culb : MonoBehaviour
{

    public Transform cube;
    private Vector3 lasPos;
    // Start is called before the first frame update
    void Start()
    {
        lasPos = cube.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lasPos = cube.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "ball")
        {
            Vector3 nowPos = lasPos - cube.transform.position;
            GolfBall sc = other.GetComponent<GolfBall>();
            nowPos.y = 0;
            nowPos *= 3000;
            nowPos.x *= -1;
            sc.BallmoveStart(nowPos);
        }
    }
}
