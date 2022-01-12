using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphs;

public class MazeGraph<Data> : MonoBehaviour where Data : MazeGraph<Data>.PositionData
{
	protected Node<Data> graph;

	protected GameMode gameMode;
	protected Maze maze;

	[Header("Drawing")]
	[SerializeField] bool Draw = true;
	[SerializeField] Vector2 DrawingOffset = new Vector2(0, 0);
	[SerializeField] Color DrawingColor = Color.green;

	void Awake()
	{
		maze = GameObject.Find("Maze").GetComponent<Maze>();
	}

	void Update()
	{
		if (Draw)
			DrawGraph();
	}

	void DrawGraph()
	{
		if (graph == null)
			return;

		var ToDraw = new List<Node<Data>>();
		var Drawing = new List<Node<Data>>();
		var Drawn = new List<Node<Data>>();

		ToDraw.Add(graph);

		while (ToDraw.Count > 0)
		{
			Drawing.AddRange(ToDraw);
			ToDraw.Clear();

			foreach (var c in Drawing)
			{
                Debug.DrawLine(c.data.position + DrawingOffset, c.data.position - DrawingOffset, DrawingColor);

                foreach (var cc in c.Neighbors)
				{
					Debug.DrawLine(c.data.position + DrawingOffset, cc.data.position + DrawingOffset, DrawingColor);
					ToDraw.Add(cc);
				}
				Drawn.Add(c);
			}

			Drawing.Clear();
			ToDraw.RemoveAll((c) => { return Drawn.Contains(c); });
		}
	}

	public void DrawPath(List<Node<Data>> path)
	{
		DrawPath(path, DrawingColor);
	}

	public void DrawPath(List<Node<Data>> path, Color color)
	{
		for (int i = 1; i < path.Count; i++)
		{
			Debug.DrawLine(path[i - 1].data.position - DrawingOffset,
						   path[i].data.position - DrawingOffset,
						   color);
		}
	}

	public class PositionData
	{
		public Vector2 position;
	}

}
