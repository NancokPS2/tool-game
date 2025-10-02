using System;
using System.Collections.Generic;
using Godot;

namespace ToolGame.Singleton;

public abstract partial class NodeManager : Node
{
	List<Node> TrackedNodes = new();
	public override void _EnterTree()
	{
		base._EnterTree();
		AddToGroup(CompGroups.NODE_MANAGER);
	}

	protected abstract bool IsValidNode(Node node);

	public void AddNode(Node node)
	{
		if (IsValidNode(node))
		{
			TrackedNodes.Add(node);
		}
	}

	public void RemoveNode(Node node)
	{
		TrackedNodes.Remove(node);
	}
}