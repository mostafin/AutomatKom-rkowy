using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatKomórkowy
{
    class Program
    {
        [STAThread]
        static void Main()
        {

            /// 2D
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());   
           
            
            
            
            // 1D ZMIENIC PROJETK NA CONOSLOWY
            //String input;
            //int iterations, numberofcells, rule;
            // try
            //{
            //Console.Write("Podaj rozmiar przestrzeni automatu: ");
            //input = Console.ReadLine();
            //Int32.TryParse(input, out numberofcells);
            //Console.Write("Podaj licbe iteracji gry:");
            //input = Console.ReadLine();
            //Int32.TryParse(input, out iterations);
            //Console.Write("Podaj numer reguly od 0 do 255: ");
            //input = Console.ReadLine();
            //Int32.TryParse(input, out rule);
            //if (rule < 0 || rule > 255)
            //    throw new Exception();

            //Console.Clear();
            //Automat1D automat = new Automat1D(numberofcells);
            //automat.Iterate(iterations, rule);
            //Automat2D automat = new Automat2D(40, 40);
            //automat.Iterate(100);
            //Console.ReadKey();
            // }
            //catch (Exception e)
            //{
            //    Console.WriteLine("Cos sie popsulo!");
            //    Console.ReadKey();
            //}


        }
    }
}
