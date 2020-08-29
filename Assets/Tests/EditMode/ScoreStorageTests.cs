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
            var score = new ScoreDto();

            var isAdded = scoreStorage.TryAddScore(score);

            Assert.IsTrue(isAdded);
        }

        [Test]
        public void TryAddScore_SameName_StorageWithOneEntry()
        {
            var scoreStorage = new ScoreStorage();
            var score = new ScoreDto();

            var isAdded = scoreStorage.TryAddScore(score);
            var isAddedAgain = scoreStorage.TryAddScore(score);

            Assert.IsTrue(isAdded);
            Assert.IsFalse(isAddedAgain);
        }


        [Test]
        public void GetAllScores_ScoresAdded_NonEmptyStorage()
        {
            var scoreStorage = new ScoreStorage();
            var score1 = new ScoreDto
            {
                Id = "1"
            };
            var score2 = new ScoreDto
            {
                Id = "2"
            };

            scoreStorage.TryAddScore(score1);
            scoreStorage.TryAddScore(score2);

            var scores = scoreStorage.GetAllScores();
            CollectionAssert.IsNotEmpty(scores);
            CollectionAssert.Contains(scores, score1);
            CollectionAssert.Contains(scores, score2);
        }

        [Test]
        public void GetScore_GotScore()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);
            var score = new ScoreDto
            {
                Id = scoreId
            };
            scoreStorage.TryAddScore(score);

            var scoreFromStorage = scoreStorage.GetScore(score.Id);

            Assert.AreEqual(score, scoreFromStorage);
        }

        [Test]
        public void TryUpdateScore_ScoreExists_ScoreUpdated()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);
            var score = new ScoreDto
            {
                Id = scoreId
            };
            scoreStorage.TryAddScore(score);
            var targetRating = 2;
            var newScore = new ScoreDto
            {
                Id = scoreId,
                Rating = targetRating
            };

            var isUpdated = scoreStorage.TryUpdateScore(newScore);

            var scoreFromStorage = scoreStorage.GetScore(score.Id);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(targetRating, scoreFromStorage.Rating);
        }

        [Test]
        public void UpdateScore_NoScore_ScoreNotUpdated()
        {
            var scoreStorage = new ScoreStorage();
            const string scoreId = nameof(scoreId);
            var newScore = new ScoreDto
            {
                Id = scoreId
            };

            var isUpdated = scoreStorage.TryUpdateScore(newScore);

            Assert.IsFalse(isUpdated);
        }
    }
}