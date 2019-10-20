using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
	public ParticleSystem Particles;

	public float WaterLevel = 100;
	public float ConsumeSpeed = 20f;
	public Image WaterLevelFillImage;

	private bool isRecovering;

	private void Update() {
		if (isRecovering) {
			Particles.Stop();
			WaterLevel += ConsumeSpeed * 2 * Time.deltaTime;
			if (WaterLevel >= 100) {
				WaterLevel = 100;
				isRecovering = false;
			}
		} else {
			var fire = Input.GetAxis("Fire1");
			if (fire > 0f) {
				WaterLevel -= ConsumeSpeed * Time.deltaTime;
				if (WaterLevel > 0f) {
					Particles.Play();
				} else {
					isRecovering = true;
					Particles.Stop();
				}
			} else {
				Particles.Stop();
			}
		}

		WaterLevelFillImage.fillAmount = WaterLevel / 100f;
	}
}