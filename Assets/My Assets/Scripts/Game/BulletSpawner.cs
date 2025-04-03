using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace NeuroDerby.Game
{
    public class BulletSpawner : MonoBehaviour
    {
        private SpriteRenderer _spawnArea;
        private float _timeBeforeSpawn = 0;

        private BulletSpawnerConfig _bulletSpawnerConfig;
        private Bullet.Pool _bulletPool;
        
        [Inject]
        public void Construct(BulletSpawnerConfig bulletSpawnerConfig, Bullet.Pool bulletPool)
        {
            _bulletSpawnerConfig = bulletSpawnerConfig;
            _bulletPool = bulletPool;
        }

        private void Start()
        {
            _spawnArea = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (_timeBeforeSpawn > 0)
            {
                _timeBeforeSpawn -= Time.deltaTime;
            }
            else
            {
                var bounds = _spawnArea.bounds;
                var bulletSpawnPosition = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    0
                );
                var bulletRotation = Quaternion.Euler(0,0, Random.Range(0, 360));
                var bullet = _bulletPool.Spawn();
                bullet.transform.position = bulletSpawnPosition;
                bullet.transform.rotation = bulletRotation;
                _timeBeforeSpawn = _bulletSpawnerConfig.SpawnRateSeconds;
            }
        }
    }
}