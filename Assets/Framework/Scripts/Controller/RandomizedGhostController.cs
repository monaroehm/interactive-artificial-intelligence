using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public class RandomizedGhostController : AgentController<Ghost>
{

    public override void OnDecisionRequired()
    {
        agent.Move(DirectionExtensions.Random());
    }

    public override void OnTileReached()
    {
    }

}
