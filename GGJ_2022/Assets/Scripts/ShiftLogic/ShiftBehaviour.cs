using UnityEngine;

public abstract class ShiftBehaviour : MonoBehaviour
{
	protected virtual void Start()
	{
		ShiftManager.Instance.WorldStateChanged += OnWorldStateChanged;
		OnWorldStateChanged(ShiftManager.Instance.CurrentWorldState);
	}

	protected abstract void OnWorldStateChanged(WorldState state);
}
