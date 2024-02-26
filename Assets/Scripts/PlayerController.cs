using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float movementSpeed = 5f;
    
    public GameEventEdible onEdibleEaten;
    
    [Header("Sound Effects")]
    // We want to have more than one audio source to be able to play steps AND the eating SFX at the same time
    public AudioSource mouthAudioSource;
    public List<AudioClip> eatingSfx;

    // Used for calculating out of world bounds
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
        
        _spriteHalfSize = _spriteRenderer.bounds.size.x / 2;
        _minXWorldPos = Camera.main.ViewportToWorldPoint(Vector3.zero).x + _spriteHalfSize;
        _maxXWorldPos = Camera.main.ViewportToWorldPoint(Vector3.one).x - _spriteHalfSize;
    }
    
    // Assigned in the PlayerInput controller in the inspector
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started || context.canceled)
            _input = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (_input.magnitude == 0)
        {
            StopMovementFX();    
            return;
        }
        
        StartMovementFX();
        
        // We want the character to look in the direction of movement
        _spriteRenderer.flipX = _input.x < 0;

        var movement = CalculateMovementWithinPlayArea();
        transform.Translate(movement);
    }

    private Vector3 CalculateMovementWithinPlayArea()
    {
        // Precalculate the player position if we were to apply the input so that we don't rubber band the object
        // with double translations
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

        return movement;
    }

    private void StopMovementFX()
    {
        _animator.SetBool("Moving", false);
    }

    private void StartMovementFX()
    {
        _animator.SetBool("Moving", true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // We only want to trigger the event when the other collision is an Edible.
        // This prepares us for the future in case we add other types that need to trigger different events
        if (col.gameObject.TryGetComponent<Edible>(out var edible))
        {
            onEdibleEaten?.Trigger(edible);
            mouthAudioSource.PlayOneShot(eatingSfx.PickRandom());
        }
    }

    // Hooked up to the Game Over event to stop the player input from affecting the character
    public void GameOver(int _)
    {
        _playerInput.enabled = false;
    }

    // Hooked up to the Start Game event to allow player movement.
    public void StartGame(int _)
    {
        _playerInput.enabled = true;
    }
}
