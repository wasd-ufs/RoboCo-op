using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, IPlayerInput
{
    // ===============================
    // INSPECTOR
    // ===============================
    [Header("Input Actions")]
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private string gameplayMapName = "Player";

    // ===============================
    // AÇÕES 
    // ===============================
    private const string MOVE     = "Move";
    private const string LOOK     = "Look";
    private const string ATTACK   = "Attack";
    private const string INTERACT = "Interact";
    private const string CROUCH   = "Crouch";
    private const string JUMP     = "Jump";
    private const string PREVIOUS = "Previous";
    private const string NEXT     = "Next";
    private const string SPRINT   = "Sprint";
    private const string PAUSE    = "Pause";

    // ===============================
    // ESTADOS PÚBLICOS
    // ===============================
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public InputButton Attack   { get; } = new();
    public InputButton Interact { get; } = new();
    public InputButton Crouch   { get; } = new();
    public InputButton Jump     { get; } = new();
    public InputButton Previous { get; } = new();
    public InputButton Next     { get; } = new();
    public InputButton Sprint   { get; } = new();
    public InputButton Pause    { get; } = new();

    // ===============================
    // INPUT SYSTEM
    // ===============================
    private InputActionMap map;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private InputAction interactAction;
    private InputAction crouchAction;
    private InputAction jumpAction;
    private InputAction previousAction;
    private InputAction nextAction;
    private InputAction sprintAction;
    private InputAction pauseAction;

    // ===============================
    // LIFECYCLE
    // ===============================
    private void Awake()
    {
        map = actions.FindActionMap(gameplayMapName, true);

        moveAction     = map.FindAction(MOVE, true);
        lookAction     = map.FindAction(LOOK, true);
        attackAction   = map.FindAction(ATTACK, true);
        interactAction = map.FindAction(INTERACT, true);
        crouchAction   = map.FindAction(CROUCH, true);
        jumpAction     = map.FindAction(JUMP, true);
        previousAction = map.FindAction(PREVIOUS, true);
        nextAction     = map.FindAction(NEXT, true);
        sprintAction   = map.FindAction(SPRINT, true);
        pauseAction   = map.FindAction(PAUSE, true);
    }

    private void OnEnable()
    {
        // VECTORS
        moveAction.performed += OnMove;
        moveAction.canceled  += OnMove;

        lookAction.performed += OnLook;
        lookAction.canceled  += OnLook;

        // BUTTONS
        BindButton(attackAction,   Attack);
        BindButton(interactAction, Interact);
        BindButton(crouchAction,   Crouch);
        BindButton(jumpAction,     Jump);
        BindButton(previousAction, Previous);
        BindButton(nextAction,     Next);
        BindButton(sprintAction,   Sprint);
        BindButton(pauseAction,    Pause);

        map.Enable();
    }

    private void OnDisable()
    {
        map.Disable();
    }

    // ===============================
    // CALLBACKS
    // ===============================
    void OnMove(InputAction.CallbackContext ctx)
    {
        Move = ctx.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext ctx)
    {
        Look = ctx.ReadValue<Vector2>();
    }

    void BindButton(InputAction action, InputButton button)
    {
        action.performed += _ => button.Update(true);
        action.canceled  += _ => button.Update(false);
    }

    // ===============================
    // FRAME RESET
    // ===============================
    private void LateUpdate()
    {
        Attack.ResetFrame();
        Interact.ResetFrame();
        Crouch.ResetFrame();
        Jump.ResetFrame();
        Previous.ResetFrame();
        Next.ResetFrame();
        Sprint.ResetFrame();
        Pause.ResetFrame();
    }
}
