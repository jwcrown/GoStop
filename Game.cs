using System;
using System.Collections.Generic;
using System.Text;

namespace Go_stop
{
    public class Game
    {
        Player Player1;
        Player Player2;
        Player CurrentPlayer;
        Player Winner;
        Deck myDeck;

        public void PlayGame()
        {
            GreetPlayers();
            DealCards();
            while (Winner == null)
            {
                CurrentPlayer = WhoseTurn();
                YourTurn(CurrentPlayer);
                if (CurrentPlayer.CalculatePoints() >= 10)
                {
                    GoStop(CurrentPlayer.CalculatePoints());
                }
            }

        }

        public void EnterGame()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "ASCII Art";
            string title = @"
         _              _               _          _            _            _      
        /\ \           /\ \            / /\       /\ \         /\ \         /\ \    
       /  \ \         /  \ \          / /  \      \_\ \       /  \ \       /  \ \   
      / /\ \_\       / /\ \ \        / / /\ \__   /\__ \     / /\ \ \     / /\ \ \  
     / / /\/_/      / / /\ \ \      / / /\ \___\ / /_ \ \   / / /\ \ \   / / /\ \_\ 
    / / / ______   / / /  \ \_\     \ \ \ \/___// / /\ \ \ / / /  \ \_\ / / /_/ / / 
   / / / /\_____\ / / /   / / /      \ \ \     / / /  \/_// / /   / / // / /__\/ /  
  / / /  \/____ // / /   / / /   _    \ \ \   / / /      / / /   / / // / /_____/   
 / / /_____/ / // / /___/ / /   /_/\__/ / /  / / /      / / /___/ / // / /          
/ / /______\/ // / /____\/ /    \ \/___/ /  /_/ /      / / /____\/ // / /           
\/___________/ \/_________/      \_____\/   \_\/       \/_________/ \/_/ ";
            Console.WriteLine(title);
        }

        public void GreetPlayers()
        {
            EnterGame();
            Console.WriteLine("Player 1 please enter your name.");
            string Player1Name = Console.ReadLine();
            Console.WriteLine("Hello " + Player1Name + " 👋\n");

            System.Console.WriteLine("Player 2 please enter your name.");
            string Player2Name = Console.ReadLine();
            System.Console.WriteLine("Hello " + Player2Name + " 👋\n");

            //Players created
            Player1 = new Player(Player1Name);
            Player2 = new Player(Player2Name);
            CurrentPlayer = Player2;
            //Deck created 
            myDeck = new Deck();
        }

        public void DealCards()
        {
            for (int count = 0; count < 10; count++)
            {
                Card cardForPlayer1 = myDeck.Deal();
                Player1.Hand.Add(cardForPlayer1);
                Card cardForPlayer2 = myDeck.Deal();
                Player2.Hand.Add(cardForPlayer2);
                if (count < 8)
                {
                    Card cardForTable = myDeck.Deal();
                    myDeck.Table.Add(cardForTable);
                }
            }
        }

        public Player WhoseTurn()
        {
            if (CurrentPlayer == Player2)
            {
                CurrentPlayer = Player1;
                return CurrentPlayer;
            }
            CurrentPlayer = Player2;
            return CurrentPlayer;
        }

        public void YourTurn(Player CurrentPlayer)
        {
            Console.WriteLine($"\n{CurrentPlayer.Name}'s Turn. Are you ready? [y]");
            string readyPlayer = Console.ReadLine();
            if (readyPlayer != "")
            {
                CurrentPlayer.ShowHand();
                myDeck.ShowTable();
                Console.WriteLine("\nWhat card will you play?");
                int PlayerMove = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Matching which card?");
                int Match = Int32.Parse(Console.ReadLine());
                
                Card PlayedCard = CurrentPlayer.Hand[PlayerMove - 1];
                DiscardCardToTable(PlayedCard); //Whenever player plays a card it will add to a table first to count matching cards easier 
                Card FlipedCard = DealCardToTable();

                //normal play: when there is only one matching(player played card & player matched card)
                if (PlayedCard.month == myDeck.Table[Match - 1].month && PlayedCard.month != FlipedCard.month)
                {
                    CardClaimRemove(PlayedCard, PlayedCard);
                    CardClaimRemove(myDeck.Table[Match - 1], myDeck.Table[Match - 1]);
                    CheckMatchingOnTable(FlipedCard);
                }
                //If there is no matching card but the fliped card is matching what player played
                else if (PlayedCard.month != myDeck.Table[Match - 1].month && PlayedCard.month == FlipedCard.month)
                {
                    CardClaimRemove(PlayedCard, PlayedCard);
                    CardClaimRemove(FlipedCard, FlipedCard);
                    CheckMatchingOnTable(FlipedCard);

                }//if the fliped card is same month card with what player matched(Three matching cards)
                else if (PlayedCard.month == myDeck.Table[Match - 1].month && PlayedCard.month == FlipedCard.month)
                {
                    if (Check4Cards(PlayedCard).Count == 3)
                    {
                        Console.WriteLine("Oh no...PUKK!!");
                    }
                    else
                    {
                        if(Check4Cards(FlipedCard).Count == 4)
                            CheckMatchingOnTable(PlayedCard);
                    }
                }
                //check any matching card on table after fliped a card from the deck
                else
                {
                    CheckMatchingOnTable(FlipedCard);
                }
            }
        }

        public void CheckMatchingOnTable(Card card)
        {
            if (Check4Cards(card) != null)
            {
                foreach (Card c in Check4Cards(card))
                {
                    CardClaimRemove(c, c);
                }
            }
        }

        public void CardClaimRemove(Card claimC, Card removeC)
        {
            Console.WriteLine($"{claimC} added to {CurrentPlayer.Name}");
            CurrentPlayer.Claim(claimC);
            myDeck.Table.Remove(removeC);
        }

        public void DiscardCardToTable(Card card)
        {
            this.CurrentPlayer.Hand.Remove(card);
            myDeck.Table.Add(card);
        }

        public Card DealCardToTable()
        {
            Card DealtCard = myDeck.Deal();
            Console.WriteLine($"Dealt Card from the Deck: {DealtCard.ToString()}\n");
            myDeck.Table.Add(DealtCard);
            return DealtCard;
        }

        public List<Card> Check4Cards(Card played)
        {
            List<Card> SameCards = new List<Card>();
            foreach (Card c in myDeck.Table)
            {
                if (played.month == c.month)
                {
                    SameCards.Add(c);
                }
            }
            if (SameCards.Count > 1)
            {
                return SameCards;
            }
            return null;
        }
        public void GoStop(int Score)
        {
            Console.WriteLine($"Your current Score is {Score}. Would you like to keep Play or Stop? Go for [1], Stop for [2]");
            int Choice = Int32.Parse(Console.ReadLine());
            if (Choice == 2)
            {
                Winner = CurrentPlayer;
                Console.WriteLine($"{Winner.Name} is the winner!");
            }
        }
    }
}