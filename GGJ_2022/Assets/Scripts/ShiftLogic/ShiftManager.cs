using System;
using UnityEngine;

public class ShiftManager : LazyCreatedSingletonBehaviour<ShiftManager>
{
	private WorldState worldState;

	public WorldState CurrentWorldState
	{
		get => worldState;
		set
		{
			if (worldState == value)
				return;

			worldState = value;
			WorldStateChanged?.Invoke(worldState);

			Debug.Log($"World is now {worldState}");
		}
	}

	public event Action<WorldState> WorldStateChanged;

	public void ShiftWorld()
	{
		CurrentWorldState = CurrentWorldState == WorldState.RealWorld ? WorldState.ShadowLand : WorldState.RealWorld;
	}
}

public enum WorldState
{
	RealWorld,
	ShadowLand,
}