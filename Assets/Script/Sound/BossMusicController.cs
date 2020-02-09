using Audio;
using Audio.Configuration;
using System;
using UnityEngine;

namespace Sound {
	public class BossMusicController : MonoBehaviour {
		private bool isStarted;
		private bool isFinished;

		private void Start() => GameManager.Instance.SetBossMusicController(this);

		public void PlayIntroAndLoop(Action onIntroFinished = null) {
			if (isStarted) return;
			isStarted = true;
			SoundManager.Instance.Play(MusicClipName.StartBattleLoop, onFinished: () => {
				if(isFinished) return;
				onIntroFinished?.Invoke();
				SoundManager.Instance.Play(MusicClipName.MainBattleLoop, true);
			});
		}

		public void PlayDefeated() {
			isFinished = true;
			SoundManager.Instance.Play(MusicClipName.PlayerLoses);
		}

		public void PlayVictory() {
			isFinished = true;
			SoundManager.Instance.Play(MusicClipName.PlayerWin);
		}
	}
}