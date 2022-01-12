using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MsPacMan))]
public class KeyboardController : AgentController<MsPacMan>
{
    public override void OnDecisionRequired()
    {
    }

    public override void OnTileReached()
    {
		if(Input.GetKey(KeyCode.W))
		{
			agent.Move(Direction.UP);
		}
		else if(Input.GetKey(KeyCode.A))
		{
			agent.Move(Direction.LEFT);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			agent.Move(Direction.DOWN);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			agent.Move(Direction.RIGHT);
		}
    }
}
