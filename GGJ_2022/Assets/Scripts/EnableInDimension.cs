using UnityEngine;

public class EnableInDimension : ShiftBehaviour
{
	[SerializeField] private WorldState activeState;

	protected override void OnWorldStateChanged(WorldState state)
	{
		gameObject.SetActive(state == activeState);
	}
}
