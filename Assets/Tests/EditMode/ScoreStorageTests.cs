using NUnit.Framework;
using Scripts.RatingSystem;
using System.Linq;

namespace Tests.EditMode
{
    [TestFixture]
    public class ScoreStorageTests
    {
        private const string ScoreId = nameof(ScoreId);
        private const string Score1Id = nameof(Score1Id);
        private const string Score2Id = nameof(Score2Id);
        private const double score1 = 1;
        private const double score2 = 2;

        private ScoreStorage<string, double> _scoreStorage;

        [SetUp]
        public void Setup()
        {
            _scoreStorage = new ScoreStorage<string, double>();
        }

        [Test]
        public void GetAllScores_Init_EmptyStorage()
        {
            CollectionAssert.IsEmpty(_scoreStorage.GetAllScores());
        }

        [Test]
        public void TryAddScore_IdNotAdded_StorageWithOneEntry()
        {
            var isAdded = _scoreStorage.TryAddScore(ScoreId, score1);

            Assert.IsTrue(isAdded);
            var scoreFromStorage = _scoreStorage.GetScore(ScoreId);
            Assert.AreEqual(score1, scoreFromStorage);
        }

        [Test]
        public void TryAddScore_IdAlreadyAdded_StorageWithOneEntry()
        {
            var isAdded = _scoreStorage.TryAddScore(ScoreId, score1);
            var isAddedAgain = _scoreStorage.TryAddScore(ScoreId, score2);

            Assert.IsTrue(isAdded);
            Assert.IsFalse(isAddedAgain);
            Assert.AreNotEqual(score1, score2);

            var scores = _scoreStorage.GetAllScores();
            CollectionAssert.Contains(scores, score1);
            CollectionAssert.DoesNotContain(scores, score2);
        }


        [Test]
        public void GetAllScores_ScoresAdded_NonEmptyStorage()
        {
            _scoreStorage.TryAddScore(Score1Id, score1);
            _scoreStorage.TryAddScore(Score2Id, score2);

            var scores = _scoreStorage.GetAllScores();
            CollectionAssert.IsNotEmpty(scores);
            Assert.AreEqual(2, scores.Count());
            CollectionAssert.Contains(scores, score1);
            CollectionAssert.Contains(scores, score2);
        }

        [Test]
        public void GetScore_GotScore()
        {
            var initScore = score1;
            _scoreStorage.TryAddScore(ScoreId, initScore);

            var scoreFromStorage = _scoreStorage.GetScore(ScoreId);

            Assert.AreEqual(initScore, scoreFromStorage);
        }

        [Test]
        public void TryUpdateScore_ScoreExists_ScoreUpdated()
        {
            var initScore = score1;
            _scoreStorage.TryAddScore(ScoreId, initScore);
            var targetScore = score2;

            var scoreFromStorage = _scoreStorage.GetScore(ScoreId);
            Assert.AreEqual(initScore, scoreFromStorage);

            var isUpdated = _scoreStorage.TryUpdateScore(ScoreId, targetScore);

            scoreFromStorage = _scoreStorage.GetScore(ScoreId);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(targetScore, scoreFromStorage);
        }

        [Test]
        public void UpdateScore_NoScore_ScoreNotUpdated()
        {
            var isUpdated = _scoreStorage.TryUpdateScore(ScoreId, default);

            Assert.IsFalse(isUpdated);
        }
    }
}