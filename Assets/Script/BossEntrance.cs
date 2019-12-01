using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem fireFx;
	[SerializeField]
	private GameObject fireRingTriggers;
	[SerializeField]
	private AudioSource fireRingExplosionSound;
	[SerializeField]
	private AudioClip fireRingExplosionClip;
	#region Golem Logic
	[SerializeField]
	private GolemController golemController;
	[SerializeField]
	private PlayerController playerController;
	#endregion
	// Start is called before the first frame update
	void Start()
    {
		fireFx.Pause();
    }

	private void OnTriggerEnter(Collider other) 
	{
		playerController = other.gameObject.GetComponent<PlayerController>();
		if(playerController) 
		{
			if(golemController) 
			{
				golemController.SetPlayer(playerController);
			}

			fireFx.Play();
			StartCoroutine("ActivateTriggers");
			fireRingExplosionSound.PlayOneShot(fireRingExplosionClip);
			StartCoroutine("SpawnGolem");
		}
	}
	private IEnumerator ActivateTriggers() {
		yield return new WaitForSeconds(.3f);
		fireRingTriggers.SetActive(true);
	}

	private IEnumerator SpawnGolem() {
		yield return new WaitForSeconds(1);

		golemController.gameObject.SetActive(true);
	}
}
