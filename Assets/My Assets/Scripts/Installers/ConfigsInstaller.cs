using NeuroDerby.FileOperations;
using UnityEngine;
using Zenject;

namespace NeuroDerby.Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "NeuroDerby/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] private PathConfig pathConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(pathConfig);
        }
    }
}