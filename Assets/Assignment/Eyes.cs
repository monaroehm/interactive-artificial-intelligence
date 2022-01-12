using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Eyes provide the agent the ability to identify objects at a distance.
/// </summary>
[RequireComponent(typeof(Agent))]
public class Eyes : Sensor
{

    /// <summary>
    /// Checks for Ghosts or Items in the specified direction and returns the first object and its distance found, if any.
    /// </summary>
    /// <returns>The first perceived object.</returns>
    /// <param name="direction">Direction to look at.</param>
    public Percept Look(Direction direction)
    {
        if (direction == Direction.NONE)
        {
            Percept nullPercept;
            nullPercept.distance = 0;
            nullPercept.type = PerceptType.NOTHING;
            nullPercept.ghost = null;
            return nullPercept;
        }

        RaycastHit2D hit2D = Physics2D.Raycast(agent.currentTile, direction.ToVector2(), 1000, ~(1 << 8) );
        GameObject obj = hit2D.collider.gameObject;

        Percept percept;

        if (obj.GetComponent<Ghost>())
        {
            percept.type = PerceptType.GHOST;
            percept.distance = Mathf.RoundToInt(hit2D.distance);
            percept.ghost = obj.GetComponent<Ghost>();
        }
        else if (obj.GetComponent<PickupItem>())
        {
            percept.type = PerceptType.ITEM;
            percept.distance = Mathf.RoundToInt(hit2D.distance);
            percept.ghost = null;
        }
        else
        {
            percept.type = PerceptType.NOTHING;
            percept.distance = 0;
            percept.ghost = null;
        }

        return percept;
    }
}

public struct Percept
{
    public int distance;
    public PerceptType type;
    public Ghost ghost;
}

public enum PerceptType
{
    GHOST,
    ITEM,
    NOTHING
}