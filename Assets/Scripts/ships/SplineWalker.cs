
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineWalker
{
    Spline spline;
    Transform transform;
    float speed;
    bool loop;
    bool repeat;
    bool reverse = false;
    List<Vector3> waypoints;
    int currentWaypoint = 0;


    public SplineWalker(Spline spline, Transform transform, float speed = 1f, bool loop = false, bool repeat = false)
    {
        this.spline = spline;
        this.transform = transform;
        this.speed = speed;
        this.loop = loop;
        this.repeat = repeat;

        DefinePoints();
    }


    void DefinePoints()
    {
        waypoints = new List<Vector3>();

        foreach (var knot in spline.Knots)
        {
            waypoints.Add(transform.TransformPoint(knot.Position));
        }
    }

    public void MoveOnSpline()
    {
        if (currentWaypoint < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[currentWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
                if (reverse)
                    currentWaypoint -= 1;
                else
                    currentWaypoint += 1;
        }

        if (currentWaypoint == 0 && reverse)
        {
            reverse = false;
            return;
        }

        if (currentWaypoint == waypoints.Count && loop)
        {
            currentWaypoint -= 1;
            reverse = true;
            return;
        }

        if (currentWaypoint == waypoints.Count && repeat)
        {
            DefinePoints();
            currentWaypoint = 0;
            return;
        }
    }
}
