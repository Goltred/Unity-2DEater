using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
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
    
    // Assigned in the inspector
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
        // Make sure we are always inside the play area
        var xValue = Mathf.Clamp(transform.position.x, _leftEdgeWorldPos + _spriteHalfSize, _rightEdgeWorldPos - _spriteHalfSize);
        transform.position = new Vector3(xValue, transform.position.y, transform.position.z);
    }
}
