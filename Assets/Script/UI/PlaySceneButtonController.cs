using Enums;
using UnityEngine;

namespace UI {
	[RequireComponent(typeof(AudioSource))]
	public class PlaySceneButtonController : MonoBehaviour {
		[SerializeField] private Scenes _sceneName;
		[SerializeField] AudioClip buttonSound;

		private AudioSource audioSource;

		private void Awake() => audioSource = GetComponent<AudioSource>();

		public void LoadScene() => loadScene(_sceneName);

		private void loadScene(Scenes sceneName) {
			audioSource.PlayOneShot(buttonSound);
			ScenesManager.Instance.ChangeScene(sceneName.ToString());
		}
	}
}