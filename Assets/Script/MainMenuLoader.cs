using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class MainMenuLoader : MonoBehaviour {
	private IEnumerator Start() {
		var asyncLoad = SceneManager.LoadSceneAsync(Scenes.MainMenu.ToString());
		asyncLoad.allowSceneActivation = false;

		var videoPlayer = GetComponent<VideoPlayer>();

		videoPlayer.Play();
		yield return new WaitForSecondsRealtime((float)videoPlayer.clip.length);
		
		asyncLoad.allowSceneActivation = true;

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone) {
			yield return null;
		}
	}
}