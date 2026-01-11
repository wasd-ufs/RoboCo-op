using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private MonoBehaviour inputSource;
    private IPlayerInput input;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    
    private Rigidbody2D _rigidbody;
    public Vector2 PlayerDirection { get; private set; }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (inputSource != null)
            input = inputSource as IPlayerInput;
        else {
            var tmp = GameObject.FindGameObjectWithTag("InputManager");
            input = tmp.GetComponent<InputHandler>();
        }
    }

    private void Start()
    {
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }
    
    private void Update()
    {
        PlayerDirection = input.Move.normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = PlayerDirection * moveSpeed;
    }

    public void StopMovement()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.isKinematic = true;
    }
}
