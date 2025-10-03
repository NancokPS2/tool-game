namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class PowerSystem : BaseSystem
{
	public override void _SystemProcess(double delta)
	{
		base._SystemProcess(delta);




		foreach (IEntity entity in GetInGroup(CompGroups.MACHINERY))
		{
			IPowerGenerator[] generators = entity.GetComponents<IPowerGenerator>(); ;
			IPowerContainer[] containers = entity.GetComponents<IPowerContainer>();
			IPowerConsumer[] consumers = entity.GetComponents<IPowerConsumer>();

			//Get power generated.
			double powerGenerated = 0;
			foreach (IPowerGenerator generator in generators)
			{
				powerGenerated += generator.PowerProvided * delta;
			}

			//Add the power generated to the containers.
			ChangePowerMachineContext context = new ChangePowerMachineContext(
				entity.GetEntityId(), containers, powerGenerated
			);
			MachineAddPower(context);

			//Process the container drain and count the amount contained.
			double powerContained = 0;
			foreach (IPowerContainer container in containers)
			{
				powerContained += container.PowerStored;
				container.PowerStored -= container.PowerDrain * delta;
			}

			//Consume power
			foreach (IPowerConsumer consumer in consumers)
			{
				consumer.PowerSatisfied = powerContained > consumer.PowerConsumed;
				powerContained -= consumer.PowerConsumed * delta;
			}
		}
	}

	public void MachineAddPower(ChangePowerMachineContext context)
	{
		foreach (var item in context.PowerContainers)
		{
			double powerAdded = Math.Clamp(context.Amount, 0, item.GetCapacityRemaining());
			item.PowerStored += powerAdded;
			context.Amount -= powerAdded;

			if (context.Amount <= 0)
				break;
		}
	}

	public static PowerMetricsContext GetPowerMetrics(ulong entityId)
	{
		double stored = 0;
		double max = 0;
		foreach (var item in ECSManager.GetComponents<IPowerContainer>(entityId))
		{
			stored += item.PowerStored;
			max += item.PowerStoredMax;
		}

		return new PowerMetricsContext(
			entityId,
			stored,
			max
		);
	}

}
