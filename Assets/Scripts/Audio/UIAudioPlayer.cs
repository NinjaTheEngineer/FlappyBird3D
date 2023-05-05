using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public enum UISounds { BUTTON_CLICK, BUTTON_CLICK_2};
public class UIAudioPlayer : AudioPlayer {
    public void PlaySound(UISounds uISounds) {
        var logId = "PlaySound";
        var audioIndex = (int)uISounds;
        if(soundPlayTimes.ContainsKey(audioIndex)) {
            if(Time.realtimeSinceStartup - soundPlayTimes[audioIndex] < soundDelay) {
                logd(logId, "Sound="+(UISounds)audioIndex+" was played less than "+soundDelay+" seconds ago => no-op");
                return;
            }
        }
        PlaySound(audioIndex);
    }
}
