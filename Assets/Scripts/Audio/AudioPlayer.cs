using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
public abstract class AudioPlayer : NinjaMonoBehaviour {
    protected Dictionary<int, float> soundPlayTimes;
    public float soundDelay = 0.2f;
    public static AudioPlayer Instance;
    public SoundEffect soundEffects;
    private void Awake() {
        soundPlayTimes = new Dictionary<int, float>();
    }
    public void PlaySound(int audioIndex) {
        soundEffects.PlaySound(audioIndex, transform.position);
        soundPlayTimes[audioIndex] = Time.realtimeSinceStartup;
    }
}

