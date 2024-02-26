using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Settings", fileName = "Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("General settings")]
    public float levelTime = 60;
    [Tooltip("The number of seconds left on the clock when the Countdown event will be fired.")]
    public int countdownEventSeconds;
    
    [Header("Edible spawn variables")]
    public float minSpawnTime;
    public float maxSpawnTime;
    public float minEdibleSpeed;
    public float maxEdibleSpeed;
}
