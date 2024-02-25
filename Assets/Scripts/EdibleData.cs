using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Edibles/EdibleData", fileName = "EdibleData")]
public class EdibleData : ScriptableObject
{
    [Tooltip("Identifier to be used for recycling purposes. Each different edible should have a different PoolId")]
    public int poolId;
    
    [Tooltip("Amount of points to give to the player when eaten")]
    public int points;

    [Tooltip("The sprite that will be loaded into the prefab when spawned")]
    public Sprite sprite;
}
