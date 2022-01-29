using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private RectTransform fill;
	[SerializeField] private RectTransform background;

	private PlayerHealth playerHealth;
	private float heartWidth;

	private void Start()
	{
		GameObject player = GameObject.FindWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		playerHealth.onHealthChanged.AddListener(OnPlayerHealthChanged);

		heartWidth = fill.GetComponent<Image>().sprite.rect.width;
		UpdateWidth(fill, heartWidth * playerHealth.MaxHealth);
		UpdateWidth(background, heartWidth * playerHealth.MaxHealth);
	}

	private void UpdateWidth(RectTransform rt, float width)
	{
		rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
	}

	private void OnPlayerHealthChanged()
	{
		UpdateWidth(fill, heartWidth * playerHealth.Health);
	}
}
