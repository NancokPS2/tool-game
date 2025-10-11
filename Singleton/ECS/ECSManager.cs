using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Arch.Core;
using Godot;
using Godot.Collections;

namespace ToolGame.ECS;

[GlobalClass]
public partial class ECSManager : Node
{
	public static ECSManager Instance = null!;
	public static Arch.Core.World World = Arch.Core.World.Create();

	protected System.Collections.Generic.Dictionary<ulong, Entity> EntityDict = new();

	public override void _EnterTree()
	{
		base._EnterTree();
		Instance = this;

		GetTree().NodeAdded += OnNodeAdded;
		GetTree().NodeRemoved += OnNodeRemoved;
	}

	public static bool IsNodeEntity(Node node)
	{
		return Instance.EntityDict.ContainsKey(node.GetInstanceId());
	}

	public static Entity GetNodeEntity(Node node)
	{
		if (!IsNodeEntity(node))
		{
			throw new Exception("The parent of this node is not an entity");
		}
		return Instance.EntityDict[node.GetInstanceId()];

	}

	public static void RegisterNodeAsComponent<TComponent>(TComponent node) where TComponent : Node, IComponent
	{
		if (!node.IsInsideTree())
			throw new Exception("The node must be inside the tree.");

		Entity entity = GetNodeEntity(node.GetParent());
		entity.Add(node, node);
		
	}

	public static Query Query(QueryDescription queryDescription)
	{
		return World.Query(queryDescription);
	}


	private void OnNodeAdded(Node node)
	{
		if (node.IsInGroup(CompGroups.ENTITY))
		{
			ulong id = node.GetInstanceId();
			Entity entity = ECSManager.World.Create(node);
			EntityDict[id] = entity;
		}
		else if (node is IComponent comp)
		{
			ulong id = node.GetParent().GetInstanceId();
			Entity entity = EntityDict[id];
			comp.RegisterToEntity(entity);
		}
	}
	private void OnNodeRemoved(Node node)
	{

	}

}
