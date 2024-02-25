using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameEventEdible onEdibleEaten;
    
    private Vector2 _input;
    private SpriteRenderer _spriteRenderer;
    private PlayerInput _playerInput;
    private Animator _animator;
    private float _spriteHalfSize;
    private float _minXWorldPos;
    private float _maxXWorldPos;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        
        // Used for calculating out of world bounds
        _spriteHalfSize = _spriteRenderer.bounds.size.x / 2;
        _minXWorldPos = Camera.main.ViewportToWorldPoint(Vector3.zero).x + _spriteHalfSize;
        _maxXWorldPos = Camera.main.ViewportToWorldPoint(Vector3.one).x - _spriteHalfSize;
    }
    
    // Assigned in the PlayerInput controller in the inspector
    public void OnMove(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (_input.magnitude == 0)
        {
            _animator.SetBool("Moving", false);
            return;
        }

        _animator.SetBool("Moving", true);
        
        // We want the character to look in the direction of movement
        _spriteRenderer.flipX = _input.x < 0;

        // Prevent the player from moving outside the viewport area
        var movement = new Vector3(_input.x * movementSpeed * Time.deltaTime, 0, 0);
        var nextPos = transform.position + movement;
        
        // We scale down to only go to the edge
        if (nextPos.x < _minXWorldPos)
        {
            movement.x = transform.position.x - _minXWorldPos;
        } else if (nextPos.x > _maxXWorldPos)
        {
            movement.x = _maxXWorldPos - transform.position.x;
        }
        
        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // We only want to trigger the event when the other collision is an Edible.
        // This prepares us for the future in case we add other types that need to trigger different events
        if (col.gameObject.TryGetComponent<Edible>(out var edible))
            onEdibleEaten?.Trigger(edible);
    }

    public void GameOver(int _)
    {
        _playerInput.enabled = false;
    }

    public void StartGame(int _)
    {
        _playerInput.enabled = true;
    }
}
