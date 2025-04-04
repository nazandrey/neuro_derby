using UnityEngine;
using Zenject;

namespace NeuroDerby.Game
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private Bullet.Pool _bulletPool;

        [Inject]
        public void Construct(DifficultyConfig difficultyConfig, Bullet.Pool bulletPool)
        {
            _speed = difficultyConfig.BulletSpeed;
            _damage = difficultyConfig.BulletDamage;
            _bulletPool = bulletPool;
        }
        
        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            var playerHealth = collision2D.gameObject.GetComponent<Health>();
            var wall = collision2D.gameObject.GetComponent<Wall>();
            if (playerHealth != null)
            {
                if (!GameState.IsOver)
                    playerHealth.Hurt(_damage);
                _bulletPool.Despawn(this);
            }
            else if (wall != null)
            {
                var point = collision2D.contacts[0];
                var currDirection = transform.TransformDirection(Vector2.up);
                var newDirection = Vector2.Reflect(currDirection, point.normal);
                transform.rotation = Quaternion.FromToRotation(Vector2.up, newDirection);
            }
        }

        private void FixedUpdate()
        {
            transform.position += transform.up * _speed;
        }

        public class Pool : MonoMemoryPool<Bullet>
        {
        }
    }
}