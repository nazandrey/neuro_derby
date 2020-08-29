using NUnit.Framework;
using Scripts.RatingSystem;

namespace Tests.EditMode
{
    [TestFixture]
    public class ScoreStorageTests
    {
        [Test]
        public void GetAllScores_Init_EmptyStorage()
        {
            var scoreStorage = new ScoreStorage();

            CollectionAssert.IsEmpty(scoreStorage.GetAllScores());
        }

        [Test]
        public void TryAddScore_NoName_StorageWithOneEntry()
        {
            var scoreStorage = new ScoreStorage();

            var isAdded = scoreStorage.TryAddScore("", default);

            Assert.IsTrue(isAdded);
        }

        [Test]
        public void TryAddScore_SameName_StorageWithOneEntry()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);
            var rating1 = 1;
            var rating2 = 2;

            var isAdded = scoreStorage.TryAddScore(scoreId, rating1);
            var isAddedAgain = scoreStorage.TryAddScore(scoreId, rating2);

            Assert.IsTrue(isAdded);
            Assert.IsFalse(isAddedAgain);
        }


        [Test]
        public void GetAllScores_ScoresAdded_NonEmptyStorage()
        {
            var scoreStorage = new ScoreStorage();
            const string score1Id = nameof(score1Id);
            const string score2Id = nameof(score2Id);
            var rating1 = 1;
            var rating2 = 2;

            scoreStorage.TryAddScore(score1Id, rating1);
            scoreStorage.TryAddScore(score2Id, rating2);

            var scores = scoreStorage.GetAllScores();
            CollectionAssert.IsNotEmpty(scores);
            CollectionAssert.Contains(scores, rating1);
            CollectionAssert.Contains(scores, rating2);
        }

        [Test]
        public void GetScore_GotScore()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);
            var initRating = 1;
            scoreStorage.TryAddScore(scoreId, initRating);

            var scoreFromStorage = scoreStorage.GetScore(scoreId);

            Assert.AreEqual(initRating, scoreFromStorage);
        }

        [Test]
        public void TryUpdateScore_ScoreExists_ScoreUpdated()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);
            var initRating = 1;
            scoreStorage.TryAddScore(scoreId, initRating);
            var targetRating = 2;

            var isUpdated = scoreStorage.TryUpdateScore(scoreId, targetRating);

            var scoreFromStorage = scoreStorage.GetScore(scoreId);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(targetRating, scoreFromStorage);
        }

        [Test]
        public void UpdateScore_NoScore_ScoreNotUpdated()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);

            var isUpdated = scoreStorage.TryUpdateScore(scoreId, default);

            Assert.IsFalse(isUpdated);
        }
    }
}