using System;
using System.Collections.Generic;
using System.Linq;
using BlackBall.Common.Extensions;
using BlackBall.Core;
using UnityEngine;

namespace BlackBall.Audio
{
    public class SoundsPlayer : CoreBehaviour
    {
        [SerializeField] private List<Sound> _sounds = null!;
        private Dictionary<string, AudioSource> _sources = new Dictionary<string, AudioSource>();

        public void Play(string soundName)
        {
            try
            {
                Sound sound = _sounds.First(sound => sound.Name == soundName);

                AudioClip randomAudioClip = sound.PossibleAudioClips.GetRandomElement().value;

                AudioSource audioSource = GetSource(soundName);
                audioSource.volume = sound.Volume;
                audioSource.PlayOneShot(randomAudioClip);
            }
            catch
            {
                throw new Exception($"Such sound name doesn't exists: \"{soundName}\"");
            }
        }

        private AudioSource GetSource(string soundName)
        {
            if (_sources.ContainsKey(soundName)) return _sources[soundName];
            
            var source = gameObject.AddComponent<AudioSource>();
            _sources.Add(soundName, source);
            return source;
        }
    }
}