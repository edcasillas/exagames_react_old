using Audio.Configurations;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour {

	[SerializeField] private Button _playButton;
	[SerializeField] private Scenes _sceneName;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip buttonSound;

	private void Start()
	{
		//_playButton.onClick.AddListener(() => LoadScene(_sceneName));
	}

	public void LoadScene(Scenes sceneName)
	{
		audioSource.PlayOneShot(buttonSound);

		SceneManager.LoadScene(sceneName.ToString());
	}
	
	public void GoToVolcano() 
	{
		SceneManager.LoadScene(Scenes.Volcano.ToString());
		SoundManager.Instance.PlaySingleClip(MusicClipName.StartBattleLoop);
		SoundManager.Instance.PlayLoopClip(MusicClipName.MainBattleLoop, false);
	}

	
	public void GoToMainMenu() 
	{
		audioSource.PlayOneShot(buttonSound);
		SceneManager.LoadScene("MainMenu");
		SoundManager.Instance.PlayLoopClip(MusicClipName.MainMenu, true);
	}
}
