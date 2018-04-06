using System;
using System.Collections.Generic;

namespace Go_stop
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> KwangCaptured { get; set; }
        public List<Card> YulCaptured { get; set; }
        public List<Card> TtiCaptured { get; set; }
        public List<Card> PiCaptured { get; set; }
        
        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            KwangCaptured = new List<Card>();
            YulCaptured = new List<Card>();
            TtiCaptured = new List<Card>();
            PiCaptured = new List<Card>();
        }

        public Card Draw(Deck someDeck)
        {
            // draws a card from a deck
            Card gotCard = someDeck.Deal();
            // adds it to the player's hand
            Hand.Add(gotCard);
            // returns the Card
            return gotCard;
        }

        public Card PlayCard(int fromHand)
        {
            // plays the Card at the specified index from the player's hand 
            if(fromHand < 0 || fromHand >= Hand.Count)
            {
                return null;
            }
            Card playedCard = Hand[fromHand];
            Hand.RemoveAt(fromHand);
            // returns this Card or null if the index does not exist
            return playedCard;
        }

        public void ShowHand() 
        {
            Console.WriteLine();
            Console.WriteLine(this.Name + "'s Hand:");
            int count = 1;
            foreach (Card c in Hand)
            {
                Console.Write(count + ". ");
                Console.WriteLine(c);
                count++;
            }
        }

        //adds matches to player's respective captured list
        public void Claim(Card cardToClaim){
            if (cardToClaim.type == "kwang"){
                this.KwangCaptured.Add(cardToClaim);
            }
            else if (cardToClaim.type == "yul"){
                this.YulCaptured.Add(cardToClaim);
            }
            else if (cardToClaim.type == "tti"){
                this.TtiCaptured.Add(cardToClaim);
            }
            else if (cardToClaim.type == "pi"){
                this.PiCaptured.Add(cardToClaim);
            }
        }

        //the following code makes no sense I know
        public int CalculatePoints(){
            int points = 0;
            points += CalculateKwang();
            points += CalculateYul();
            points += CalculateTti();
            points += CalculatePi();

            return points;
        }

        public int CalculateKwang(){
            if (KwangCaptured.Count > 3){
                return KwangCaptured.Count;
            }
            else if (KwangCaptured.Count == 3){
                foreach (Card c in KwangCaptured){
                    if (c.special == "rain"){
                        return 0;
                    }
                    return 3;
                }
            }
            return 0;
        }

        public int CalculateYul(){
            int points = 0;
            if (YulCaptured.Count >= 5){
                points += YulCaptured.Count - 4;
            }
            int godori = 0;
            foreach (Card c in YulCaptured){
                if (c.special == "godori"){
                    godori += 1;
                }
            }
            if (godori == 3){
                points += 3;
            }
            return points;
        }

        public int CalculateTti(){
            int points = 0;
            if (TtiCaptured.Count >= 5){
                points += TtiCaptured.Count - 4;
            }
            int hongDan = 0;
            int cheongDan = 0;
            int choDan = 0;
            foreach (Card c in TtiCaptured){
                if (c.special == "hong-dan"){
                    hongDan +=1;
                }
                if (c.special == "cheong-dan"){
                    cheongDan +=1;
                }
                if (c.special == "cho-dan"){
                    choDan +=1;
                }
            }
            if (hongDan == 3){
                points += 3;
            }
            if (cheongDan == 3){
                points += 3;
            }
            if (choDan == 3){
                points += 3;
            }
            return points;
        }
        public int CalculatePi(){
            int piCount = PiCaptured.Count;
            foreach (Card p in PiCaptured){
                if (p.special == "twoPi"){
                    piCount += 1;
                }
            }
            if (piCount >= 10){
                return piCount - 9;
            }
            return 0;
        }
    }
}