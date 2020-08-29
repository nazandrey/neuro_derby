using NUnit.Framework;
using Scripts.Glicko;
using Scripts.RatingSystem;

namespace Tests.EditMode
{
    [TestFixture]
    public class PlayerScoreStorageTests
    {
        [Test]
        public void GetAllScores_Init_EmptyStorage()
        {
            var playerScoreStorage = new PlayerScoreStorage();

            CollectionAssert.IsEmpty(playerScoreStorage.GetAllScores());
        }

        [Test]
        public void TryAddScore_NoName_StorageWithOneEntry()
        {
            var playerScoreStorage = new PlayerScoreStorage();
            var player = new PlayerDto();

            var isAdded = playerScoreStorage.TryAddScore(player);

            Assert.IsTrue(isAdded);
        }

        [Test]
        public void TryAddScore_SameName_StorageWithOneEntry()
        {
            var playerScoreStorage = new PlayerScoreStorage();
            var player = new PlayerDto();

            var isAdded = playerScoreStorage.TryAddScore(player);
            var isAddedAgain = playerScoreStorage.TryAddScore(player);

            Assert.IsTrue(isAdded);
            Assert.IsFalse(isAddedAgain);
        }


        [Test]
        public void GetAllScores_PlayersAdded_NonEmptyStorage()
        {
            var playerScoreStorage = new PlayerScoreStorage();
            var player1 = new PlayerDto
            {
                Name = "1"
            };
            var player2 = new PlayerDto 
            {
                Name = "2"
            };

            playerScoreStorage.TryAddScore(player1);
            playerScoreStorage.TryAddScore(player2);

            var scores = playerScoreStorage.GetAllScores();
            CollectionAssert.IsNotEmpty(scores);
            CollectionAssert.Contains(scores, player1);
            CollectionAssert.Contains(scores, player2);
        }

        [Test]
        public void GetScore_GotScore()
        {
            var playerScoreStorage = new PlayerScoreStorage();
            const string playerName = nameof(playerName);
            var player = new PlayerDto
            {
                Name = playerName
            };
            playerScoreStorage.TryAddScore(player);

            var playerFromStorage = playerScoreStorage.GetScore(player.Name);

            Assert.AreEqual(player, playerFromStorage);
        }

        [Test]
        public void TryUpdateScore_PlayerExists_ScoreUpdated()
        {
            var playerScoreStorage = new PlayerScoreStorage();
            const string playerName = nameof(playerName);
            var player = new PlayerDto
            {
                Name = playerName
            };
            playerScoreStorage.TryAddScore(player);
            var targetRating = 2;
            var newPlayer = new PlayerDto
            {
                Name = playerName,
                Rating = targetRating
            };

            var isUpdated = playerScoreStorage.TryUpdateScore(newPlayer);

            var playerFromStorage = playerScoreStorage.GetScore(player.Name);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(targetRating, playerFromStorage.Rating);
        }

        [Test]
        public void UpdateScore_NoPlayer_ScoreNotUpdated()
        {
            var playerScoreStorage = new PlayerScoreStorage();
            const string playerName = nameof(playerName);
            var newPlayer = new PlayerDto
            {
                Name = playerName
            };

            var isUpdated = playerScoreStorage.TryUpdateScore(newPlayer);

            Assert.IsFalse(isUpdated);
        }
    }
}