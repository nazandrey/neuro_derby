using System.Collections.Generic;
using Glicko2;
using NeuroDerby.Players;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using NUnit.Framework;
using Zenject;

namespace Tests.EditMode
{
    [TestFixture]
    public class PlayersLoaderTests : ZenjectUnitTestFixture
    {
        private const string Player1Name = nameof(Player1Name);
        private const string Player2Name = nameof(Player2Name);

        [Inject] private IScoreStorage<string, Player> _scoreStorage;
        [Inject] private PlayersLoader _playersLoader;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Container.BindInterfacesTo<ScoreStorage<string, Player>>().AsCached();
            Container.Bind<RatingCalculator>().AsCached();
            Container.BindInterfacesTo<TestPlayersDtoLoader>().AsCached();
            
            Container.Bind<PlayersLoader>().AsCached();

            Container.Inject(this);
        }

        [Test]
        public void Load_GotPlayersInStorage()
        {
            CollectionAssert.IsEmpty(_scoreStorage.GetAllScores());
            
            _playersLoader.Load();
            
            Assert.IsTrue(_scoreStorage.TryGetScore(Player1Name, out _));
            Assert.IsTrue(_scoreStorage.TryGetScore(Player2Name, out _));
        }
        
        private class TestPlayersDtoLoader : IPlayersDtoLoader
        {
            public List<PlayerDto> Load()
            {
                return new List<PlayerDto>
                {
                    new PlayerDto{ Name = Player1Name},
                    new PlayerDto{ Name = Player2Name},
                };
            }
        }
    }
}