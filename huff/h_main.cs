using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace huff
{
    public class h_main
    {
        public int encode(string in_f, string out_f)
        {
            h_table table = new h_table(in_f);
            //table.print();
            //Console.ReadLine();
            h_tree tree = new h_tree(table);
            //write_encode(tree, out_f);
            return 0;
        }

        public int decode(string in_f, string out_f)
        {
            return 0;
        }

        private int write_encode(h_tree tree, string out_f)
        {



            return 0;
        }
    }
}
