using System;
using System.Collections.Generic;

namespace Go_stop
{
    public class Deck
    {
        public List<Card> Cards { get; set; }
        public List<Card> Table { get; set; }

        public Deck()
        {
            Reset();
        }

        public Card Deal()
        {
            // selects the "top-most" card
            Card dealtCard = Cards[0];
            // removes it from the list of cards
            Cards.RemoveAt(0);
            // returns the Card
            return dealtCard;
        }

        public Card RemoveTable(int fromTable)
        {
            Card matchedCard = Table[fromTable];
            Table.RemoveAt(fromTable);
            // returns this Card or null if the index does not exist
            return matchedCard;
        }

        public void Reset()
        {
            Cards = new List<Card>();
            Table = new List<Card>();

            Cards.Add(new Card("January", "kwang", "none"));
            Cards.Add(new Card("January", "tti", "hong-dan"));
            Cards.Add(new Card("January", "pi", "none"));
            Cards.Add(new Card("January", "pi", "none"));
            
            Cards.Add(new Card("February", "yul", "godori"));
            Cards.Add(new Card("February", "tti", "hong-dan"));
            Cards.Add(new Card("February", "pi", "none"));
            Cards.Add(new Card("February", "pi", "none"));

            Cards.Add(new Card("March", "kwang", "none"));
            Cards.Add(new Card("March", "tti", "hong-dan"));
            Cards.Add(new Card("March", "pi", "none"));
            Cards.Add(new Card("March", "pi", "none"));

            Cards.Add(new Card("April", "yul", "godori"));
            Cards.Add(new Card("April", "tti", "cho-dan"));
            Cards.Add(new Card("April", "pi", "none"));
            Cards.Add(new Card("April", "pi", "none"));
            
            Cards.Add(new Card("May", "yul", "none"));
            Cards.Add(new Card("May", "tti", "cho-dan"));
            Cards.Add(new Card("May", "pi", "none"));
            Cards.Add(new Card("May", "pi", "none"));

            Cards.Add(new Card("June", "yul", "none"));
            Cards.Add(new Card("June", "tti", "cheong-dan"));
            Cards.Add(new Card("June", "pi", "none"));
            Cards.Add(new Card("June", "pi", "none"));

            Cards.Add(new Card("July", "yul", "none"));
            Cards.Add(new Card("July", "tti", "cho-dan"));
            Cards.Add(new Card("July", "pi", "none"));
            Cards.Add(new Card("July", "pi", "none"));

            Cards.Add(new Card("August", "kwang", "none"));
            Cards.Add(new Card("August", "yul", "godori"));
            Cards.Add(new Card("August", "pi", "none"));
            Cards.Add(new Card("August", "pi", "none"));

            Cards.Add(new Card("September", "yul", "twoPi"));        
            Cards.Add(new Card("September", "tti", "cheong-dan"));        
            Cards.Add(new Card("September", "pi", "none"));        
            Cards.Add(new Card("September", "pi", "none"));

            Cards.Add(new Card("October", "yul", "none"));    
            Cards.Add(new Card("October", "tti", "cheong-dan"));    
            Cards.Add(new Card("October", "pi", "none"));    
            Cards.Add(new Card("October", "pi", "none"));
            
            Cards.Add(new Card("November", "kwang", "none"));
            Cards.Add(new Card("November", "pi", "twoPi"));
            Cards.Add(new Card("November", "pi", "none"));
            Cards.Add(new Card("November", "pi", "none"));

            Cards.Add(new Card("December", "kwang", "rain"));
            Cards.Add(new Card("December", "yul", "none"));
            Cards.Add(new Card("December", "tti", "rain"));
            Cards.Add(new Card("December", "pi", "twoPi"));
            
            Shuffle();
            
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < Cards.Count; i++)
            {
                int otherIndex = rnd.Next(0, Cards.Count);
                // swap card at i with card at otherIndex
                Card temp = Cards[otherIndex];
                Cards[otherIndex] = Cards[i];
                Cards[i] = temp;
            }
        }

        //ignore this please
        public override string ToString()
        {
            foreach (Card c in Cards)
            {
                System.Console.WriteLine(c);
            }
            return $"{Cards.Count} cards in the deck";
        }

        public void ShowTable() 
        {
            Console.WriteLine();
            Console.WriteLine("Table:");
            int count = 1;
            foreach (Card c in Table)
            {
                Console.Write(count + ". ");
                Console.WriteLine(c);
                count++;
            }
        }
    }
}