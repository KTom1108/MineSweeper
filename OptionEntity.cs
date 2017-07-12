using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
    public class OptionEntity
    {
        public string difficultyType { get; set; }
        public int hNumForButton { get; set; }
        public int vNumForButton { get; set; }

        public int numForMine { get; set; }

        public bool isContinue { get; set; }
        public string result { get; set; }
    }
}
