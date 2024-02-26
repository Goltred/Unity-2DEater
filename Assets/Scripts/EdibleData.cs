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

    [Tooltip("The minimum scale modifier applied to the prefab")]
    [Range(0.3f, 1.25f)]
    public float minScaleModifier = 1;
    
    [Range(0.3f, 1.25f)]
    [Tooltip("The maximumscale applied to the prefab")]
    public float maxScaleModifier = 1;
}
