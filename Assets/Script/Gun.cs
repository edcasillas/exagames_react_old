using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
	public ParticleSystem Particles;

	public float WaterLevel = 100;
	public float ConsumeSpeed = 20f;
	public Image WaterLevelFillImage;

	private bool isRecovering;

	[Header("Sound Variables")]
	[SerializeField] GunSounds gS;

	private void Update() {
		if (isRecovering) {

			
			if(!gS.GetAudioSource().isPlaying) {

			gS.PlayReloadingSound();
			}
			
			Particles.Stop();
			WaterLevel += ConsumeSpeed * 2 * Time.deltaTime;
			if (WaterLevel >= 100) {
				WaterLevel = 100;
				isRecovering = false;
				gS.GetAudioSource().Stop();
				
			}
		} else {
			var fire = Input.GetAxis("Fire1");
			if (fire > 0f) {
				WaterLevel -= ConsumeSpeed * Time.deltaTime;
				if (WaterLevel > 0f) {
					Particles.Play();
					if(!gS.GetAudioSource().isPlaying && !gS.isPaused) {
						gS.PlayShootSound();
					}
					else if(gS.isPaused) {
						gS.Resume();
					}
				} else {
					isRecovering = true;
					Particles.Stop();
					gS.GetAudioSource().Stop();
				}
			} else {
				Particles.Stop();
				gS.Pause();

			}
		}

		WaterLevelFillImage.fillAmount = WaterLevel / 100f;
	}
}