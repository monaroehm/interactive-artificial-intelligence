using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalKnowledgeSensor : Sensor
{
	GameMode gameMode;

	protected override void Awake()
	{
		base.Awake();
		gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
	}

	public MsPacMan GetMsPacMan()
	{
		return gameMode.GetMsPacMan();
	}

	public Ghost GetGhost(GhostName ghost)
	{
		return gameMode.GetGhost(ghost);
	}

	public PickupItem TryGetPickupItem(PickupType type, Vector2 tile)
	{
		return gameMode.TryGetPickupItem(type, tile);
	}

	public List<PickupItem> GetPickupItems(PickupType type)
	{
		return gameMode.GetPickups(type);
	}
}
