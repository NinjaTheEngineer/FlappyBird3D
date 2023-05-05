using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

[CreateAssetMenu(fileName = "SoundEffect", menuName = "Sounds/Create New Sound Effect")]
public class SoundEffect : ScriptableObject {
    public AudioClip[] clips;
    public Vector2 volume = new Vector2(0.5f, 0.5f);
    public Vector2 pitch = new Vector2(1f, 1f);

    [Range(1, 100)]
    public int minDistance = 1, maxDistance = 5;
    private int currentSoundIndex;
    private AudioSource lastSource;
    private Vector3 currentPosition;
    public void StopLastSound(int soundIndex) {
        if (lastSource != null) {
            if (lastSource.clip.Equals(clips[soundIndex])) {
                lastSource.Stop();
            }
        }
    }
    public void PlaySound(int soundIndex, Vector3 position) {
        currentPosition = position;
        currentSoundIndex = (int)soundIndex;
        Play();
    }
    public void PlaySound(int soundIndex, AudioSource audioSource = null) {
        currentSoundIndex = (int)soundIndex;
        Play(audioSource);
    }
    private AudioSource Play(AudioSource audioSourceParam = null) {
        var logId = "Play";
        if(clips.Length<=currentSoundIndex) {
            Debug.LogWarning($"Missing sound clips for {name}");
            return null;
        }

        AudioSource source = audioSourceParam;
        bool destroySource = false;
        if(source==null) {
            GameObject obj = new GameObject("Sound", typeof(AudioSource));
            if(currentPosition == null)
                currentPosition = Camera.main.transform.position;
            obj.transform.position = currentPosition;
            source = obj.GetComponent<AudioSource>();
            destroySource = true;
        }
        source.Stop();
        var clip = clips[currentSoundIndex];
        source.spatialBlend = 1;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = AudioRolloffMode.Custom;
        source.clip = clip;
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);

        source.Play();
        Utils.logd(logId, "Playing clip="+clip.logf()+" from "+source.name.logf());
        lastSource = source;
        if(destroySource) {
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }

        return source;
    }
}

