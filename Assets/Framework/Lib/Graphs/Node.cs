using System;
using System.Collections.Generic;

namespace Graphs
{
	/// <summary>
	/// Implement a simple graph data structure with a generic data attribute (DataType). 
	/// A node has connections to other nodes, called edges, and these edges have a weight (cost) attribute.
	/// </summary>
	public sealed class Node<DataType>
	{
		public DataType data;

		Dictionary<Node<DataType>, double> edgeDictionary = new Dictionary<Node<DataType>, double>();

		public Node(DataType data)
		{
			this.data = data;
		}

		/// <summary>
		/// Sets the edge weight for the edge to a node.
		/// Creates a new edge if necessary.
		/// </summary>
		/// <param name="node">Target node of the edge to create or modify.</param>
		/// <param name="weight">Weight of the edge to set. Must be positive.</param>
		public void SetEdge(Node<DataType> node, double weight)
		{
			if (weight <= 0)
				throw new Exception("Illegal edge weight: " + weight);

			if (node == this)
				throw new Exception("Cannot create edge to self.");

			edgeDictionary[node] = weight;
		}

#if NET_4_6
		// Note: Read-only if .Net 4.6 compatibility enabled
		public IReadOnlyDictionary<Node<DataType>, double> Edges {
#else
		public Dictionary<Node<DataType>, double> Edges
#endif
		{
			get
			{
				return edgeDictionary;
			}
		}

		public Dictionary<Node<DataType>, double>.KeyCollection Neighbors
		{
			get
			{
				return edgeDictionary.Keys;
			}
		}

		public override String ToString()
		{
			return String.Format("{{{0}}}", data);
		}
	}
}

