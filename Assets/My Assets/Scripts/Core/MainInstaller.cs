using Glicko2;
using NeuroDerby.Players;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using Zenject;

namespace NeuroDerby.Core
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScoreStorage<string, Player>>().AsCached();
            Container.BindInterfacesTo<PlayerNumToNameConverter>().AsCached();
            Container.BindInterfacesTo<PlayersSaver<string>>().AsCached();
            Container.Bind<RatingCalculator>().FromNew().AsCached();
            
            Container.BindInterfacesTo<GlickoScoreUpdater<string>>().AsCached();
        }
    }
}