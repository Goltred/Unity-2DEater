using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameEventEdible onEdibleEaten;
    private Vector2 _input;
    private SpriteRenderer _spriteRenderer;
    private float _spriteHalfSize;
    private float _leftEdgeWorldPos;
    private float _rightEdgeWorldPos;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Used for calculating out of world bounds
        _spriteHalfSize = _spriteRenderer.size.x / 2;
        _leftEdgeWorldPos = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        _rightEdgeWorldPos = Camera.main.ViewportToWorldPoint(Vector3.one).x;
    }
    
    // Assigned in the PlayerInput controller in the inspector
    public void OnMove(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (_input.magnitude == 0)
            return;
        
        transform.Translate(_input * movementSpeed * Time.deltaTime);
        
        if (Camera.main.WorldToViewportPoint(_spriteRenderer.bounds.min).x < 0)
        {
            EnsureInsidePlayArea();
        } 
        else if (Camera.main.WorldToViewportPoint(_spriteRenderer.bounds.max).x > 1)
        {
            EnsureInsidePlayArea();
        }
    }

    private void EnsureInsidePlayArea()
    {
        var xValue = Mathf.Clamp(transform.position.x, _leftEdgeWorldPos + _spriteHalfSize, _rightEdgeWorldPos - _spriteHalfSize);
        transform.position = new Vector3(xValue, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // We only want to trigger the event when the other collision is an Edible.
        // This prepares us for the future in case we add other types that need to trigger different events
        if (col.gameObject.TryGetComponent<Edible>(out var edible))
            onEdibleEaten?.Trigger(edible);
    }
}
