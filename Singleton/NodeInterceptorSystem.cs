using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace ToolGame.Singleton;

public partial class NodeInterceptorSystem : Node
{
	public override void _Ready()
	{
		base._Ready();
		GetTree().NodeAdded += OnNodeAdded;
		GetTree().NodeRemoved += OnNodeRemoved;
	}


	protected NodeManager[] GetAllNodeManagers()
	{
		NodeManager[] nodes = GetTree().GetNodesInGroup(NodeGroups.NODE_MANAGER).OfType<NodeManager>().ToArray();
		return nodes;
	}

	private void OnNodeAdded(Node node)
	{
		foreach (var item in GetAllNodeManagers())
		{
			item.AddNode(node);
		}
	}

	private void OnNodeRemoved(Node node)
	{
		throw new NotImplementedException();
	}
}
