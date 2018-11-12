using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace AutomatKomórkowy
{
    class Automat2D
    {
        bool  f = false;
        private int[,] cells;
        private int ibound;
        private int jbound;
        int N, M;
        
        public Automat2D(int N,int M, int figura)
        {
            cells = new int[N, M];
            this.N = N;
            this.M = M;

            ibound = cells.GetUpperBound(0);
            jbound = cells.GetUpperBound(1);

            switch (figura)
            {
                case 0:
                    oscylator();
                    break;
                case 1:
                    glider();
                    break;
                case 2:
                    niezmienna();
                    break;
                case 3:
                    dakota();
                    break;
                default:
                    oscylator();
                    break;
                       

            }
        }
     
        public void stop()
        {
            f = !f;
        }

        public void oscylator()
        {
            cells[N / 2, M / 2] = 1;
            cells[N / 2 - 1, M / 2] = 1;
            cells[N / 2 - 2, M / 2] = 1;
        }

        public void glider()
        {
            cells[N / 2, M / 2] = 1;
            cells[N / 2, M / 2 + 1] = 1;
            cells[N / 2, M / 2 + 2] = 1;
            cells[N / 2 - 1, M / 2] = 1;
            cells[N / 2 - 2, M / 2 + 1] = 1;
        }
        public void niezmienna()
        {
            cells[N / 2, M / 2 + 1] = 1;
            cells[N / 2, M / 2 + 2] = 1;
            cells[N / 2 - 1, M / 2] = 1;
            cells[N / 2 - 1, M / 2 + 3] = 1;
            cells[N / 2 - 2, M / 2 + 1] = 1;
            cells[N / 2 - 2, M / 2 + 2] = 1;
        }

        public void dakota()
        {
            cells[N / 2, M / 2 + 1] = 1;
            cells[N / 2 - 1, M / 2] = 1;
            cells[N / 2 - 2, M / 2] = 1;
            cells[N / 2 - 3, M / 2] = 1;
            cells[N / 2 - 3, M / 2 + 1] = 1;
            cells[N / 2 - 3, M / 2 + 2] = 1;
            cells[N / 2 - 3, M / 2 + 3] = 1;
            cells[N / 2 - 2, M / 2 + 4] = 1;
            cells[N / 2, M / 2 + 4] = 1;
        }

        public void Iterate(int iterations,Bitmap bitmap , PictureBox pictureBox , ProgressBar bar,Graphics graphics)
        {
            byte[] b = new byte[] { 254 };
            var encoder = Encoding.GetEncoding(437);
            for (int n = 0; n < iterations; n++)
            {
                if(f == true)
                {
                    break;
                }
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        if (cells[i, j] == 1)
                            //graphics.FillRectangle(Brushes.Black, i, j, 1, 1);
                        bitmap.SetPixel(i, j, Color.Black);
                        else
                            //graphics.FillRectangle(Brushes.SteelBlue, i, j, 1, 1);
                        bitmap.SetPixel(i, j, Color.SteelBlue);
                    }
                }
                System.Threading.Thread.Sleep(200);
                bar.Invoke(new Action(() => bar.Increment(1)));
                pictureBox.Invoke(new Action(() => pictureBox.Image = bitmap ));
                cells = GetnextIterationCells();
            }
        }  
        private int[,] GetnextIterationCells()
        {
            int[,] newCells = new int[cells.GetLength(0), cells.GetLength(1)];
            for (int i = 0; i < newCells.GetLength(0); i++)
                for (int j = 0; j < newCells.GetLength(1); j++)
                    newCells[i, j] = CheckNeighboursState(i, j);

            return newCells;
        }
        private int CheckNeighboursState(int i,int j)
        {
            List<int> neighbours = new List<int>();

            if(i==0 && j!=0 && j!=jbound)
            {
                for (int k = j - 1; k <= j + 1; k++)
                    neighbours.Add(cells[ibound, k]);
                for (int k = j - 1; k <= j + 1; k++)
                    neighbours.Add(cells[i+1, k]);
                neighbours.Add(cells[i, j - 1]);
                neighbours.Add(cells[i, j + 1]);
            }
           else if(i==ibound && j != 0 && j != jbound)
            {
                for (int k = j - 1; k <= j + 1; k++)
                    neighbours.Add(cells[0, k]);
                for (int k = j - 1; k <= j + 1; k++)
                    neighbours.Add(cells[ibound - 1, k]);
                neighbours.Add(cells[i, j - 1]);
                neighbours.Add(cells[i, j + 1]);
            }
          else  if(j==0 && i!=0 && i!=ibound)
            {
                for (int k = i - 1; k <= i + 1; k++)
                    neighbours.Add(cells[k, jbound]);
                for (int k = i - 1; k <= i + 1; k++)
                    neighbours.Add(cells[k, j+1]);
                neighbours.Add(cells[i-1, j]);
                neighbours.Add(cells[i+1, j]);
            }
           else if (j == jbound && i != 0 && i != ibound)
            {
                for (int k = i - 1; k <= i + 1; k++)
                    neighbours.Add(cells[k, 0]);
                for (int k = i - 1; k <= i + 1; k++)
                    neighbours.Add(cells[k, j -1]);
                neighbours.Add(cells[i - 1, j]);
                neighbours.Add(cells[i + 1, j]);
            }
           else if(i == 0 && j==0)
            {
                neighbours.Add(cells[i, j + 1]);
                neighbours.Add(cells[i+1, j + 1]);
                neighbours.Add(cells[i+1, j]);
                neighbours.Add(cells[i, jbound]);
                neighbours.Add(cells[i+1, jbound]);
                neighbours.Add(cells[ibound, j]);
                neighbours.Add(cells[ibound, j + 1]);
                neighbours.Add(cells[ibound, jbound]);

            }
           else if(i == ibound && j==0)
            {
                neighbours.Add(cells[i-1, j +1 ]);
                neighbours.Add(cells[i-1, j]);
                neighbours.Add(cells[i, j + 1]);
                neighbours.Add(cells[0, j]);
                neighbours.Add(cells[0, j+1]);
                neighbours.Add(cells[i-1, jbound]);
                neighbours.Add(cells[i, jbound]);
                neighbours.Add(cells[0, jbound]);
            }
           else if(i==ibound && j==jbound)
            {
                neighbours.Add(cells[i, j - 1]);
                neighbours.Add(cells[i-1, j - 1]);
                neighbours.Add(cells[i-1, j]);
                neighbours.Add(cells[i-1, 0]);
                neighbours.Add(cells[i, 0]);
                neighbours.Add(cells[0, j -1]);
                neighbours.Add(cells[0, jbound]);
                neighbours.Add(cells[0, 0]);
            }
           else if(i==0 && j==jbound)
            {
                neighbours.Add(cells[i, j - 1]);
                neighbours.Add(cells[i+1, j -1 ]);
                neighbours.Add(cells[i+1, j ]);
                neighbours.Add(cells[ibound, j - 1]);
                neighbours.Add(cells[ibound, j ]);
                neighbours.Add(cells[i, 0]);
                neighbours.Add(cells[i, 1]);
                neighbours.Add(cells[ibound, 0]);

            }
            else
            {
                neighbours.Add(cells[i-1, j - 1]);
                neighbours.Add(cells[i-1, j]);
                neighbours.Add(cells[i-1, j + 1]);
                neighbours.Add(cells[i, j - 1]);
                neighbours.Add(cells[i, j + 1]);
                neighbours.Add(cells[i+1, j - 1]);
                neighbours.Add(cells[i+1, j ]);
                neighbours.Add(cells[i+1, j + 1]);
            }
            int count = (from x in neighbours
                         where x != 0
                         select x).Count();
            switch(cells[i,j])
            {
                case 0:
                    if (count == 3)
                        return 1;
                    return 0;
                case 1:
                    if (count == 2 || count == 3)
                        return 1;
                  else  if (count > 3 || count < 2)
                        return 0;
                    break;
            }

            return 0;
        }
    }
}
