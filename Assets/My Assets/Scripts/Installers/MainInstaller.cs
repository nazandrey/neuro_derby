using Glicko2;
using NeuroDerby.Game;
using NeuroDerby.Game.EventDatas;
using NeuroDerby.Players;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using UnityEngine;
using Zenject;

namespace NeuroDerby.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Bullet bulletPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScoreStorage<string, Player>>().AsSingle();
            Container.BindInterfacesTo<PlayerNumToNameConverter>().AsCached();
            Container.BindInterfacesTo<PlayersSaver<string>>().AsCached();
            Container.BindInterfacesTo<PlayerNameChecker>().AsCached();
            Container.BindInterfacesTo<PlayerDataConverter>().AsCached();
            Container.BindInterfacesTo<PlayerNameCleaner>().AsCached();

            Container.Bind<GameState>().AsSingle();
            
            Container.Bind<RatingCalculator>().FromNew().AsCached();

            Container.BindInterfacesTo<GlickoScoreUpdater>().AsCached();
            
            Container.BindFactory<GameOverEvent, GameOverEvent.Factory>();
            
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(30)
                .FromComponentInNewPrefab(bulletPrefab)
                .UnderTransformGroup($"{nameof(Bullet)} Pool");
        }
    }
}