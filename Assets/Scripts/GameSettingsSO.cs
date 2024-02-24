using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Settings", fileName = "Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("General settings")]
    public float levelTime = 60;
    
    [Header("Edible spawn variables")]
    public float minSpawnTime;
    public float maxSpawnTime;
    public float minEdibleSpeed;
    public float maxEdibleSpeed;
}
