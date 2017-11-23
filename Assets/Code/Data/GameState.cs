using System;

namespace Assets.Code.Data
{
    public class GameState
    {
        public DateTime StarDate { get; set; }

        public decimal Money { get; set; }
        public double Energon { get; set; }
        public double Detoxin { get; set; }
        public double Kremir { get; set; }
        public double Lepitium { get; set; }
        public double Raenium { get; set; }
        public double Texon { get; set; }

        public GameState()
        {
            StarDate = new DateTime(2349, 1, 1);

            Money = 10;
            Energon = 0;
            Detoxin = 0;
            Kremir = 0;
            Lepitium = 0;
            Raenium = 0;
            Texon = 0;
        }
    }
}
