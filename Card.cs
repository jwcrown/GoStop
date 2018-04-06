using System;

namespace Go_stop
{
    public class Card
    {
        public string month { get; set; }
        public string type { get; set; }
        public string special { get; set; }

        public Card(string month, string type, string special)
        {
            this.month = month;
            this.type = type;
            this.special = special;
        }

        public override string ToString()
        {
            return $"{month} {type} (special: {special})";
        }
    }
}