using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float _initialSpeed = 5f;
    [SerializeField]
    private float _angleChangeSpeed = 10f;
    [SerializeField]
    private float _minAngle = 15f;
    [SerializeField]
    private float _maxAngle = 165f;
    [SerializeField]
    private float _lineLength = 2f;

    private Rigidbody2D _ballRB;
    private LineRenderer _angleLR;
    private bool _isBallMoving = false;
    private float _currentAngle;

    private bool _isIncreasingAngle = true;

    private Transform _previousParent;
    private Vector3 _startPosition;


    [Inject]
    public void Construct(PlayerInputs inputs) 
    {
        inputs.OnShootEvent += StartBallMoving;
    }

    private void StartBallMoving() 
    {
        if (_isBallMoving) return;

        SoundManager.Instance.PlaySound(SoundManager.Instance.ShootSound);
        _isBallMoving = true;
        _angleLR.enabled = false;
        _previousParent = transform.parent;
        transform.SetParent(null);
        _ballRB.velocity = AngleToDirection(_currentAngle) * _initialSpeed;

    }
    // Start is called before the first frame update
    void Awake() 
    {
        _ballRB = GetComponent<Rigidbody2D>();
        _ballRB.velocity = Vector2.zero;

        _angleLR = GetComponent<LineRenderer>();
        _currentAngle = _minAngle;

        _startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isIncreasingAngle)
            _currentAngle += _angleChangeSpeed * Time.deltaTime;
        else
            _currentAngle -= _angleChangeSpeed * Time.deltaTime;
        if (_currentAngle > _maxAngle)
            _isIncreasingAngle = false;
        if (_currentAngle < _minAngle)
            _isIncreasingAngle = true;

        UpdateLineRenderer();
    }
    public void ResetBall() 
    {
        _ballRB.velocity = Vector2.zero;
        _isBallMoving = false;
        transform.parent = _previousParent;
        _angleLR.enabled = true;

        transform.localPosition = _startPosition;
    }
    private void UpdateLineRenderer() 
    {
        Vector2 direction = AngleToDirection(_currentAngle);
        _angleLR.SetPosition(0, transform.position); // Начало линии в позиции шарика
        _angleLR.SetPosition(1, transform.position + new Vector3(direction.x, direction.y) * _lineLength);
    }
    private Vector2 AngleToDirection(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }
    public void ModifyBallBounce(float platformSpeed) 
    {
        Vector2 ballDirection = _ballRB.velocity.normalized;


        float bounceAngle = Mathf.Clamp(platformSpeed * 75f, -75f, 75f);


        float newAngle = Mathf.Atan2(ballDirection.y, ballDirection.x) * Mathf.Rad2Deg;
        newAngle += bounceAngle; 

        Vector2 newDirection = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        _ballRB.velocity = newDirection.normalized * _initialSpeed;
    }
}
