using System;
using UnityEngine;

public class ShiftManager : LazyCreatedSingletonBehaviour<ShiftManager>
{
	private WorldState worldState;
	private ShiftEffect shiftEffect;

	public WorldState CurrentWorldState
	{
		get => worldState;
		set
		{
			if (worldState == value)
				return;
			if (shiftEffect && shiftEffect.IsPlaying)
				return;

			worldState = value;

			if (shiftEffect)
			{
				shiftEffect.PlayEffect(NotifyWorldStateChanged);
			}
			else
			{
				NotifyWorldStateChanged();
			}

			Debug.Log($"World is now {worldState}");
		}
	}

	public event Action<WorldState> WorldStateChanged;

	private void Start()
	{
		shiftEffect = FindObjectOfType<ShiftEffect>();
	}

	public void ShiftWorld()
	{
		CurrentWorldState = CurrentWorldState == WorldState.RealWorld ? WorldState.ShadowLand : WorldState.RealWorld;
	}

	private void NotifyWorldStateChanged()
	{
		WorldStateChanged?.Invoke(worldState);
	}
}

public enum WorldState
{
	RealWorld,
	ShadowLand,
}