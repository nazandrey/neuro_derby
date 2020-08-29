using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;
using Glicko2;
using Scripts.Glicko;
using NUnit.Framework;

namespace Tests.EditMode
{
    [TestFixture]
    public class GlickoTest
    {
        [Test]
        [Ignore("Дорогой тест, просто на проверку сортировки игроков, не Explicit, т к Rider почему-то всё равно его запускает")]
        public void RatingTest()
        {
            const int playersNum = 11;
            var calculator = new RatingCalculator(/* initVolatility, tau */);
            var players = new List<Player>();
            for (var i = 0; i < playersNum; i++)
            {
                players.Add(new TestPlayer(new Rating(calculator/* , rating, ratingDeviation, volatility */), $"player{i + 1}", i + 1));
            }

            var random = new Random();
            var results = new RatingPeriodResults();
            const int rounds = 10000;
            for (var i = 0; i < rounds; i++)
            {
                var playerNums = Enumerable.Range(0, playersNum).ToList();
                var combinations = new Combinations<int>(playerNums, 2, GenerateOption.WithoutRepetition);
                foreach (var combination in combinations)
                {
                    var player1Num = combination[0];
                    var player2Num = combination[1];
                    var player1 = players[player1Num];
                    var player2 = players[player2Num];

                    var player1VictoryProbTotal = (float)player1Num / (playersNum - 1);
                    var player2VictoryProbTotal = (float)player2Num / (playersNum - 1);

                    var victoryProbTotalDelta = Math.Abs(player1VictoryProbTotal - player2VictoryProbTotal);

                    var drawProb = GetDrawProbability(victoryProbTotalDelta);

                    var noDrawProb = 1 - drawProb;
                    var player1VictoryProb = noDrawProb * player1VictoryProbTotal / (player1VictoryProbTotal + player2VictoryProbTotal);
                    var player1LoseProb = noDrawProb - player1VictoryProb;

                    Assert.AreEqual(1, player1VictoryProb + drawProb + player1LoseProb, 0.001);

                    var toss = random.NextDouble();

                    if (toss < player1VictoryProb)
                    {
                        // player 1 won
                        results.AddResult(player1.RatingInfo, player2.RatingInfo);
                    }
                    else if (toss < player1VictoryProb + drawProb)
                    {
                        // draw
                        results.AddDraw(player1.RatingInfo, player2.RatingInfo);
                    }
                    else if (toss <= player1VictoryProb + drawProb + player1LoseProb)
                    {
                        // player 2 won
                        results.AddResult(player2.RatingInfo, player1.RatingInfo);
                    }
                    else
                    {
                        Console.WriteLine(
                            $"what's this? toss: {toss}, player1VictoryProb: {player1VictoryProb}, drawProb: {drawProb}, player1LoseProb: {player1LoseProb}");
                    }
                }
            }

            calculator.UpdateRatings(results);
            CollectionAssert.IsOrdered(players, new PlayerComparer());
        }

        [TestCase(0.03f, 0.5f)]
        [TestCase(0f, 0.5f)]
        [TestCase(0.05f, 0.4f)]
        [TestCase(0.05f, 0.4f)]
        [TestCase(0.1f, 0.2f)]
        [TestCase(0.2f, 0.1f)]
        [TestCase(0.4f, 0.04f)]
        [TestCase(0.6f, 0.005f)]
        [TestCase(0.8f, 0.001f)]
        [TestCase(2f, 0f)]
        public void GetDrawProbabilityTests(float delta, float expectedResult)
        {
            var result = GetDrawProbability(delta);
            Assert.AreEqual(expectedResult, result, 0.02);
        }

        private class PlayerComparer : IComparer<Player>, IComparer
        {
            public int Compare(Player x, Player y)
            {
                if (x == null || y == null)
                    return 0;
                return (int)Math.Round(x.Rating - y.Rating, 0);
            }

            public int Compare(object x, object y)
            {
                return Compare(x as Player, y as Player);
            }
        }

        private float GetDrawProbability(float victoryProbTotalDelta)
        {
            // https://planetcalc.com/5992/
            // Коэффициент выведен по графику и примерными, кажущимися адекватными цифрам:
            //	delta	drawProb
            //	0%		50%
            //	5%		40%
            //	10%		20%
            //	20%		10%
            //	40%		4%
            //	60%		0.5%
            //	80%		0.1%
            // 100%		0%
            const float fiftyPercentPoint = 0.0422f;
            if (victoryProbTotalDelta < fiftyPercentPoint) return 0.5f;
            const float coeff = 0.022f;
            return coeff * (1 / victoryProbTotalDelta - 1);
        }

        private class TestPlayer : Player
        {
            public TestPlayer(Rating rating, string name, int num) : base(rating, name)
            {
                Num = num;
            }

            public int Num { get; }
        }
    }
}