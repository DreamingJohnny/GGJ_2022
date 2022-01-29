using UnityEngine;

public abstract class LazyCreatedSingletonBehaviour<T> : MonoBehaviour where T : LazyCreatedSingletonBehaviour<T>
{
	private static T s_instance;

	public static T Instance
	{
		get
		{
			if (s_instance != null)
				return s_instance;

			LazyCreateInstance();
			return s_instance;
		}
	}

	protected virtual void Awake()
	{
		if (s_instance == null)
			s_instance = (T)this;
		else if (s_instance != this)
			Destroy(gameObject);
	}

	protected virtual void OnDestroy()
	{
		if (s_instance == this)
			s_instance = null;
	}

	private static void LazyCreateInstance()
	{
		GameObject go = new GameObject(typeof(T).Name);
		s_instance = go.AddComponent<T>();
	}
}