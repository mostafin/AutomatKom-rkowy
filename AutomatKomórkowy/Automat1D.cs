using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatKomórkowy
{
    enum Rules : Byte
    {
        rule30 = 30,
        rule60 = 60,
        rule90 = 90,
        rule102 = 102,
        rule150 = 150,
        rule250 = 250
    }
    class Automat1D
    {
        private List<int> cells;
        private string rule;

        public Automat1D()
        {
            cells = new List<int>() { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
        }
        public Automat1D(int numberofcells)
        {
            cells = new List<int>();
            for (int i = 0; i < numberofcells; i++)
                cells.Add(0);
            if (numberofcells % 2 == 0)
                cells[numberofcells / 2] = 1;
            else
                cells[(numberofcells - 1) / 2] = 1;
        }
        public void Iterate(int iterations, int rule)
        {
            this.rule = Convert.ToString(rule,2).PadLeft(8,'0');
            var encoder = System.Text.Encoding.GetEncoding(437);
            byte[] b = new byte[] { 254 };
               
            for (int i = 0; i < iterations; i++)
            {
                foreach (int cell in cells)
                {
                    if (cell == 0)
                        Console.Write(" ");
                    if (cell == 1)
                        Console.Write(encoder.GetString(b));
                }
                Console.WriteLine();
                cells = GetNextIterationCells();
            }
        }
        private List<int> GetNextIterationCells()
        {
            List<int> newCells = new List<int>();
            for (int i = 0; i < cells.Count; i++)
            {
                if (i == 0)
                    newCells.Add(GenerateCellstate(0, cells[i], cells[i + 1]));
                else if (i == cells.Count - 1)
                    newCells.Add(GenerateCellstate(cells[i - 1], cells[i], 0));
                else
                    newCells.Add(GenerateCellstate(cells[i - 1], cells[i], cells[i + 1]));

            }
            return newCells;
        }
        private int GenerateCellstate(int left, int mid, int right)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(left.ToString()).Append(mid.ToString()).Append(right.ToString());

            switch (sb.ToString())
            {
                case "111":
                    return Int32.Parse(rule[0].ToString());
                case "110":
                    return Int32.Parse(rule[1].ToString());
                case "101":
                    return Int32.Parse(rule[2].ToString());
                case "100":
                    return Int32.Parse(rule[3].ToString());
                case "011":
                    return Int32.Parse(rule[4].ToString());
                case "010":
                    return Int32.Parse(rule[5].ToString());
                case "001":
                    return Int32.Parse(rule[6].ToString());
                case "000":
                    return Int32.Parse(rule[7].ToString());

            }
            return 0;
        }
    }

}
