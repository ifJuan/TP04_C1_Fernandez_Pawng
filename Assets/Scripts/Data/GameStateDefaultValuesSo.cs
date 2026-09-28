using UnityEngine;

[CreateAssetMenu(fileName = "GameStateDefaultValues", menuName = "Data/Game/State")]
public class GameStateDefaultValuesSo : ScriptableObject
{
    [Header("State")]
    public GameStateManager.State state;
    [Header("Players")]
    public float characterPercentageHeight;
    public float characterSpeedModifier;
    [Header("Ball")]
    public UnityEngine.Color ballColor;
    [Header("Game")]
    public int gameDuration;
    public int goalsAmount;
}
