using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace TrayCLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Put in the input file location:");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\input.txt");

            string input = File.ReadAllText(path);

            string[] lines = File.ReadAllLines(path);

            pos size = new pos();
            pos pos = new pos();
            int EOA = lines.Length -1;
            int i = 0;
            List<pos> listOfDirtPaths = new List<pos>();

            //first line is the size of the board
            size = getPos(lines[i]);
            i = 1;

           //second line is the position of the Hoover
           if (i == 1) pos = getPos(lines[i]);
            i = 2;

          //All but last line are lists of dirt paths. Creates a list of dirt paths
          while(i < EOA)
         {
                listOfDirtPaths.Add(getPos(lines[i]));
                i++;
            }

            int ret = 0;

          if(i == EOA)
            {
                ret = Walk(lines[i], pos, size, listOfDirtPaths);
            }
            

            string output = pos.x.ToString() + " " + pos.y.ToString() + "\n" + ret.ToString();

            Console.WriteLine(output);

            Console.Read();

        }

        private static int Walk(string str, pos pos, pos size, List<pos> listOfDirtPaths)
        {
            List<pos> tempListOfDirtyPaths = listOfDirtPaths;
            List<pos> returnListOfPaths = new List<pos>();
            foreach(Char c in str)
            {
                pos = pos.Move(c, size);
                if (pos.isOnDirtPath(tempListOfDirtyPaths))
                {
                    if (returnListOfPaths.IndexOf(pos) < 0)
                    {
                        returnListOfPaths.Add(pos);
                    }

                }
            }
            return returnListOfPaths.Count;
        }

        private static pos getPos(string str)
        {
            int[] pos = new int[2];
            string[] output = str.Split(' ');
            try
            {
                int.TryParse(output[0], out pos[0]);
                int.TryParse(output[1], out pos[1]);
            }
            catch (Exception exp) { Console.WriteLine("Position is cannot be converted to a Number - " + str);  }
            return new pos(pos);
        }

        private class pos
        {
            public int x;
            public int y;

            public pos()
            {
                x = 0;
                y = 0;
            }

            public pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public pos(int[] array)
            {
                this.x = array[0];
                this.y = array[1];
            }

            public pos getPos()
            {
                return this;
            }

            public bool isEqual(pos p)
            {
                if (p.x == this.x && p.y == this.y) return true;
                return false;
            }

            public pos Move(Char c, pos size)
            {
                if (c.Equals('N') && this.y < size.y) y++;
                if (c.Equals('S') && this.y > 0) y--;
                if (c.Equals('E') && this.x < size.x) x++;
                if (c.Equals('W') && this.x > 0) x--;

                return this;
            }

            public bool isOnDirtPath(List<pos> listOfDirtPaths)
            {
                foreach(pos dot in listOfDirtPaths)
                {
                    if (dot.isEqual(this)) return true;
                }

                return false;
            }
        }

    }
}
