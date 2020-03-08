using UnityEngine;

public class GunTester : MonoBehaviour {
	public ParticleSystem Particles;

	private void Update() {
		var fire = Input.GetAxis("Fire1");

		if (fire > 0f) {
			Particles.Play();
		} else {
			Particles.Stop();
		}
	}
}