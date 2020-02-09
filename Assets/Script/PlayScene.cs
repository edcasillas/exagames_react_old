using Audio.Configurations;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour 
{
	[SerializeField] private Button _playButton;
	[SerializeField] private Scenes _sceneName;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip buttonSound;

	private void Start() {
		//_playButton.onClick.AddListener(() => LoadScene(_sceneName));
	}

	public void LoadScene(Scenes sceneName) {
		audioSource.PlayOneShot(buttonSound);

		ScenesManager.Instance.ChangeScene(sceneName.ToString());
	}

	public void GoToVolcano() 
	{
		ScenesManager.Instance.ChangeScene(Scenes.Volcano.ToString());
	}

	public void GoToMainMenu() {
		audioSource.PlayOneShot(buttonSound);
		ScenesManager.Instance.ChangeScene(Scenes.MainMenu.ToString());
	}
}