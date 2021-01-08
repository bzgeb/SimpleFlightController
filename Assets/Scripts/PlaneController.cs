using UnityEngine;

public class PlaneController : MonoBehaviour
{
    Transform _transform;
    Rigidbody _rigidbody;

    public float MinThrust = 600f;
    public float MaxThrust = 1200f;
    float _currentThrust;
    public float ThrustIncreaseSpeed = 400f;

    float _deltaPitch;
    public float PitchIncreaseSpeed = 300f;
    
    float _deltaRoll;
    public float RollIncreaseSpeed = 300f;

    void Start()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var thrustDelta = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            thrustDelta += ThrustIncreaseSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            thrustDelta -= ThrustIncreaseSpeed;
        }

        _currentThrust += thrustDelta * Time.deltaTime;
        _currentThrust = Mathf.Clamp(_currentThrust, MinThrust, MaxThrust);

        _deltaPitch = 0f;
        if (Input.GetKey(KeyCode.S))
        {
            _deltaPitch -= PitchIncreaseSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _deltaPitch += PitchIncreaseSpeed;
        }
        _deltaPitch *= Time.deltaTime;

        _deltaRoll = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            _deltaRoll += RollIncreaseSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _deltaRoll -= RollIncreaseSpeed;
        }
        _deltaRoll *= Time.deltaTime;
    }

    void FixedUpdate()
    {
        var localRotation = _transform.localRotation;
        localRotation *= Quaternion.Euler(0f, 0f, _deltaRoll);
        localRotation *= Quaternion.Euler(_deltaPitch, 0f, 0f);
        _transform.localRotation = localRotation;
        _rigidbody.velocity = _transform.forward * (_currentThrust * Time.fixedDeltaTime);
    }
}