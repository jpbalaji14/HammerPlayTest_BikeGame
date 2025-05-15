using UnityEngine;

public class BikeController : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Rigidbody2D _frontTyreRigidbody2D;
    [SerializeField] private Rigidbody2D _backTyreRigidbody2D;
    [SerializeField] private Rigidbody2D _bikeRigidbody2D;
    [SerializeField] private float _speed=150f;
    [SerializeField] private float _rotatitonSpeed=100f;
    public float tiltTorque;
    [SerializeField] private float _moveInput;

    private void Start()
    {
        BikeSetup();
    }
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.A))
        {
            _bikeRigidbody2D.AddTorque(tiltTorque);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _bikeRigidbody2D.AddTorque(-tiltTorque);
        }
    }

    private void FixedUpdate()
    {
            _frontTyreRigidbody2D.AddTorque(-_moveInput*_speed*Time.fixedDeltaTime);
            _backTyreRigidbody2D.AddTorque(-_moveInput*_speed*Time.fixedDeltaTime);
            _bikeRigidbody2D.AddTorque(_moveInput*_rotatitonSpeed*Time.fixedDeltaTime);
    }

    public void BikeSetup()
    {
        transform.position = _startPosition;
    }
    public void TurnOffBike()
    {
        gameObject.SetActive(false);
    }
}
