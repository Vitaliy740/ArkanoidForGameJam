using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlatformController : MonoBehaviour
{
    [Inject]
    private PlayerInputs _inputs;

    private Vector2 _lastMousePosition;

    private float _moveDelta;
    private void FixedUpdate()
    {
        if (_inputs.MousePosition != _lastMousePosition)
        {
            _moveDelta = _inputs.MousePosition.x - _lastMousePosition.x;
            float newXPos = Mathf.Clamp(_inputs.MousePosition.x, -7.5f, 7.5f);
            transform.position = new Vector2(newXPos, transform.position.y);
            _lastMousePosition = _inputs.MousePosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallController collisionBall = collision.gameObject.GetComponent<BallController>();
        if (collisionBall) 
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.PlatformHitSound);
            collisionBall.ModifyBallBounce(_moveDelta);
        }
    }


}
