using System.Collections;
using Golems;
using Sound;
using UnityEngine;

public class BossEntrance : MonoBehaviour {
	[SerializeField] private ParticleSystem fireFx;
	[SerializeField] private GameObject fireRingTriggers;
	[SerializeField] private AudioSource fireRingExplosionSound;
	[SerializeField] private AudioClip fireRingExplosionClip;

	#region Golem Logic
	[SerializeField] private GolemController golemController;
	[SerializeField] private PlayerController playerController;
	#endregion

	private BossMusicController musicController;

	private void Awake() => musicController = GetComponent<BossMusicController>();

	private void Start() => fireFx.Pause();

	private void OnTriggerEnter(Collider other) {
		playerController = other.gameObject.GetComponent<PlayerController>();
		if (playerController) {
			if (golemController) {
				golemController.SetPlayer(playerController);
			}

			fireFx.Play();
			musicController.PlayIntroAndLoop(spawnGolem);
			StartCoroutine(nameof(activateTriggers));
			fireRingExplosionSound.PlayOneShot(fireRingExplosionClip);
		}
	}

	private IEnumerator activateTriggers() {
		yield return new WaitForSeconds(.3f);
		fireRingTriggers.SetActive(true);
	}

	private void spawnGolem() => golemController.gameObject.SetActive(true);
}