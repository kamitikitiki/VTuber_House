using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RouteMoveStatus
{
    stop,
    move,
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

    [HideInInspector]
    [System.NonSerialized]
    public RouteMoveStatus moveEnd;

    [HideInInspector]
    [System.NonSerialized]
    public bool nextRoute;    
}

public class PitcheingLine : MonoBehaviour
{
    [SerializeField]
    public List<RouteParameter> routes;

    private RouteParameter nowRoute;

    private float startTime;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private RouteMoveStatus movekey;

    private void Start()
    {
        movekey = RouteMoveStatus.stop;


        foreach (RouteParameter route in routes)
        {
            route.moveEnd = RouteMoveStatus.stop;
            route.nextRoute = false;
        }
    }

    private void Update()
    {
        switch (movekey)
        {
            case RouteMoveStatus.stop:
                if (Input.GetKey(KeyCode.Space))
                {
                    startTime = Time.timeSinceLevelLoad;
                    nowRoute = routes.Find(r => r.MovePoint.name == "StartPoint");
                    movekey = RouteMoveStatus.move;
                }

                break;
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
                        transform.position = nowRoute.EndMovePoint.position;
                        string rName = nowRoute.EndMovePoint.name;
                        startTime = Time.timeSinceLevelLoad;
                        nowRoute = routes.Find(r => r.MovePoint.name == rName);
                    }
                }
                var rate = diff / nowRoute.time;
                var pos = nowRoute.Curve.Evaluate(rate);

                if (nowRoute.CurveFlag)
                {
                    transform.position = Vector3.Slerp(nowRoute.MovePoint.position, nowRoute.EndMovePoint.position, pos);
                }
                else
                {
                    transform.position = Vector3.Lerp(nowRoute.MovePoint.position, nowRoute.EndMovePoint.position, rate);
                }


                break;
            case RouteMoveStatus.end:
                movekey = RouteMoveStatus.stop;

                break;
        }
    }

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
}
