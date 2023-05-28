using System;
using BlackBall.Platforms;
using BlackBall.Platforms.ConcretePlatforms;
using DG.Tweening;
using Mobik.Common.Core;
using Mobik.Common.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlackBall
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
    public class BallController : MonoBehaviourCached
    {
        public event Action? Died;
        [SerializeField] private float _acceleration = 50f;
        [SerializeField] private float _maxSpeed = 1f;
        [SerializeField] private ParticleSystem _deathEffect = null!;
        [SerializeField] private ParticleSystem _hitEffect = null!;
        [SerializeField] private Camera _camera = null!;
        [SerializeField] private PlatformsSpawner _platformsSpawner = null!;

        private PlayerInput _playerInput = null!;
        private Rigidbody2D _rigidbody = null!;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float velocityX = GetMovementVelocityX();
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

        public void TriggerDeath()
        {
            Died?.Invoke();
            _deathEffect.Play();
            ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play("Death");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
        }

        public void RevivePlayerBall()
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<TrailRenderer>().enabled = true;
            transform.position = _platformsSpawner.LowestPlatform.BonusSpawnPoint.position;
        }

        private float GetMovementVelocityX()
        {
            float screenPositionX = _playerInput.actions["TouchPositionX"].ReadValue<float>();
            bool isPressed = _playerInput.actions["IsTouching"].IsPressed();
            Vector3 touchWorldPosition =
                _camera.ScreenToWorldPoint(new Vector3(screenPositionX, 0f, _camera.nearClipPlane));

            float sign = 0f;
            if (isPressed && !touchWorldPosition.x.EqualsApproximately(transform.position.x, 0.01f))
            {
                sign = (touchWorldPosition.x - transform.position.x).GetSign();
            }

            return sign * _maxSpeed;
        }
    }
}