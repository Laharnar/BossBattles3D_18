using System.Collections;
public abstract class UnitAction {
    /// <summary>
    /// Note that source will change over frames. When you use it in coroutines, chache it.
    /// </summary>
    public static UnitController activeSource;
    public abstract IEnumerator RunAction();
}
