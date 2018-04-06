using System;
using System.Collections.Generic;
using System.Text;

namespace Go_stop
{
    public class Program
    {
        public static void Main(string[] args)
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
            Console.WriteLine("Player 1 please enter your name.");
            string Player1Name = Console.ReadLine();
            Console.WriteLine("Hello " + Player1Name + " 👋");
            Console.WriteLine();                   
            
            System.Console.WriteLine("Player 2 please enter your name.");
            string Player2Name = Console.ReadLine();
            System.Console.WriteLine("Hello " + Player2Name + " 👋");
            Console.WriteLine();                   
            
            //Players created
            Player Player1 = new Player(Player1Name);
            Player Player2 = new Player(Player2Name);

            //Deck created 
            Deck myDeck = new Deck();

            //Deals 10 cards to each player and 8 to the table
            for (int count = 0; count < 10; count++){
                Card cardForPlayer1 = myDeck.Deal();
                Player1.Hand.Add(cardForPlayer1);
                Card cardForPlayer2 = myDeck.Deal();
                Player2.Hand.Add(cardForPlayer2);                    
                if(count < 8){
                    Card cardForTable = myDeck.Deal();
                    myDeck.Table.Add(cardForTable);
                }
            }

            string winnner = "";
            Player CurrentPlayer = Player2;

            do{// runs until winner has been decided.

                //switches player's turn
                if (CurrentPlayer == Player2){
                    CurrentPlayer = Player1;
                }
                else{
                    CurrentPlayer = Player2;
                }

                Console.WriteLine(CurrentPlayer.Name +"'s Turn. Are you ready? [y]");
                string readyPlayer = Console.ReadLine();
                if (readyPlayer != ""){                   
                    CurrentPlayer.ShowHand();
                    myDeck.ShowTable();
                    Console.WriteLine();                   
                    Console.WriteLine("What card will you play?");
                    int PlayerMove = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Matching which card?");
                    int Match = Int32.Parse(Console.ReadLine());
                    
                    while (CurrentPlayer.Hand[PlayerMove - 1].month != myDeck.Table[Match - 1].month){
                        Console.WriteLine("Opps... Invaild move!");
                        Console.WriteLine("What card will you play?");
                        PlayerMove = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Matching which card?");//Need to figure out if player doesnt want to match a card
                        Match = Int32.Parse(Console.ReadLine());
                        
                    }

                    Card firstCard = CurrentPlayer.PlayCard(PlayerMove - 1);
                    Card secondCard = myDeck.Deal();

                    //probably needs its own function
                    //checking to make sure there are not multiple cards of the same month on the table
                    bool duplicate = false;
                    int count = 0;
                    foreach (Card c in myDeck.Table){
                        if (firstCard.month == c.month){
                            count ++;
                        }
                        if (count == 2){
                            duplicate = true;
                        }
                    }

                    //We only want the player to claim cards if they didnt do a pukk
                    if (firstCard.month != secondCard.month || duplicate){
                        Card firstMatch = myDeck.RemoveTable(Match - 1);
                        CurrentPlayer.Claim(firstCard);
                        CurrentPlayer.Claim(firstMatch);
                    }
                    else{//finds pukk and both cards stay on table
                        Console.WriteLine("Oh no...PUKK!!");
                        myDeck.Table.Add(firstCard);
                        myDeck.Table.Add(secondCard);
                    }

                    //logic for second card match needed here

                    //checks score and exits while loop if player has won
                    //logic needed for options to go or stop inside here
                    if (CurrentPlayer.CalculatePoints() >= 10){
                        winnner = CurrentPlayer.Name;
                        continue;
                    }                
                    Console.WriteLine("Finished turn");
                    Console.WriteLine();
                }

            } while (winnner == "");

            Console.WriteLine(winnner, " is the winner!");
        }
    }
}
