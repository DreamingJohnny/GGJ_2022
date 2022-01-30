using System;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
	[SerializeField] private Transform followTarget;
	[SerializeField] private ParallaxLayer[] layers;

	private float xOffset;

	private void Start()
	{
		xOffset = transform.position.x - followTarget.position.x;

		foreach (ParallaxLayer layer in layers)
		{
			layer.rend.sharedMaterial = new Material(layer.rend.sharedMaterial);
		}
	}

	private void FixedUpdate()
	{
		Vector3 myPosition = transform.position;
		Vector3 targetPosition = followTarget.position;
		transform.position = new Vector3(targetPosition.x + xOffset, myPosition.y, myPosition.z);

		foreach (ParallaxLayer layer in layers)
		{
			Sprite sprite = layer.rend.sprite;
			float translation = targetPosition.x / sprite.rect.width * sprite.pixelsPerUnit * layer.speed;
			layer.rend.sharedMaterial.SetVector("_Scroll", new Vector4(translation, 0f));
		}
	}


	[Serializable]
	private struct ParallaxLayer
	{
		public SpriteRenderer rend;
		public float speed;
	}
}
