using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public HermiteMove prefab;

    public WayPoints waypoints;

    public void Try()
    {
        var hermite = Instantiate(prefab, transform);
        hermite
            .Run(waypoints.Points,
            6,
            h =>
            {
                Destroy(h.gameObject);
            });
    }
}
