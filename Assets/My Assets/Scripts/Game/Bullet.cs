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

        private void OnTriggerEnter2D(Collider2D other)
        {
            var playerHealth = other.gameObject.GetComponent<Health>();
            if (playerHealth != null)
                playerHealth.Hurt(_damage);
            _bulletPool.Despawn(this);
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