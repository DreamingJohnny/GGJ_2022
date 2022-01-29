using System;
using UnityEngine;

public class ShiftManager : LazyCreatedSingletonBehaviour<ShiftManager>
{
	private WorldState worldState;

	public WorldState CurrentState
	{
		get => worldState;
		set
		{
			if (worldState == value)
				return;

			worldState = value;
			StateChanged?.Invoke(worldState);

			Debug.Log($"World is now {worldState}");
		}
	}

	public event Action<WorldState> StateChanged;

	public void ShiftWorld()
	{
		CurrentState = CurrentState == WorldState.RealWorld ? WorldState.ShadowLand : WorldState.RealWorld;
	}
}

public enum WorldState
{
	RealWorld,
	ShadowLand,
}