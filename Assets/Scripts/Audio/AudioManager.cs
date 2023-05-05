using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class AudioManager : NinjaMonoBehaviour {
    public static AudioManager Instance;
    public AudioClip backgroundMusic;
    public PlayerAudioPlayer playerAudioPlayer;
    public UIAudioPlayer uiAudioPlayer;
    public EagleAudioPlayer eagleAudioPlayer;
    [SerializeField] private AudioSource musicSource;
    private void Awake() {
        var logId = "Awake";
        Instance = this;
        /*if(Instance==null) {
            logd(logId,"Instance="+this.logf());
        } else {
            logd(logId,"Instance="+Instance.logf()+" => destroying gameObject="+gameObject.logf());
            Destroy(gameObject);
        }*/
        PlayBackgroundMusic();
    }
    public void PlayBackgroundMusic() {
        var logId = "PlayBackgroundMusic";
        if(backgroundMusic==null) {
            logw(logId, "BackgroundMusic="+backgroundMusic.logf()+" => no-op");
            return;
        }
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    bool playingSound = false;
    public void PlayPlayerSound(PlayerSounds sound) {
        var logId = "PlayPlayerSound";
        if(playerAudioPlayer==null || playingSound) {
            logw(logId, "AudioPlayer="+playerAudioPlayer.logf()+" PlayingSound="+playingSound+" => no-op");
            return;
        }
        playingSound = true;
        playerAudioPlayer.PlaySound(sound);
        playingSound = false;
    }
    public void PlayUISound(UISounds sound) {
        var logId = "PlayUISound";
        if(uiAudioPlayer==null || playingSound) {
            logw(logId, "AudioPlayer="+uiAudioPlayer.logf()+" PlayingSound="+playingSound+" => no-op");
            return;
        }
        playingSound = true;
        uiAudioPlayer.PlaySound(sound);
        playingSound = false;
    }
    public void PlayEagleSound(EagleSounds sound) {
        var logId = "PlayEagleSound";
        if(eagleAudioPlayer==null || playingSound) {
            logw(logId, "AudioPlayer="+eagleAudioPlayer.logf()+" PlayingSound="+playingSound+" => no-op");
            return;
        }
        playingSound = true;
        eagleAudioPlayer.PlaySound(sound);
        playingSound = false;
    }

    private void OnDestroy() {
        logd("OnDestroy", "Being destroyed");
    }
}
