using System;
using UnityEngine;

public class ChangeColor : ShiftBehaviour
{
	[SerializeField] private Color realColor = Color.green;
	[SerializeField] private Color darkColor = Color.blue;

	private SpriteRenderer spriteRenderer;

	protected override void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		base.Start();
	}

	protected override void OnWorldStateChanged(WorldState state)
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