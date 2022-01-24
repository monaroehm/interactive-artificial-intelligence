using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphs;

public abstract class GameMode : MonoBehaviour
{

	[Header("Prefabs")]
	[SerializeField]
	private GameObject msPacMan = null;
	[SerializeField]
	private GameObject blinky = null;
	[SerializeField]
	private GameObject pinky = null;
	[SerializeField]
	private GameObject inky = null;
	[SerializeField]
	private GameObject sue = null;
	[SerializeField]
	private GameObject pill = null;
	[SerializeField]
	private GameObject powerPellet = null;
	[SerializeField]
	private GameObject junction = null;

	[Header("Maze")]
	[SerializeField]
	TextAsset mazeFile = null;

	[SerializeField]
	Maze maze = null;

	protected MsPacMan player;
	protected Dictionary<GhostName, Ghost> ghosts = new Dictionary<GhostName, Ghost>();
	protected Dictionary<GhostName, bool> ghostsEdible = new Dictionary<GhostName, bool>();

	protected Node<GameTile>[,] nodes;

	protected Dictionary<PickupType, List<PickupItem>> pickupItems = new Dictionary<PickupType, List<PickupItem>>();
	protected Dictionary<PickupType, List<PickupItem>> pickupItemsEaten = new Dictionary<PickupType, List<PickupItem>>();


	private bool GhostsAreEdible = false;

	private void Awake()
	{
		GameData.Reset();
		maze.LoadMaze(mazeFile);
		this.nodes = maze.initialNodes;
		// connect nodes
	}

	private void Start()
	{
		InstantiateGhosts();
		InstantiatePlayer();
		InstantiatePowerUps();
		InstantiateJunctions();
	}

	protected void ResetGame()
	{
		ResetMaze();
		StopAllCoroutines();
		SetGhostEdible(false);
		GameData.Reset();
	}

	protected void ResetMaze()
	{
		ResetPickups();
		ResetGhosts();
		ResetPacMan();
		maze.ResetSmell();
	}

	void InstantiatePlayer()
	{
		player = Instantiate(msPacMan, maze.msPacManSpawn, Quaternion.identity).GetComponent<MsPacMan>();
	}

	void InstantiateGhosts()
	{
		ghosts.Add(GhostName.BLINKY, Instantiate(blinky, maze.ghostSpawn, Quaternion.identity).GetComponent<Ghost>());
		ghosts.Add(GhostName.PINKY, Instantiate(pinky, maze.ghostSpawn, Quaternion.identity).GetComponent<Ghost>());
		ghosts.Add(GhostName.INKY, Instantiate(inky, maze.ghostSpawn, Quaternion.identity).GetComponent<Ghost>());
		ghosts.Add(GhostName.SUE, Instantiate(sue, maze.ghostSpawn, Quaternion.identity).GetComponent<Ghost>());

		ghostsEdible.Add(GhostName.BLINKY, false);
		ghostsEdible.Add(GhostName.PINKY, false);
		ghostsEdible.Add(GhostName.INKY, false);
		ghostsEdible.Add(GhostName.SUE, false);
	}

	void InstantiatePowerUps()
	{
		foreach (PickupType type in maze.pickupItems.Keys)
		{
			if (!pickupItemsEaten.ContainsKey(type))
				pickupItemsEaten[type] = new List<PickupItem>();

			if (!pickupItems.ContainsKey(type))
				pickupItems[type] = new List<PickupItem>();

			foreach (Vector2 position in maze.pickupItems[type])
			{
				GameObject prefab;
				switch (type)
				{
					case PickupType.PILL:
						prefab = pill;
						break;
					case PickupType.POWER_PELLET:
						prefab = powerPellet;
						break;
					default:
						throw new UnityException("Not Implemented");
				}

				pickupItems[type].Add(Instantiate(prefab, position, Quaternion.identity).GetComponent<PickupItem>());

			}
		}
	}

	void InstantiateJunctions()
	{
		Instantiate(junction, maze.junctionOne, Quaternion.identity).GetComponent<Junction>().teleportationTarget = maze.junctionOne + Vector2.right * 26;
		Instantiate(junction, maze.junctionTwo, Quaternion.identity).GetComponent<Junction>().teleportationTarget = maze.junctionTwo - Vector2.right * 26;
	}

	private void ConnectNodes()
	{
		//iterate over array, connect all neighboring Nodes
		for (int col = 0; col < nodes.GetLength(0); col++)
		{
			for (int row = 0; row < nodes.GetLength(1); row++)
			{
				if(nodes[row, col] != null)
                {
					Vector2 currentPos = new Vector2(col, row);
					Node<GameTile> currentNode = nodes[col, row];

					if (maze.IsTileWalkable(currentPos + Vector2.up))
					{
						int newX = (int)currentPos.x + 0;
						int newY = (int)currentPos.y + 1;
						currentNode.SetEdge(nodes[newX, newY], 1);
					}
					// TODO repeat for other directions
					// out of bounds error
                }
			}
		}
	}

	Node<GameTile> GetMazeGraphForAgent(Vector2 position_Agent)
	{
		Node<GameTile> startNode = nodes[(int)position_Agent.x, (int)position_Agent.y];
		return startNode;
    }

	void ClearPickups()
	{
		foreach (PickupType type in pickupItems.Keys)
		{
			foreach (PickupItem item in pickupItems[type])
			{
				Destroy(item.gameObject);
			}
			pickupItems[type].Clear();
		}

		foreach (PickupType type in pickupItemsEaten.Keys)
		{
			pickupItemsEaten[type].Clear();
		}
	}

	void ClearPacMan()
	{
		Destroy(player.gameObject);
	}

	void ClearGhosts()
	{
		foreach (Ghost g in ghosts.Values)
		{
			Destroy(g.gameObject);
		}
		ghosts.Clear();
	}

	protected void ResetPacMan()
	{
		player.Reset(maze.msPacManSpawn);
	}

	protected void ResetGhost(Ghost ghost)
	{
		ghost.Reset(maze.ghostSpawn);
	}

	protected void ResetGhosts()
	{
		foreach (Ghost g in ghosts.Values)
		{
			ResetGhost(g);
		}
	}

	protected void ResetPickups()
	{
		foreach (PickupType type in pickupItems.Keys)
		{
			foreach (PickupItem item in pickupItems[type])
			{
				item.gameObject.SetActive(true);
			}
		}

		foreach (PickupType type in pickupItemsEaten.Keys)
		{
			pickupItemsEaten[type].Clear();
		}
	}

	protected abstract void OnGhostEncounter(MsPacMan pacMan, Ghost ghost);

	protected abstract void OnPickup(MsPacMan pacMan, PickupItem item);

	public void OnPickupCollision(Agent agent, PickupItem item)
	{
		if (agent.CompareTag("Player"))
		{
			MsPacMan pacMan = agent.GetComponent<MsPacMan>();
			OnPickup(pacMan, item);
		}
		// TODO add for all cases that item on Node of Vec2 of MsPacMan position is removed (change PickupType to NONE)
	}

	public void OnAgentCollision(Agent agent, Collision2D collision)
	{
		if (agent.CompareTag("Player"))
		{
			if (collision.gameObject.CompareTag("Ghost"))
			{
				OnGhostEncounter(agent.GetComponent<MsPacMan>(), collision.gameObject.GetComponent<Ghost>());
			}
		}
	}

	public bool IsGhostEdible(Ghost ghost)
	{
		return ghostsEdible[ghost.id];
	}

	public bool AreGhostsEdible(){
		return GhostsAreEdible;
	}

	protected void SetGhostEdible(Ghost ghost, bool isEdible)
	{
		ghostsEdible[ghost.id] = isEdible;
	}

	protected void SetGhostEdible(bool isEdible)
	{
		GhostsAreEdible = isEdible;
		foreach (Ghost g in ghosts.Values)
		{
			SetGhostEdible(g, isEdible);
		}
	}

	public void OnJunctionCollision(Agent agent, Vector2 targetPosition)
	{
		agent.TeleportTo(targetPosition);
	}

	public PickupItem TryGetPickupItem(PickupType type, Vector2 position)
	{
		int idx = maze.pickupItems[type].IndexOf(position);

		if (idx != -1)
			return pickupItems[type][idx];

		return null;
	}

	public MsPacMan GetMsPacMan()
	{
		return player;
	}

	public Ghost GetGhost(GhostName name)
	{
		return ghosts[name];
	}

	public List<PickupItem> GetPickups(PickupType type)
	{
		return pickupItems[type];
	}
}
