using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject Gun;

	private void Awake() {
		Gun.SetActive(false);
	}

	private void OnTriggerEnter(Collider other) {
		var pickup = other.gameObject.GetComponent<Pickup>();
		if (pickup) {
			Debug.Log("Pickup was taken");
			Gun.SetActive(true);
			Destroy(pickup.gameObject);
		}
	}
}