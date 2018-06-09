using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager:MonoBehaviour {
    static LevelManager _m;

    public static LevelManager m {
        get {
            if (_m == null) {
                _m = new GameObject().AddComponent<LevelManager>();
            }
            return _m;
        }
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
