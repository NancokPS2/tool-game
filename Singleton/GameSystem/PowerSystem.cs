using Arch.Core;
using Arch.Core.Extensions;
using ToolGame.ECS;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class PowerSystem : BaseSystem
{
	public override QueryDescription QueryDefault { get; set; } = new QueryDescription().WithAny<IPowerConsumer, IPowerGenerator, IPowerConsumer>();


	public override void _SystemProcess(double delta)
	{
		base._SystemProcess(delta);

		{
			//Get power generated.
			double powerGenerated = 0;
			foreach (IPowerGenerator generator in chunk.Get())
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

	public static PowerMetricsContext GetPowerMetrics(Entity entity)
	{
		double stored = entity.Get<IPowerContainer>().PowerStored;
		double max = entity.Get<IPowerContainer>().PowerStoredMax;

		return new PowerMetricsContext(
			entity,
			stored,
			max
		);
	}

	public override void ProcessEntity(Entity entity, double delta)
	{
		throw new NotImplementedException();
	}
}
