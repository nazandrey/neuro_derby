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
        private const double Score1 = 1;
        private const double Score2 = 2;

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
        public void GetAllScores_ScoresAdded_NonEmptyStorage()
        {
            _scoreStorage.TryAddScore(Score1Id, Score1);
            _scoreStorage.TryAddScore(Score2Id, Score2);

            var scores = _scoreStorage.GetAllScores();
            CollectionAssert.IsNotEmpty(scores);
            Assert.AreEqual(2, scores.Count());
            CollectionAssert.Contains(scores, Score1);
            CollectionAssert.Contains(scores, Score2);
        }

        [Test]
        public void TryAddScore_IdNotAdded_ScoreAdded()
        {
            var isAdded = _scoreStorage.TryAddScore(ScoreId, Score1);

            Assert.IsTrue(isAdded);
            var scoreFromStorage = GetScoreById(ScoreId);
            Assert.AreEqual(Score1, scoreFromStorage);
        }

        [Test]
        public void TryAddScore_IdEmptyString_ScoreAdded()
        {
            var isAdded = _scoreStorage.TryAddScore(string.Empty, Score1);

            Assert.IsTrue(isAdded);
            _scoreStorage.TryGetScore(string.Empty, out var scoreFromStorage);
            Assert.AreEqual(Score1, scoreFromStorage);
        }

        [Test]
        public void TryAddScore_IdNull_ScoreAdded()
        {
            var isAdded = _scoreStorage.TryAddScore(null, Score1);

            Assert.IsFalse(isAdded);
        }

        [Test]
        public void TryAddScore_IdAlreadyAdded_StorageWithOneEntry()
        {
            var isAdded = _scoreStorage.TryAddScore(ScoreId, Score1);
            var isAddedAgain = _scoreStorage.TryAddScore(ScoreId, Score2);

            Assert.IsTrue(isAdded);
            Assert.IsFalse(isAddedAgain);
            Assert.AreNotEqual(Score1, Score2);

            var scores = _scoreStorage.GetAllScores();
            CollectionAssert.Contains(scores, Score1);
            CollectionAssert.DoesNotContain(scores, Score2);
        }

        [Test]
        public void TryGetScore_HasId_GotScore()
        {
            var initScore = Score1;
            _scoreStorage.TryAddScore(ScoreId, initScore);

            var scoreFromStorage = GetScoreById(ScoreId);

            Assert.AreEqual(initScore, scoreFromStorage);
        }

        [Test]
        public void TryGetScore_NoId_FalseAndDefaultResult()
        {
            var scoreFromStorage = GetScoreById(ScoreId, false);

            Assert.AreEqual((double)default, scoreFromStorage);
        }

        [Test]
        public void TryUpdateScore_ScoreExists_ScoreUpdated()
        {
            var initScore = Score1;
            _scoreStorage.TryAddScore(ScoreId, initScore);
            var targetScore = Score2;

            var scoreFromStorage = GetScoreById(ScoreId);
            Assert.AreEqual(initScore, scoreFromStorage);
            Assert.AreNotEqual(initScore, targetScore);

            var isUpdated = _scoreStorage.TryUpdateScore(ScoreId, targetScore);

            _scoreStorage.TryGetScore(ScoreId, out scoreFromStorage);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(targetScore, scoreFromStorage);
        }

        [Test]
        public void TryUpdateScore_NoScore_ScoreNotUpdated()
        {
            var isUpdated = _scoreStorage.TryUpdateScore(ScoreId, default);

            Assert.IsFalse(isUpdated);
        }

        private double GetScoreById(string scoreId, bool assertIsGotten = true)
        {
            var isGotten = _scoreStorage.TryGetScore(scoreId, out var scoreFromStorage);
            Assert.IsTrue(assertIsGotten ? isGotten : !isGotten);
            return scoreFromStorage;
        }
    }
}