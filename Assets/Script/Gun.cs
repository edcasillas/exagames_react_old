using UnityEngine;

public class Gun : MonoBehaviour {
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