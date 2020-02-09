using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour 
{
	public static PauseManager instance;
	private bool _isInPause = false;

	public bool IsInPause {
		get { return _isInPause; }
		set {
			_isInPause = value;
			if (_isInPause) {
				pauseMenu.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				Time.timeScale = 0;
			} else {
				pauseMenu.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				Time.timeScale = 1;
			}
		}
	}

	[SerializeField] private GameObject pauseMenu;

	// Start is called before the first frame update
	void Awake() {
		if (instance) {
			Destroy(this);
		} else {
			instance = this;
		}
	}

	private void Start() {
		IsInPause = false;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			IsInPause = !IsInPause;
		}
	}

	public void ResumeGame() {
		IsInPause = false;
	}

	public void GoToMainMenu() {
		ScenesManager.Instance.ChangeScene(Scenes.MainMenu.ToString());
	}
}