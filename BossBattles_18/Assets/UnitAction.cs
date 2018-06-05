using System.Collections;
public abstract class UnitAction {
    public static UnitController activeSource;
    public abstract IEnumerator RunAction();
}
