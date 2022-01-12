using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// The logic of an Agent of type <typeparamref name="AgentType"/>. By processing sensory input and making (movement) decisions, it defines the behavior.
/// 
/// <c>OnDecisionRequired()</c>: The controller can react whenever a motion decision is required due to the maze.
/// 
/// <c>OnTileReached()</c>: The controller can react whenever a tile was reached (less frequent compared to using <c>Update()</c>).
/// 
/// </summary>
[RequireComponent(typeof(Agent))]
public abstract class AgentController<AgentType> : MonoBehaviour where AgentType : Agent
{
    protected AgentType agent;

    protected virtual void Awake()
    {
        agent = GetComponent<AgentType>();
    }

    /// <summary>
    /// Called when the agent reaches an intersection.
    /// Move commands sent in this step will be executed as the next motion, if valid and possible.
    /// </summary>
    public abstract void OnDecisionRequired();

    /// <summary>
    /// Called when the agent reaches the center of a tile.
    /// The move sent will be processed as soon as it becomes valid and possible.
    /// </summary>
    public abstract void OnTileReached();

}
