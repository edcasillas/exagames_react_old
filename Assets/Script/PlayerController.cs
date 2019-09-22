using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		var pickup = other.gameObject.GetComponent<Pickup>();
		if (pickup) {
			Debug.Log("Pickup was taken");
			Destroy(pickup.gameObject);
		}
	}
}