using NeuroDerby.MenuLogic;
using NeuroDerby.Players;
using Zenject;

namespace NeuroDerby.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle();
            Container.BindInterfacesTo<PlayersLoader>().AsCached();
            Container.BindInterfacesTo<PlayersDtoLoader>().AsCached();
        }
    }
}