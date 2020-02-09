using System.Collections.Generic;
using UnityEngine;

namespace Audio.Configurations 
{
	[CreateAssetMenu(fileName = "MusicConfiguration", menuName = "ScriptableObjects/MusicConfiguration")]
	public class MusicConfiguration : ScriptableObject 
	{
		[SerializeField] private MusicClips[] MusicClipsArray;

		private Dictionary<MusicClipName, AudioClip> _clipsDictionary;

		public AudioClip GetAudioClip(MusicClipName musicClipName) 
		{
			InitializeDictionaryIfNeeded();

			AudioClip clip;

			if (_clipsDictionary.TryGetValue(musicClipName, out clip)) 
			{
				return clip;
			}
			
			Debug.LogError(string.Format("Clip with name {0} doesn't exist, check it on Music Configuration", musicClipName));
			return null;
		}

		private void InitializeDictionaryIfNeeded()
		{
			if (_clipsDictionary == null)
			{
				_clipsDictionary = new Dictionary<MusicClipName, AudioClip>();
				
				for (int i = 0; i < MusicClipsArray.Length; i++)
				{
					_clipsDictionary.Add(MusicClipsArray[i].MusicClipName, MusicClipsArray[i].MusicClip);
				}
			}
		}
		
		[System.Serializable]
		private class MusicClips 
		{
			public MusicClipName MusicClipName;
			public AudioClip MusicClip;
	
		}
	}
}
