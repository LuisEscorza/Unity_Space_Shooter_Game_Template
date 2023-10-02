using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [field: Header("Misc")]
    private Entity _player;
    private Rigidbody2D _rb;
    private float _movementSpeed;
    private Vector2 _movementInput = new(0, 0);
    private bool _playerCanMove = true;
    #endregion

    private void Awake()
    {
        _player = GetComponent<Entity>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _movementSpeed = _player.MovementSpeed;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, ScreenUtils.ScreenLimitLeft, ScreenUtils.ScreenLimitRight);
        currentPosition.y = Mathf.Clamp(currentPosition.y, ScreenUtils.ScreenLimitBottom, ScreenUtils.ScreenLimitTop);
        transform.position = currentPosition;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!_playerCanMove) { return; }
        if (_movementInput != Vector2.zero) _rb.velocity = _movementSpeed * _movementInput;
        else _rb.velocity = Vector2.zero;
    }

    public void OnMovementInput(InputAction.CallbackContext inputValue)
    {
        _movementInput = inputValue.ReadValue<Vector2>();
    }

    public void SetPlayerCanMove(bool canMove)
    {
        _playerCanMove = canMove;
    }

}
