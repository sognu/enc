using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huff
{
    public class HuffTree
    {

        public List<Node> Nodes { get; set; }

        public HuffTree()
        {
            Nodes = new List<Node>();

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

        private void MkTree(HuffTable t)
        {
            int index = 0;

            Nodes = t.Table;

            while (Nodes.Count > 1)
            {
                Combine(Nodes[index], Nodes[index + 1], index);
            }
        }

        private void Combine(Node a, Node b, int index)
        {
            Node parent = new Node
            {
                Symbol = -1,
                Count = a.Count + b.Count,
                Left = Greater(a, b),
                Right = Lesser(a, b)
            };

            Nodes.Remove(a);
            Nodes.Remove(b);
            InsertParent(parent);
        }

        private void InsertParent(Node parent)
        {
            int i = 0;

            while (i <= Nodes.Count)
            {
                if (i == Nodes.Count) { Nodes.Add(parent); break; }

                else if (Nodes[i].Count >= parent.Count)
                {
                    Nodes.Insert(i, parent);
                    break;
                }
                i++;
            }
        }

        private Node Greater(Node a, Node b)
        {
            return a.Count >= b.Count ? a : b;
        }

        private Node Lesser(Node a, Node b)
        {
            return a.Count < b.Count ? a : b;
        }
    }
}

