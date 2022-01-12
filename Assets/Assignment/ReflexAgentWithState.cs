using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reflex agent controller with state.
/// 
/// A reflex agent with state can change its state depending on observed sensory input or actions.
/// This includes timers, counters, seen / unseen objects, ...
/// </summary>
[RequireComponent(typeof(MazeMap))]
public class ReflexAgentWithState : AgentController<MsPacMan>
{
    
    MazeMap map;
    Eyes eyes;

    protected override void Awake()
    {
        base.Awake();
        map = GetComponent<MazeMap>(); 
        eyes = GetComponent<Eyes>();
    }

    public override void OnDecisionRequired()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTileReached()
    {
        throw new System.NotImplementedException();
    }
}
