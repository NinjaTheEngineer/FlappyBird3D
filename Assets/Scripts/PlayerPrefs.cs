using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public static class PlayerPrefs {
    public enum Key {
        Highscore,
    }
    public static void SetFloat(Key key, float value) {
        string logId = "PlayerPrefs::SetFloat";
        Utils.logt(logId, "Setting "+key+" to "+value);
        UnityEngine.PlayerPrefs.SetFloat(key.ToString(), value);
    }
    public static float GetFloat(Key key) {
        return UnityEngine.PlayerPrefs.GetFloat(key.ToString());
    }
    public static void SetInt(Key key, int value) {
        string logId = "PlayerPrefs::SetInt";
        Utils.logt(logId, "Setting "+key+" to "+value);
        UnityEngine.PlayerPrefs.SetInt(key.ToString(), value);
    }
    public static int GetInt(Key key) {
        return UnityEngine.PlayerPrefs.GetInt(key.ToString());
    }
    public static void SetString(Key key, string value) {
        string logId = "PlayerPrefs::SetInt";
        if(string.IsNullOrEmpty(value)) {
            Utils.logw(logId, "Value="+value.logf()+" => no-op");
            return;
        }
        Utils.logt(logId, "Setting "+key+" to "+value);
        UnityEngine.PlayerPrefs.SetString(key.ToString(), value);
    }
    public static string GetString(Key key) {
        return UnityEngine.PlayerPrefs.GetString(key.ToString());
    }
    public static void DeleteKey(Key key) {
        string logId = "PlayerPrefs::DeleteKey";
        Utils.logt(logId, "Deleting Key="+key);
        UnityEngine.PlayerPrefs.DeleteKey(key.ToString());
    }
}