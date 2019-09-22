using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public ParticleSystem Particles;

	private void Update() {
		var fire = Input.GetAxis("Fire1");
		if (fire > 0f) {
			//if (!Particles.isPlaying) 
				Particles.Play();
		} else {
			//if(Particles.isPlaying)
				Particles.Stop();
		}
	}

}