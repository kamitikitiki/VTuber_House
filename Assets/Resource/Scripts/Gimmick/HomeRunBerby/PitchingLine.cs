using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RouteMoveStatus
{
    stop,
    move,
    hit,
    end
}

[System.Serializable]
public class RouteParameter
{
    public Transform MovePoint;
    public Transform EndMovePoint;
    public float time;
    public bool CurveFlag = false;
    public AnimationCurve Curve;
}

public class PitchingLine : MonoBehaviour
{
    [SerializeField]
    public Transform ball;
    public List<RouteParameter> routes;


    private RouteParameter nowRoute;

    private float startTime;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private RouteMoveStatus movekey;
    private bool startmove;


    private void Start()
    {
        movekey = RouteMoveStatus.stop;
        startmove = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ball.GetComponent<HitBallManeger>().Reset();
            Debug.Log("リセット");
            movekey = RouteMoveStatus.stop;
            startmove = false;
        }

        switch (movekey)
        {
            //待機状態
            case RouteMoveStatus.stop:
                if (startmove)
                {
                    startTime = Time.timeSinceLevelLoad;
                    nowRoute = routes.Find(r => r.MovePoint.name == "StartPoint");
                    movekey = RouteMoveStatus.move;
                }

                break;
            //移動状態
            case RouteMoveStatus.move:
                var diff = Time.timeSinceLevelLoad - startTime;

                if (diff > nowRoute.time)
                {
                    if (nowRoute.EndMovePoint.name == "EndPoint")
                    {
                        movekey = RouteMoveStatus.end;
                    }
                    else
                    {
                        ball.position = nowRoute.EndMovePoint.position;
                        string rName = nowRoute.EndMovePoint.name;
                        startTime = Time.timeSinceLevelLoad;
                        nowRoute = routes.Find(r => r.MovePoint.name == rName);
                    }
                }
                var rate = diff / nowRoute.time;
                var pos = nowRoute.Curve.Evaluate(rate);

                if (nowRoute.CurveFlag)
                {
                    ball.position = Vector3.Slerp(nowRoute.MovePoint.position, nowRoute.EndMovePoint.position, pos);
                }
                else
                {
                    ball.position = Vector3.Lerp(nowRoute.MovePoint.position, nowRoute.EndMovePoint.position, rate);
                }

                break;
            //ヒット
            case RouteMoveStatus.hit:

                break;
            //移動終了
            case RouteMoveStatus.end:
                movekey = RouteMoveStatus.stop;
                startmove = false;
                break;
        }

        if(ball.GetComponent<HitBallManeger>().IsHit())
        {
            movekey = RouteMoveStatus.hit;
        }

    }

    //ギズモ
    private void OnDrawGizmosSelected()
    {
        foreach(RouteParameter route in routes)
        {
            if (route.MovePoint.name == "EndPoint")
            {
                continue;
            }
            else
            {
                Gizmos.DrawLine(route.MovePoint.position, route.EndMovePoint.position);
            }
        }
    }

    public void PitcheingStart()
    {
        startmove = true;
    }

    public bool PitchingEnd()
    {
        return movekey == RouteMoveStatus.end ? true : false;
    }

    public bool PitchingHit()
    {
        return movekey == RouteMoveStatus.hit ? true : false;
    }

    public void IsHit()
    {
        movekey = RouteMoveStatus.hit;
    }
}
