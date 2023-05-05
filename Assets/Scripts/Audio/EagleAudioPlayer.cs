using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public enum EagleSounds { FLY, DEATH};
public class EagleAudioPlayer : AudioPlayer {
    public void PlaySound(EagleSounds eagleSound) {
        var logId = "PlaySound";
        var audioIndex = (int)eagleSound;
        if(soundPlayTimes.ContainsKey(audioIndex)) {
            if(Time.realtimeSinceStartup < soundPlayTimes[audioIndex]+soundDelay) {
                logd(logId, "Sound="+(EagleSounds)audioIndex+" was played less than "+soundDelay+" seconds ago => no-op", true);
                return;
            }
        }
        PlaySound(audioIndex);
    }
}
