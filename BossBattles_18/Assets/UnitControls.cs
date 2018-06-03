using UnityEngine;
public abstract class UnitControls :MonoBehaviour{
    public abstract bool[] GetInputs(); // Light, Strong, Block, Dash
    public abstract void PostMoveUpdate(); // For camera on player
    public abstract Vector3 GetRawDirectionInput(); // for movement and dashin, for player = axis raw
    public abstract Vector3 GetDirSmooth(); // for movement
}
