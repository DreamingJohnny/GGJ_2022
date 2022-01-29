using UnityEngine;

public abstract class ShiftBehaviour : MonoBehaviour
{
	protected WorldState CurrentWorldState => ShiftManager.Instance.CurrentWorldState;

	protected virtual void Start()
	{
		ShiftManager.Instance.WorldStateChanged += OnWorldStateChanged;
		OnWorldStateChanged(CurrentWorldState);
	}

	protected abstract void OnWorldStateChanged(WorldState state);
}
