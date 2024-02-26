using UnityEngine;

/// <summary>
/// When spawning edibles we need to make sure a SpriteRenderer is there, so setup requirements here to not
/// end up with null references down the line
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveDown))]
public class Edible : MonoBehaviour
{
    public int points => _data.points;
    public int poolId => _data.poolId;
    
    private EdibleData _data;
    private MoveDown _moveDown;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _moveDown = GetComponent<MoveDown>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Reset the gameobject settings based on the incoming data
    public void Configure(EdibleData data)
    {
        _data = data;
        _spriteRenderer.sprite = data.sprite;

        var targetScaleModifier = Random.Range(data.minScaleModifier, data.maxScaleModifier);
        transform.localScale *= targetScaleModifier;
    }

    public void SetFallSpeed(float speed)
    {
        _moveDown.fallSpeed = speed;
    }
    
    public void DisableMovement()
    {
        _moveDown.enabled = false;
    }

    public void EnableMovement()
    {
        _moveDown.enabled = true;
    }
}
