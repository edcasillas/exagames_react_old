using System.Collections.Generic;
using UnityEngine;

namespace Audio.Configuration {
	[CreateAssetMenu(fileName = "MusicConfiguration", menuName = "ScriptableObjects/MusicConfiguration")]
	public class MusicConfiguration : ScriptableObject {
		[SerializeField] private MusicClips[] MusicClipsArray;

		private Dictionary<MusicClipName, AudioClip> _clipsDictionary;

		public AudioClip GetAudioClip(MusicClipName musicClipName) {
			InitializeDictionaryIfNeeded();

			if (_clipsDictionary.TryGetValue(musicClipName, out var clip)) {
				return clip;
			}

			Debug.LogError($"Clip with name {musicClipName} doesn't exist, check it on Music Configuration");
			return null;
		}

		private void InitializeDictionaryIfNeeded() {
			if (_clipsDictionary == null) {
				_clipsDictionary = new Dictionary<MusicClipName, AudioClip>();

				for (int i = 0; i < MusicClipsArray.Length; i++) {
					_clipsDictionary.Add(MusicClipsArray[i].MusicClipName, MusicClipsArray[i].MusicClip);
				}
			}
		}

		[System.Serializable]
		private class MusicClips {
			public MusicClipName MusicClipName;
			public AudioClip MusicClip;

		}
	}
}