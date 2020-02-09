using Sound;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	private BossMusicController bossMusicController;

	private void Awake() {
		if(Instance) Destroy(gameObject);
		Instance = this;
	}

	private void OnDestroy() => Instance = null;

	public void SetBossMusicController(BossMusicController bossMusicController) => this.bossMusicController = bossMusicController;

	public void SetPlayerVictory() {
		bossMusicController.PlayVictory();
	}

	public void SetPlayerDead() {
		bossMusicController.PlayDefeated();
	}
}