using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum JumpState
{
    Floating,
    Grounded
}

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool isHoldingSomething = false;

    [Header("References")]
    [SerializeField] private InputHandler input;

    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;

    [ReadOnly] [SerializeField] private JumpState _jumpState = JumpState.Grounded;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _rightSide = false;
    private Vector2 _playerDirection;

    private string _currentAnimation = "";
    private string _posfix;
    
    [Header("Audio morte player")]
    [SerializeField] private AudioClip _dieClip;
    [SerializeField] private float _volume, _pitch;
    
    private void OnEnable()
    {
        ChangeAnimation("idle_" + _posfix);
    }

    private void Update()
    {
        ReadInput();
        HandleMovement();
        HandleAnimation();
        HandleActions();
        HandleFlip();
    }

    // ===============================
    // INPUT
    // ===============================
    void ReadInput()
    {
        _playerDirection = input.Move;
    }

    void HandleActions()
    {
        if (input.Jump.JustPressed && _jumpState == JumpState.Grounded)
            Jump();
    }

    // ===============================
    // MOVEMENT
    // ===============================
    void HandleMovement()
    {
        _rigidbody.linearVelocity = new Vector2(
            _playerDirection.x * horizontalSpeed,
            _rigidbody.linearVelocity.y
        );
    }

    // ===============================
    // ANIMATION
    // ===============================
    void HandleAnimation()
    {
        if (_jumpState != JumpState.Grounded)
            return;

        bool isMoving = Mathf.Abs(_rigidbody.linearVelocity.x) > 0.01f;
        ChangeAnimation(isMoving ? "walking_" + _posfix : "idle_" + _posfix);
    }

    // ===============================
    // FLIP
    // ===============================
    void HandleFlip()
    {
        if (_playerDirection.x < 0 && !_rightSide ||
            _playerDirection.x > 0 && _rightSide)
        {
            TurnPlayer();
        }
    }

    private void TurnPlayer()
    {
        _rightSide = !_rightSide;
        transform.Rotate(0f, 180f, 0f);
    }

    // ===============================
    // JUMP
    // ===============================
    private void Jump()
    {
        _jumpState = JumpState.Floating;
        ChangeAnimation("jumping_" + _posfix);
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _jumpState = JumpState.Grounded;
            ChangeAnimation("idle_" + _posfix);
        }
    }

    // ===============================
    // ANIMATION CORE
    // ===============================
    private void ChangeAnimation(string animationName, float crossfade = 0.2f)
    {
        if (_currentAnimation == animationName)
            return;

        _currentAnimation = animationName;
        _animator.CrossFade(animationName, crossfade);
    }
    
    /// <summary>
    /// Gerencia as acoes que irao acontecer na morte do player 
    /// </summary>
    public void Die()
    {
        // Desativa os controles de input
        _inputActionAsset.FindActionMap(_inputMap).Disable();

    // ===============================
    // DEATH
    // ===============================
    public void Die()
    {
        enabled = false;          // para o player
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.isKinematic = true;
        
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        AudioManager.instance.PlayAudio(_dieClip,_volume,_pitch);
        IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicao("Start",indexScene,1.5f);
    }
}
