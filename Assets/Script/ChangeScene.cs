using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	public void LoadScene(string _sceneName) 
	{
		ScenesManager.Instance.ChangeScene(_sceneName);
	}
}