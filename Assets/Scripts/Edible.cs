using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Edibles/EdibleData", fileName = "EdibleData")]
public class EdibleData : ScriptableObject
{
    [Tooltip("Identifier to be used for recycling purposes. Each different edible should have a different PoolId")]
    public int PoolId;
    
    [Tooltip("Amount of points to give to the player when eaten")]
    public int Points;
}

// When spawning edibles we need to make sure a SpriteRenderer is there, so setup requirements here to not
// end up with null references down the line
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveDown))]
public class Edible : MonoBehaviour
{
    public EdibleData Data;

    private MoveDown _moveDown;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _moveDown = GetComponent<MoveDown>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void DisableMovement()
    {
        _moveDown.enabled = false;
    }

    public void EnableMovement()
    {
        _moveDown.enabled = true;
    }

    public SpriteRenderer GetRenderer()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        return _spriteRenderer;
    }
}
