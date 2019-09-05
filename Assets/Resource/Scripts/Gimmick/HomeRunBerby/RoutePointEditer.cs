using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePointEditer : MonoBehaviour
{
    public bool draw_sphere;
    public float sphere_radius;
    public Color sphereColor = Color.blue;

    private void OnDrawGizmos()
    {
        if(draw_sphere)
        {
            Gizmos.color = sphereColor;
            Gizmos.DrawSphere(transform.position, sphere_radius);
        }
    }
}
