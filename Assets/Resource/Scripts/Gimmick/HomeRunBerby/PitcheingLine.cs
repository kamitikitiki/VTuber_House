using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RouteParameter
{
    public Transform MovePoint;
    public float time;
}

/*
public interface IAutoMovement
{
    void setMovementVelocity(Vector3 velocity);

    void onMovementStart();
    void onMovementEnd();
}
*/

public class PitcheingLine : MonoBehaviour
{
    [SerializeField]
    protected List<RouteParameter> routes;
}
