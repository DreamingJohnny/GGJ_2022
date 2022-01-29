using UnityEngine;

public class Boulder : ShiftBehaviour
{
	[SerializeField] private float normalMass = 10;
	[SerializeField] private float shadowWorldMass = 1;

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			GetComponent<Rigidbody2D>().mass = normalMass;
		}
		else
		{
			GetComponent<Rigidbody2D>().mass = shadowWorldMass;
		}
	}
}
