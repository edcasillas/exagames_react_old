using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem fireFx;
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
			StartCoroutine("SpawnGolem");
		}
	}

	private IEnumerator SpawnGolem() {
		yield return new WaitForSeconds(1);

		golemController.gameObject.SetActive(true);
	}
}
