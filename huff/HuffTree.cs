using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huff
{
    public class HuffTree
    {

        public List<Node> Tree { get; set; }

        public HuffTree()
        {
            Tree = new List<Node>();

        }

        public int Encode(string in_f, string out_f)
        {
            HuffTable table = new HuffTable(in_f);
            MkTree(table);
            //table.Print();
            //Console.ReadLine();
            //write_encode(tree, out_f);
            return 0;
        }

        public int Decode(string in_f, string out_f)
        {
            return 0;
        }

        private int MkTree(HuffTable t)
        {
            while (Tree.Count > 1)
            {

                if (t.Table.Count > 2)
                {




                }

                
            }

            return 0;
        }
        
    }
}
