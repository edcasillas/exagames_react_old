using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
	public static ScenesManager Instance { get; private set; }

	private void Start() 
	{
		if (Instance == null)
		{
			Instance = this;
		} 
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	public void ChangeScene(string scene) 
	{
		SceneManager.LoadSceneAsync(scene);
		SceneManager.sceneLoaded += SceneIsLoaded;
	}

	private void SceneIsLoaded(Scene scenLoaded, LoadSceneMode LoadMode) 
	{
		Debug.Log("loaded scene name" + scenLoaded.name);
		Debug.Log("Load scene mode" + LoadMode);
	}
}