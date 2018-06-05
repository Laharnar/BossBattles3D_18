using UnityEngine;
[System.Serializable]
public class CooldownAbility {
    public float rate=2f;
    public Transform pref;
    float t;

    internal bool IsReady() {
        if (Time.time > t) {
            t = Time.time + rate;
            return true;
        }
        return false;
    }
}
