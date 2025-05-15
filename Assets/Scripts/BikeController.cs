using UnityEngine;

public class BikeController : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Rigidbody2D _frontTyreRigidbody2D;
    [SerializeField] private Rigidbody2D _backTyreRigidbody2D;
    [SerializeField] private Rigidbody2D _bikeRigidbody2D;
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
        _moveInput = Input.GetAxisRaw("Vertical");

        if (!_isStartedMoving && IsMovementKeyPressed()) 
        {
            _isStartedMoving = true;
            GameManager.Instance.TimerController.TimerStart();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _bikeRigidbody2D.AddTorque(_tiltTorque);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _bikeRigidbody2D.AddTorque(-_tiltTorque);
        }
    }
    bool IsMovementKeyPressed()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
    private void FixedUpdate()
    {
            _frontTyreRigidbody2D.AddTorque(-_moveInput*_speed*Time.fixedDeltaTime);
            _backTyreRigidbody2D.AddTorque(-_moveInput*_speed*Time.fixedDeltaTime);
            _bikeRigidbody2D.AddTorque(_moveInput*_rotatitonSpeed*Time.fixedDeltaTime);
    }

    public void BikeSetup()
    {
        this.transform.position = _startPosition;
        _bikeRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
    public void TurnOffBike()
    {  
        _isStartedMoving = false;
        this.gameObject.SetActive(false);
    }
    public void StopBikeMovement()
    {
        _bikeRigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    private void OnDisable()
    {
        TurnOffBike();
    }
}
