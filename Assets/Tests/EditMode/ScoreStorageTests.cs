using NUnit.Framework;
using Scripts.RatingSystem;
using System.Linq;

namespace Tests.EditMode
{
    [TestFixture]
    public class ScoreStorageTests
    {
        [Test]
        public void GetAllScores_Init_EmptyStorage()
        {
            var scoreStorage = new ScoreStorage<string, double>();

            CollectionAssert.IsEmpty(scoreStorage.GetAllScores());
        }

        [Test]
        public void TryAddScore_IdNotAdded_StorageWithOneEntry()
        {
            var scoreStorage = new ScoreStorage<string, double>();
            const string scoreId = nameof(scoreId);
            var score = 1;

            var isAdded = scoreStorage.TryAddScore(scoreId, score);

            Assert.IsTrue(isAdded);
            var scoreFromStorage = scoreStorage.GetScore(scoreId);
            Assert.AreEqual(score, scoreFromStorage);
        }

        [Test]
        public void TryAddScore_IdAlreadyAdded_StorageWithOneEntry()
        {
            var scoreStorage = new ScoreStorage<string, double>();
            const string scoreId = nameof(scoreId);
            var score1 = 1;
            var score2 = 2;

            var isAdded = scoreStorage.TryAddScore(scoreId, score1);
            var isAddedAgain = scoreStorage.TryAddScore(scoreId, score2);

            Assert.IsTrue(isAdded);
            Assert.IsFalse(isAddedAgain);
            Assert.AreNotEqual(score1, score2);

            var scores = scoreStorage.GetAllScores();
            CollectionAssert.Contains(scores, score1);
            CollectionAssert.DoesNotContain(scores, score2);
        }


        [Test]
        public void GetAllScores_ScoresAdded_NonEmptyStorage()
        {
            var scoreStorage = new ScoreStorage<string, double>();
            const string score1Id = nameof(score1Id);
            const string score2Id = nameof(score2Id);
            var score1 = 1;
            var score2 = 2;

            scoreStorage.TryAddScore(score1Id, score1);
            scoreStorage.TryAddScore(score2Id, score2);

            var scores = scoreStorage.GetAllScores();
            CollectionAssert.IsNotEmpty(scores);
            Assert.AreEqual(2, scores.Count());
            CollectionAssert.Contains(scores, score1);
            CollectionAssert.Contains(scores, score2);
        }

        [Test]
        public void GetScore_GotScore()
        {
            var scoreStorage = new ScoreStorage<string, double>();
            const string scoreId = nameof(scoreId);
            var initScore = 1;
            scoreStorage.TryAddScore(scoreId, initScore);

            var scoreFromStorage = scoreStorage.GetScore(scoreId);

            Assert.AreEqual(initScore, scoreFromStorage);
        }

        [Test]
        public void TryUpdateScore_ScoreExists_ScoreUpdated()
        {
            var scoreStorage = new ScoreStorage<string, double>();
            const string scoreId = nameof(scoreId);
            var initScore = 1;
            scoreStorage.TryAddScore(scoreId, initScore);
            var targetScore = 2;

            var scoreFromStorage = scoreStorage.GetScore(scoreId);
            Assert.AreEqual(initScore, scoreFromStorage);

            var isUpdated = scoreStorage.TryUpdateScore(scoreId, targetScore);

            scoreFromStorage = scoreStorage.GetScore(scoreId);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(targetScore, scoreFromStorage);
        }

        [Test]
        public void UpdateScore_NoScore_ScoreNotUpdated()
        {
            var scoreStorage = new ScoreStorage<string, double>();
            const string scoreId = nameof(scoreId);

            var isUpdated = scoreStorage.TryUpdateScore(scoreId, default);

            Assert.IsFalse(isUpdated);
        }
    }
}