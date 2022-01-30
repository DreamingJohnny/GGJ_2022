using System;
using Cinemachine;
using UnityEngine;

public class CameraShake : SingletonBehaviour<CameraShake>
{
	private CinemachineVirtualCamera vcam;
	private CinemachineBasicMultiChannelPerlin shake;
	private float shakeTimer;
	private float shakeDuration;
	private float shakeIntensity;

	protected override void Awake()
	{
		base.Awake();

		vcam = GetComponent<CinemachineVirtualCamera>();
		shake = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}

	private void Update()
	{
		shakeTimer += Time.deltaTime;
		// if (shakeTimer >= shakeDuration)
		// {
		// 	shake.m_AmplitudeGain = 0f;
		// }
		if (shakeDuration > 0)
		{
			float t = Falloff(shakeTimer / shakeDuration);
			shake.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, t);
			print(new {t = t, gain = shake.m_AmplitudeGain});
		}
		else
		{
			shake.m_AmplitudeGain = 0f;
		}
	}

	public void Shake(float intensity, float duration)
	{
		shake.m_AmplitudeGain = intensity;
		shakeTimer = 0f;
		shakeDuration = duration;
	}

	private static float Falloff(float x)
	{
		return Mathf.Pow(x, 12);
	}
}
