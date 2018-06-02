using UnityEngine;
public interface IUnitControls {
    bool[] GetInputs(); // Light, Strong, Block, Dash
    void PostMoveUpdate(); // For camera on player
    Vector3 GetRawDirectionInput(); // for movement and dashin, for player = axis raw
    Vector3 GetDirSmooth(); // for movement
}
