using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace ToolGame.Singleton;

[GlobalClass]
public partial class ECSManager : Node
{
	public static ECSManager Instance = null!;

	protected System.Collections.Generic.Dictionary<ulong, IEntity> EntityDict = new();
	protected System.Collections.Generic.Dictionary<ulong, Node> EntityNodeDict = new();
	protected System.Collections.Generic.Dictionary<ulong, List<IComponent>> ComponentDict = new();

	public override void _EnterTree()
	{
		base._EnterTree();
		Instance = this;
		GetTree().NodeAdded += OnNodeAdded;
		GetTree().NodeRemoved += OnNodeRemoved;
	}

	public static TComponent? GetComponent<TComponent>(ulong entityId) where TComponent : IComponent
	{
		return Instance.ComponentDict[entityId].OfType<TComponent>().SingleOrDefault();
	}

	public static TComponent[] GetComponents<TComponent>(ulong entityId) where TComponent : IComponent
	{
		return Instance.ComponentDict[entityId].OfType<TComponent>().ToArray();
	}

	public static IEntity? GetEntity(ulong entityId)
	{
		Instance.EntityDict.TryGetValue(entityId, out IEntity? entity);
		return entity;
	}

	public static void AddComponent(ulong entityId, IComponent comp)
	{
		Instance.EntityNodeDict[entityId].AddChild(comp as Node);
		Instance.RegisterComponent(entityId, comp);
	}

	protected void RegisterEntity(IEntity entity)
	{
		ulong entityId = entity.GetEntityId();

		if (ComponentDict.ContainsKey(entityId))
			throw new Exception($"Entity ID {entityId} is already taken. Caused by node at path {(entity as Node)?.GetPath() ?? "(NOT A NODE!?)"}");

		//Initialize the dicts for this entity
		EntityDict[entityId] = entity;
		EntityNodeDict[entityId] = entity as Node ?? throw new Exception();
		ComponentDict[entityId] = new();

		Log.Info($"Entity {EntityNodeDict[entityId].Name} added with ID {entityId}");
	}

	protected void RegisterComponent(ulong entityId, IComponent comp)
	{
		//Get EntityID
		Node entityNode = comp.GetEntityInstanceNode();
		Node compNode = comp as Node ?? throw new Exception();

		//Make sure it was added to the scene
		if (compNode.GetParent() != entityNode)
			throw new Exception($"The component must be a child of the entity.");

		//Ensure the instance is the same as stored (the entity needs to exist before the components).
		if (EntityDict[entityId] != entityNode)
			throw new Exception();

		//Add the entity to the group needed for a system to find it.
		foreach (string group in comp.GetProcessingGroups())
		{
			entityNode.AddToGroup(group);
		}

		//Add the component to the list.
		ComponentDict[entityId].Add(comp);

		Log.Info($"Component {compNode.Name} added to entity {entityNode.Name} (ID: {entityId})");
	}

	protected NodeManager[] GetAllNodeManagers()
	{
		NodeManager[] nodes = GetTree().GetNodesInGroup(CompGroups.NODE_MANAGER).OfType<NodeManager>().ToArray();
		return nodes;

	}

	private void OnNodeAdded(Node node)
	{
		if (node is IComponent comp)
		{
			RegisterComponent(comp.GetComponentId(), comp);
		}
		else if (node is IEntity entity)
		{
			RegisterEntity(entity);
		}
	}

	private void OnNodeRemoved(Node node)
	{
		if (node is IComponent comp)
		{
			ComponentDict[comp.GetComponentId()].Remove(comp);
		}
	}


}
