using NeuroDerby.FileOperations;
using NeuroDerby.Game;
using UnityEngine;
using Zenject;

namespace NeuroDerby.Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "NeuroDerby/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] private PathConfig pathConfig;
        [SerializeField] private BulletSpawnerConfig bulletSpawnerConfig;
        [SerializeField] private DifficultyConfig difficultyConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(pathConfig);
            Container.BindInstance(bulletSpawnerConfig);
            Container.BindInstance(difficultyConfig);
        }
    }
}