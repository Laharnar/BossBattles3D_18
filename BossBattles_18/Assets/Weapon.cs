using System.Collections.Generic;
using UnityEngine;
public class Weapon:MonoBehaviour {

    public int baseDamage = 5;
    public bool attackPlaying = true;
    List<Transform> hitHistory = new List<Transform>();

    private void OnTriggerEnter(Collider collision) {
        Debug.Log("asd");
        if (attackPlaying) {
            // don't allow multiple hits per attack with edge case collisions.
            if (hitHistory.Contains(collision.transform.root)) {
                return;
            }
            //hitHistory.Add(collision.transform.root);
            UnitController uc = collision.transform.root.GetComponentInChildren<UnitController>();
            uc.TryDamage(this);
        }
    }

    void OnAttackEnd() {
        attackPlaying = false;
        hitHistory.Clear();
    }
}
