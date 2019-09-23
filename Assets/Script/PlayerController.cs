using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject Gun;
	public int InitialHealth;

	public int Health { get; private set; }

	private void Awake() {
		Gun.SetActive(false);
		Health = InitialHealth;
	}

	private void OnTriggerEnter(Collider other) {
		var pickup = other.gameObject.GetComponent<Pickup>();
		if (pickup) {
			Debug.Log("Pickup was taken");
			Gun.SetActive(true);
			Destroy(pickup.gameObject);
		}
	}

	public void TakeDamage(int damage) {
		Health -= damage;
		if (Health <= 0) {
			// is dead!
		}
	}
}