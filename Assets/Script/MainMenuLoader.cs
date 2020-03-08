using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class MainMenuLoader : MonoBehaviour {
	private AsyncOperation asyncLoad;

	private IEnumerator Start() {
		Debug.Log("Loading async scene");
		asyncLoad = SceneManager.LoadSceneAsync(Scenes.MainMenu.ToString());
		asyncLoad.allowSceneActivation = false;
		SceneManager.sceneLoaded += SceneIsLoaded;

		Debug.Log("Getting the video player component");
		var videoPlayer = GetComponent<VideoPlayer>();

		Debug.Log("Start to reproduce the video");
		videoPlayer.Play();
		Debug.Log("Waiting until the video is over");
		yield return new WaitForSecondsRealtime((float)videoPlayer.clip.length);
		Debug.Log("The video is over, the scene can be changed");
		asyncLoad.allowSceneActivation = true;

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone) {
			Debug.Log("Waiting for the scene to be ready");
			yield return null;
		}
	}

	private void Update() {
		if (Input.anyKeyDown) asyncLoad.allowSceneActivation = true;
	}

	private void SceneIsLoaded(Scene scenLoaded, LoadSceneMode LoadMode) {
		Debug.Log("loaded scene name" + scenLoaded.name);
		Debug.Log("Load scene mode" + LoadMode);
	}
}