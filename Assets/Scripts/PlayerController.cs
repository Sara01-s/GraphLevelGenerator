using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
internal sealed class PlayerController : MonoBehaviour {

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField, Min(0)] private float _rayToGroundDistance;
    [SerializeField, Min(0)] private float _playerSpeed;
    [SerializeField, Min(0)] private float _rotationSpeed;
    [SerializeField, Min(0)] private float _jumpHeight;
    [SerializeField, Range(-50.0f, -1.0f)] private float _gravityScale;

    private bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, _rayToGroundDistance, _groundLayer);

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    
    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if (transform.position.y < -15.0f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar si me caigo XD
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        moveDirection = _cameraTransform.forward * moveDirection.z + _cameraTransform.right * moveDirection.x;
        moveDirection.y = 0.0f;

        if (IsGrounded && _playerVelocity.y < 0.0f)
            _playerVelocity.y = 0.0f;

        _controller.Move((moveDirection * _playerSpeed) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded) {
            _playerVelocity.y += Mathf.Sqrt((_jumpHeight * -3.0f) * _gravityScale);
        }
        
        _playerVelocity.y += _gravityScale * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        if (moveDirection != Vector3.zero) {
            var rotateTo = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;   
            var finalRotation = Quaternion.Euler(0.0f, rotateTo, 0.0f);

            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, Time.deltaTime * _rotationSpeed);
        }
 
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.down * _rayToGroundDistance);
    }

}
