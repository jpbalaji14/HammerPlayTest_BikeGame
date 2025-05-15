using UnityEngine;

public class BikeController : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Rigidbody2D _frontTyreRigidbody2D;
    [SerializeField] private Rigidbody2D _backTyreRigidbody2D;
    [SerializeField] private Rigidbody2D _bikeRigidbody2D;
    [SerializeField] private WheelJoint2D _frontWheelJoint;
    [SerializeField] private WheelJoint2D _backWheelJoint;
    [SerializeField] private Transform _bikeBodyTransform;
    [SerializeField] private SpriteRenderer _bikeBodySpriteRenderer;
    [SerializeField] private SpriteRenderer _bikeHeadSpriteRenderer;
    [SerializeField] private Color _bikeCrashColor;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotatitonSpeed;
    [SerializeField] private float _tiltTorque;
    [SerializeField] private float _moveInput;
    [SerializeField] private bool _isStartedMoving;

    private void OnEnable()
    {
        BikeSetup();
    }
    void Update()
    {
        HandleInput();
        CheckStartMovement();
        UpdateBikeSound();
        ApplyTilt();
    }
    private void HandleInput()
    {
        _moveInput = Input.GetAxisRaw("Vertical");
    }
    private void CheckStartMovement()
    {
        if (!_isStartedMoving && IsMovementKeyPressed())
        {
            _isStartedMoving = true;
            GameManager.Instance.TimerController.TimerStart();
        }
    }
    private void UpdateBikeSound()
    {
        if (_moveInput > 0)
            GameManager.Instance.AudioManager.Accelerate();
        else
            GameManager.Instance.AudioManager.Idle();
    }
    bool IsMovementKeyPressed()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }

    private void FixedUpdate()
    {
        ApplyWheelTorque();
    }
    private void ApplyWheelTorque()
    {
        if (_moveInput > 0)
        {
            float _torque = -_moveInput * _speed * Time.fixedDeltaTime;
            _frontTyreRigidbody2D.AddTorque(_torque);
            _backTyreRigidbody2D.AddTorque(_torque);
        }
        else if (_moveInput < 0f)
        {
            float _brakeTorque = _speed * 3f * Time.fixedDeltaTime;
            _frontTyreRigidbody2D.AddTorque(_brakeTorque);
            _backTyreRigidbody2D.AddTorque(_brakeTorque);
        }

        // Prevent reverse wheel spin
        if (_frontTyreRigidbody2D.angularVelocity > 0f)
            _frontTyreRigidbody2D.angularVelocity = 0f;

        if (_backTyreRigidbody2D.angularVelocity > 0f)
            _backTyreRigidbody2D.angularVelocity = 0f;

        _bikeRigidbody2D.AddTorque(_moveInput * _rotatitonSpeed * Time.fixedDeltaTime);
    }
    private void ApplyTilt()
    {
        if (Input.GetKey(KeyCode.A))
            _bikeRigidbody2D.AddTorque(_tiltTorque);
        else if (Input.GetKey(KeyCode.D))
            _bikeRigidbody2D.AddTorque(-_tiltTorque);
    }

    public void BikeSetup()
    {
        GameManager.Instance.UiManager.SetupCamBikeTracking(_bikeBodyTransform);
        GameManager.Instance.AudioManager.SetUpBikeAudioSource(_bikeBodyTransform.GetComponent<AudioSource>());
        GameManager.Instance.UiManager.SetupBikeSprite(_bikeBodySpriteRenderer, _bikeHeadSpriteRenderer);
    }

    public void CrashBike()
    {
        _bikeBodySpriteRenderer.color = _bikeCrashColor;
        _bikeRigidbody2D.bodyType = RigidbodyType2D.Static;
        _frontWheelJoint.enabled = false;
        _backWheelJoint.enabled = false;
        this.GetComponent<AudioSource>().enabled=false;
    }
    public void DestroyBike()
    {
        Destroy(this.gameObject);
    }
  
}
