using System;
using BlackBall.Core;
using BlackBall.Platforms;
using BlackBall.Platforms.ConcretePlatforms;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlackBall
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
    public class BallController : CoreBehaviour
    {
        public event Action? Died;
        [SerializeField] private float _acceleration = 2f;
        [SerializeField] private float _maxSpeed = 1f;
        [SerializeField] private ParticleSystem _deathEffect = null!;
        [SerializeField] private ParticleSystem _hitEffect = null!;
        private PlayerInput _playerInput = null!;
        private Rigidbody2D _rigidbody = null!;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float movementValue = _playerInput.actions["Movement"].ReadValue<Vector2>().x;
            float xVel = 0.9f * _rigidbody.velocity.x + movementValue * _acceleration * Time.deltaTime;
            xVel = Mathf.Clamp(xVel, -_maxSpeed, _maxSpeed);
            _rigidbody.velocity = new Vector2(xVel, _rigidbody.velocity.y);
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

        public void TriggerDeath()
        {
            Died?.Invoke();
            _deathEffect.Play();
            ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play("Death");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
        }
    }
}