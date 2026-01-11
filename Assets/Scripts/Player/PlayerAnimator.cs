using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{   
    [SerializeField] PlayerMovement playerMovement;
    private Vector2 _playerDirection;
    
    private Animator _animator;
    private string _currentAnimation = "";
    
    private bool _rightSide = false;
    private string _posfix;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        ChangeAnimation("idle_" + _posfix);
    }

    private void Update()
    {
        _playerDirection = playerMovement.PlayerDirection;
        var isMoving = _playerDirection.magnitude > 0.01f;
        ChangeAnimation(isMoving ? "walking_" + _posfix : "idle_" + _posfix);
        HandleFlip();
    }
    
    void HandleFlip()
    {
        if (_playerDirection.x < 0 && !_rightSide ||
            _playerDirection.x > 0 && _rightSide)
        {
            TurnPlayer();
        }
    }

    void TurnPlayer()
    {
        _rightSide = !_rightSide;
        transform.Rotate(0f, 180f, 0f);
    }
    
    private void ChangeAnimation(string animationName, float crossfade = 0.2f)
    {
        if (_currentAnimation == animationName)
            return;

        _currentAnimation = animationName;
        _animator.CrossFade(animationName, crossfade);
    }
    
}

