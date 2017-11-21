using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class GameState
    {
        public double Money { get; set; }
        public double Energon { get; set; }
        public double Detoxin { get; set; }
        public double Kremir { get; set; }
        public double Lepitium { get; set; }
        public double Raenium { get; set; }
        public double Texon { get; set; }

        public GameState()
        {
            Money = 0;
            Energon = 0;
            Detoxin = 0;
            Kremir = 0;
            Lepitium = 0;
            Raenium = 0;
            Texon = 0;
        }
    }
}
