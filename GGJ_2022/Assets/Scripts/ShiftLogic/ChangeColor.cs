using System;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
	[SerializeField] private Color realColor = Color.green;
	[SerializeField] private Color darkColor = Color.blue;

	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		ShiftManager.Instance.StateChanged += OnStateChanged;
		OnStateChanged(ShiftManager.Instance.CurrentState);
	}

	private void OnStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			spriteRenderer.color = realColor;
		}
		else
		{
			spriteRenderer.color = darkColor;
		}
	}
}