using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint: MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.waypoints.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.instance.waypoints.Remove(this.gameObject);
    }
}
