using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public enum PlayerSounds {FLY, FIX, CRASH}
public class PlayerAudioPlayer : AudioPlayer {
    public void PlaySound(PlayerSounds playerSound) {
        var logId = "PlaySound";
        var audioIndex = (int) playerSound;
        if(soundPlayTimes.ContainsKey(audioIndex)) {
            if(Time.realtimeSinceStartup - soundPlayTimes[audioIndex] < soundDelay) {
                logd(logId, "Sound="+(PlayerSounds)audioIndex+" was played less than "+soundDelay+" seconds ago => no-op", true);
                return;
            }
        }
        PlaySound(audioIndex);
    }
}
