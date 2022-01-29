using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShiftEffect : MonoBehaviour
{
	[SerializeField] private Material material;
	[SerializeField] private float scaleFrom = 0.5f;
	[SerializeField] private float scaleTo = 1.7f;
	[SerializeField] private bool pauseTime = true;
	[SerializeField] private float duration = 1f;

	private RawImage image;
	private RenderTexture screenCapture;
	private Camera mainCamera;

	private bool isPlaying;
	// private Mesh fullscreenQuadMesh;

	public bool IsPlaying => isPlaying;

	private void Start()
	{
		image = GetComponent<RawImage>();
		mainCamera = Camera.main;

		material = new Material(material);
		image.material = material;
		image.enabled = false;

		// fullscreenQuadMesh = new Mesh();
		// fullscreenQuadMesh.SetVertices(new []
		// {
		// 	new Vector3(-1f, -1f),
		// 	new Vector3(-1f, 1f),
		// 	new Vector3(1f, 1f),
		// 	new Vector3(1f, -1f),
		// });
		// fullscreenQuadMesh.SetUVs(0, new []
		// {
		// 	new Vector2(0f, 0f),
		// 	new Vector2(0f, 1f),
		// 	new Vector2(1f, 1f),
		// 	new Vector2(1f, 0f),
		// });
		// fullscreenQuadMesh.SetIndices(new []
		// {
		// 	0, 1, 2,
		// 	0, 2, 3,
		// }, MeshTopology.Triangles, 0);
		//
		// cmdbuff = new CommandBuffer();
		// cmdbuff.name = "HEEEEJ (nummer 2)";
		// cmdbuff.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
		// cmdbuff.DrawMesh(fullscreenQuadMesh, Matrix4x4.identity, material);
		//
		// mainCamera.AddCommandBuffer(CameraEvent.AfterImageEffects, cmdbuff);
	}

	// private CommandBuffer cmdbuff;

	// private void OnEnable()
	// {
	// 	RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
	//
	// }
	//
	// private void OnDisable()
	// {
	// 	RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
	// }

	// private void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam)
	// {
	// 	if (!isPlaying)
	// 		return;
	//
	// 	if (cam == null || cam != mainCamera)
	// 		return;
	//
	// 	// Debug.Log("asdasd");
	// 	//
	// 	// CommandBuffer cmd = CommandBufferPool.Get("HEEEEEEEJ");
	// 	//
	// 	// cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
	// 	// cmd.DrawMesh(fullscreenQuadMesh, Matrix4x4.identity, material);
	// 	//
	// 	// ctx.ExecuteCommandBuffer(cmd);
	// 	//
	// 	// CommandBufferPool.Release(cmd);
	//
	// 	//ctx.
	//
	// 	// GL.Begin(GL.TRIANGLES);
	// 	// GL.PushMatrix();
	// 	// GL.LoadIdentity();
	// 	//
	// 	// material.SetPass(0);
	// 	//
	// 	// GL.Vertex3(-1f, -1f, 0f);
	// 	// GL.Vertex3(-1f, 1f, 0f);
	// 	// GL.Vertex3(1f, 1f, 0f);
	// 	//
	// 	// GL.Vertex3(-1f, -1f, 0f);
	// 	// GL.Vertex3(1f, 1f, 0f);
	// 	// GL.Vertex3(1f, -1f, 0f);
	// 	// GL.PopMatrix();
	// 	// GL.End();
	// }

	public void PlayEffect(Action afterCaptureCallback)
	{
		if (isPlaying)
		{
			Debug.LogError("Already playing shift effect!", this);
			afterCaptureCallback();
		}
		else
		{
			StartCoroutine(CoPlayEffect(afterCaptureCallback));
		}
	}

	private IEnumerator CoPlayEffect(Action afterCaptureCallback)
	{
		try
		{
			screenCapture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
			mainCamera.targetTexture = screenCapture;
			mainCamera.Render();
			mainCamera.targetTexture = null;

			isPlaying = true;

			afterCaptureCallback();

			image.texture = screenCapture;
			image.enabled = true;

			if (pauseTime)
			{
				Time.timeScale = 0f;
			}

			for (float time = 0f; time < duration; time += Time.unscaledDeltaTime)
			{
				material.SetFloat("_ScaleRadius", Mathf.Lerp(scaleFrom, scaleTo, time / duration));
				yield return null;
			}

			Time.timeScale = 1f;

			image.enabled = false;
			isPlaying = false;
		}
		finally
		{
			if (screenCapture)
			{
				RenderTexture.ReleaseTemporary(screenCapture);
			}
		}
	}
}
