using System;
using BlackBall.Core;
using BlackBall.Platforms;
using BlackBall.Platforms.ConcretePlatforms;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using static BlackBall.Common.Math.MathAddition;

namespace BlackBall
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
    public class BallController : CoreBehaviour
    {
        public event Action? Died;
        [SerializeField] private float _acceleration = 50f;
        [SerializeField] private float _maxSpeed = 1f;
        [SerializeField] private ParticleSystem _deathEffect = null!;
        [SerializeField] private ParticleSystem _hitEffect = null!;
        [SerializeField] private Camera _camera = null!;
        private PlayerInput _playerInput = null!;
        private Rigidbody2D _rigidbody = null!;

        public void TriggerDeath()
        {
            Died?.Invoke();
            _deathEffect.Play();
            ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play("Death");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
        }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // float movementValue = _playerInput.actions["Movement"].ReadValue<Vector2>().normalized.x;
            //
            // // float xVel = movementValue * _maxSpeed;
            // float xVel = 0.9f * _rigidbody.velocity.x + movementValue * _acceleration * Time.deltaTime;
            // xVel = Mathf.Clamp(xVel, -_maxSpeed, _maxSpeed);
            // _rigidbody.velocity = new Vector2(xVel, _rigidbody.velocity.y);

            float velocityX = GetMovementVelocityX();
            
            //todo remove
            Debug.Log(velocityX);
            _rigidbody.velocity = new Vector2(velocityX, _rigidbody.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out PlatformBase platform))
            {
                _hitEffect.Play();
                float lastScaleValue = transform.localScale.x;
                transform.DOScaleY(0.8f * lastScaleValue, 0.08f);
                transform.DOScaleY(lastScaleValue, 0.08f);
                ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play(platform is PlatformBouncy
                    ? "SlimeBallHit"
                    : "DeafBallHit");
            }
        }

        private float GetMovementVelocityX()
        {
            float screenPositionX = _playerInput.actions["TouchPositionX"].ReadValue<float>();
            bool isPressed = _playerInput.actions["IsTouching"].IsPressed();
            Vector3 touchWorldPosition = _camera.ScreenToWorldPoint(new Vector3(screenPositionX, 0f, _camera.nearClipPlane));

            Debug.Log(touchWorldPosition.x);
            float sign = 0f;
            if (isPressed && !AreEqual(touchWorldPosition.x, transform.position.x, 0.01f))
            {
                sign = GetSign(touchWorldPosition.x - transform.position.x);
            }

            return sign * _maxSpeed;
        }
    }
}