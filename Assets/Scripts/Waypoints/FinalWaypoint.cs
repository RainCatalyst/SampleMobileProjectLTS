using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWaypoint : Waypoint
{
    public override void Complete()
    {
        base.Complete();
        // Restart game on completion
        gameEvents.FinishLevel(true);
    }
}
