using Glicko2;
using NeuroDerby.Game.EventDatas;
using NeuroDerby.Players;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using Zenject;

namespace NeuroDerby.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScoreStorage<string, Player>>().AsSingle();
            Container.BindInterfacesTo<PlayerNumToNameConverter>().AsCached();
            Container.BindInterfacesTo<PlayersSaver<string>>().AsCached();
            Container.BindInterfacesTo<PlayerNameChecker>().AsCached();
            Container.Bind<RatingCalculator>().FromNew().AsCached();
            
            Container.BindInterfacesTo<GlickoScoreUpdater>().AsCached();
            
            Container.BindFactory<GameOverEvent, GameOverEvent.Factory>();
        }
    }
}