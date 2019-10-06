using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem fireFx;
	[SerializeField]
	private GameObject golem;
    // Start is called before the first frame update
    void Start()
    {
		fireFx.Pause();
    }

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag== "Player") {
			fireFx.Play();
			StartCoroutine("SpawnGolem");
		}
	}

	private IEnumerator SpawnGolem() {
		yield return new WaitForSeconds(1);

		golem.SetActive(true);
	}
}
