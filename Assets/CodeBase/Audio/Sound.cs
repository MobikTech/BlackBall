using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlackBall.Audio
{
    [Serializable]
    public class Sound 
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField, Range(0, 1f)] public float Volume { get; private set; }
        [field:SerializeField] public List<AudioClip> PossibleAudioClips { get; private set; }
    }
}