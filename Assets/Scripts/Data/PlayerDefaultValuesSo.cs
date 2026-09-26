using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefaultValues", menuName = "Data/Game/Player")]
public class PlayerDefaultValuesSo : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    [Header("Appearance")]
    public UnityEngine.Color initialColor;
    [Header("Controls")]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    [Header("Position")]
    public Appearance.Side side;
}
