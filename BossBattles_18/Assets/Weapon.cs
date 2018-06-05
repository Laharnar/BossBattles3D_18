using System.Collections.Generic;
using UnityEngine;
public class Weapon:MonoBehaviour {
    public int alliance;
    public int baseDamage = 5;
    public bool attackPlaying = true;
    List<Transform> hitHistory = new List<Transform>();
    public bool useOnce = false;

    private void OnTriggerEnter(Collider collision) {
        if (attackPlaying) {
            // don't allow multiple hits per attack with edge case collisions.
            if (hitHistory.Contains(collision.transform.root)) {
                return;
            }
            //hitHistory.Add(collision.transform.root);
            UnitController uc = collision.transform.root.GetComponentInChildren<UnitController>();
            if (uc!= null && uc.alliance != alliance) {
                uc.TryDamage(this);
            }
        }
    }

    void OnAttackEnd() {
        attackPlaying = false;
        hitHistory.Clear();
        if (useOnce) {
            Destroy(gameObject);
        }
    }
}
