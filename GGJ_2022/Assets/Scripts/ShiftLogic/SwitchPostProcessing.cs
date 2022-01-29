using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SwitchPostProcessing : ShiftBehaviour
{
	[SerializeField] private VolumeProfile realProfile;
	[SerializeField] private VolumeProfile shadowProfile;

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			GetComponent<Volume>().profile = realProfile;
		}
		else
		{
			GetComponent<Volume>().profile = shadowProfile;
		}
	}
}
