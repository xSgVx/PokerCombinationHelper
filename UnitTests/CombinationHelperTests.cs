using CardGameBase;
using CardGameBase.Factories;
using CardGameBase.Models.Comparers;
using Poker.Extensions;
using Poker.Models;

namespace UnitTests
{
    public class CombinationHelperTests
    {
        CardCreator cardCreator;

        [SetUp]
        public void Setup()
        {
            cardCreator = new();
        }

        [Test]
        public void StraightComboTest1()
        {
            var cards = cardCreator.CreateCardsFromString("6c 7d 7h Qh 9h 6s 8h 10c");

            var helper = new CombinationHelper(cards);

            Assert.That(helper.Combination, Is.EqualTo(PokerCombinations.Straight));
        }

        [Test]
        public void StraightComboTest2()
        {
            var cards = cardCreator.CreateCardsFromString("5c 7d 7h 3h 9h 6s 8h 4s");

            var helper = new CombinationHelper(cards);
            Assert.That(helper.Combination, Is.EqualTo(PokerCombinations.Straight));
        }

        [Test]
        public void StraightFlushComboTest()
        {
            var cards = cardCreator.CreateCardsFromString("5h 7d 7h 3s 9h 6h 8h 9d");

            var helper = new CombinationHelper(cards);
            Assert.That(helper.Combination, Is.EqualTo(PokerCombinations.StraightFlush));
        }

        [Test]
        public void FullHouseComboTest()
        {
            var cards = cardCreator.CreateCardsFromString("7c 7d 7h Jh Jd 6s Js 6h");

            var helper = new CombinationHelper(cards);
            Assert.That(helper.Combination, Is.EqualTo(PokerCombinations.FullHouse));
        }

        [Test]
        public void OneWinnerWithStraightTest()
        {
            var board = new Board(cardCreator.CreateCardsFromString("3c 6d 5h"));
            var p1 = new Player("p1", cardCreator.CreateCardsFromString("2c 4d"));  //win combo=2,3,4,5,6
            var p2 = new Player("p2", cardCreator.CreateCardsFromString("6c 6h"));
            var p3 = new Player("p3", cardCreator.CreateCardsFromString("5c Qd"));

            var winners = PokerGameAssistant.Instance.GetWinner(new[] { p1, p2, p3 }, board);

            Assert.That(winners.Count() == 1 && winners.First().Equals(p1));
        }

        [Test]
        public void OneWinnerFromTwoPlayersWithFlushTest()
        {
            var board = new Board(cardCreator.CreateCardsFromString("3h 6h 5h"));
            var p1 = new Player("p1", cardCreator.CreateCardsFromString("2c 2d"));
            var p2 = new Player("p2", cardCreator.CreateCardsFromString("6c 6h"));
            var p3 = new Player("p3", cardCreator.CreateCardsFromString("Jh 5h"));  //win1 hk=J
            var p4 = new Player("p4", cardCreator.CreateCardsFromString("5c Qd"));
            var p5 = new Player("p5", cardCreator.CreateCardsFromString("5h Qh"));  //win2 hk=Q

            var winners = PokerGameAssistant.Instance.GetWinner(new[] { p1, p2, p3, p4, p5 }, board);

            Assert.That(winners.Count() == 1 && winners.Contains(p5));
        }

        [Test]
        public void OneWinnerFromTwoPlayersWithStraightFlushTest()
        {
            var board = new Board(cardCreator.CreateCardsFromString("Jc Qc 10c"));
            var p1 = new Player("p1", cardCreator.CreateCardsFromString("8c 9c"));  //win1 hk=9
            var p2 = new Player("p2", cardCreator.CreateCardsFromString("6c 6h"));
            var p3 = new Player("p3", cardCreator.CreateCardsFromString("9c Kc"));  //win2 hk=K
            var p4 = new Player("p4", cardCreator.CreateCardsFromString("5c Qd"));
            var p5 = new Player("p5", cardCreator.CreateCardsFromString("5c Qd"));

            var winners = PokerGameAssistant.Instance.GetWinner(new[] { p1, p2, p3, p4, p5 }, board);

            Assert.That(winners.Count() == 1 && winners.Contains(p3));
        }

        [Test]
        public void TwoWinnersWithFullHouseTest()
        {
            //same cards = two winners
            var board = new Board(cardCreator.CreateCardsFromString("Jc Js 5d 6c 7s"));
            var p1 = new Player("p1", cardCreator.CreateCardsFromString("Ac 9c"));
            var p2 = new Player("p2", cardCreator.CreateCardsFromString("Jh 5h"));  //win1 hk=5,J
            var p3 = new Player("p3", cardCreator.CreateCardsFromString("9s Kc"));
            var p4 = new Player("p4", cardCreator.CreateCardsFromString("5c Jd"));  //win2 hk=5,J
            var p5 = new Player("p5", cardCreator.CreateCardsFromString("Ks Ad"));

            var winners = PokerGameAssistant.Instance.GetWinner(new[] { p1, p2, p3, p4, p5 }, board);

            Assert.That(winners.Count() == 2 && winners.Contains(p2) && winners.Contains(p4));
        }
    }
}