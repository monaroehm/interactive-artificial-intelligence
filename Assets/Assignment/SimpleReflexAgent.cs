using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple reflex agent controller.
/// 
/// A reflex agent reacts to every sensory input in an identical way, independent of previous experience.
/// It is "stateless" and should not have any variables that change due to input or actions.
/// </summary>
[RequireComponent(typeof(MazeMap))]
[RequireComponent(typeof(Eyes))]
public class SimpleReflexAgent : AgentController<MsPacMan>
{

    MazeMap map;
    Eyes eyes;

    protected  override void Awake()
    {
        base.Awake();
        map = GetComponent<MazeMap>();
        eyes = GetComponent<Eyes>();
    }

	public override void OnDecisionRequired()
    {
       // throw new System.NotImplementedException();
    }

    public override void OnTileReached()
    {
       // throw new System.NotImplementedException();
    }
}
