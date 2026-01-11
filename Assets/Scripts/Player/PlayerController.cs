using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public enum JumpState
{
    Floating,
    Grounded
}

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool isHoldingSomething = false;
    [SerializeField] private float horizontalSpeed = 4;
    
    [SerializeField] private float jumpForce = 5;
    [ReadOnly] [SerializeField] private JumpState _jumpState = JumpState.Grounded;
    private Rigidbody2D _rigidbody;
    
    
    private bool _rightSide = false;
    private Vector2 _playerDirection;
    
    [Header("Droid or Human")]
    [SerializeField] private string _inputMap; //Droid ou Human
    
    [Header("InputSystem_Actions")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    private InputAction _moveInput;
    private InputAction _jumpInput;
    
    private Animator _animator;
    private string _currentAnimation = "";
    private string _posfix;
    
    [Header("Audio morte player")]
    [SerializeField] private AudioClip _dieClip;
    [SerializeField] private float _volume, _pitch;
    
    private void OnEnable()
    {
        _inputActionAsset.FindActionMap(_inputMap).Enable();
    }
    
    private void OnDisable()
    {
        _inputActionAsset.FindActionMap(_inputMap).Disable();
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveInput = _inputActionAsset.FindAction(_inputMap+"/Move");
        _jumpInput = _inputActionAsset.FindAction(_inputMap+"/Jump");
        _animator = GetComponent<Animator>();
        _posfix = this.gameObject.tag;
    }

    void Start()
    {
        ChangeAnimation("idle_"+_posfix);
    }

    void Update()
    {
        _playerDirection = _moveInput.ReadValue<Vector2>();
        _rigidbody.linearVelocity = new Vector2(
            _playerDirection.x * horizontalSpeed, 
            _rigidbody.linearVelocity.y
            );
        
        if (_jumpState == JumpState.Grounded)
            ChangeAnimation(Mathf.Abs(_rigidbody.linearVelocity.x) > 0 ? "walking_"+_posfix : "idle_"+_posfix);

        if (_jumpInput.WasPressedThisFrame() && _jumpState == JumpState.Grounded)
            Jump();
        
        if (_playerDirection.x < 0 && !_rightSide || _playerDirection.x > 0 && _rightSide) 
            TurnPlayer();
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _jumpState = JumpState.Grounded;
            ChangeAnimation("idle_"+_posfix);
        }
    }
    
    private void TurnPlayer()
    {
        _rightSide = !_rightSide;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {   
        _jumpState = JumpState.Floating;
        ChangeAnimation("jumping_"+_posfix);
        _rigidbody.AddForce(new Vector2(0f, jumpForce));
    }

    public string GetInputMapName() => _inputMap;

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

        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.isKinematic = true;
        
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        AudioManager.instance.PlayAudio(_dieClip,_volume,_pitch);
        IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicao("Start",indexScene,1.5f);
    }
}
