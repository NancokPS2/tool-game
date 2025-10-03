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

	protected NodeManager[] GetAllNodeManagers()
	{
		NodeManager[] nodes = GetTree().GetNodesInGroup(CompGroups.NODE_MANAGER).OfType<NodeManager>().ToArray();
		return nodes;

	}

	private void OnNodeAdded(Node node)
	{
		if (node is IComponent comp)
		{
			//Get EntityID
			ulong entityId = comp.GetComponentId();
			Node entityInstance = comp.GetEntityInstanceNode();

			//Ensure the instance is the same as stored (the entity needs to exist before the components).
			if (EntityDict[entityId] != entityInstance)
				throw new Exception();
			//Debug.Assert(EntityDict[entityId] == entityInstance);

			//Add the entity to the group needed for a system to find it.
			foreach (string group in comp.GetProcessingGroups())
			{
				entityInstance.AddToGroup(group);
			}

			//Add the component to the list.
			ComponentDict[entityId].Add(comp);

			Log.Info($"Component added {node.Name} to entity {entityInstance.Name} (ID: {entityId})");
		}
		else if (node is IEntity entity)
		{
			ulong entityId = node.GetEntityId();
			if (ComponentDict.ContainsKey(entityId))
				throw new Exception("Entity ID was already taken!?");
			//Debug.Assert(!ComponentDict.ContainsKey(entityId));

			//Initialize the dicts for this entity
			EntityDict[entityId] = entity;
			ComponentDict[entityId] = new();

			Log.Info($"Entity {node.Name} added with ID {entityId}");
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
