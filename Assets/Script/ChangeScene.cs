using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string _sceneName)
	{
		SceneManager.LoadScene(_sceneName);
	}
}
