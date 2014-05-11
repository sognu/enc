using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Huff
{
    public class HuffTable 
    {

        public List<Node> Table;
        private List<char> Chars;

        public HuffTable(string in_f)
        {
            Table = new List<Node>();
            Chars = new List<char>();
            MkTable(in_f);
        }

        private int MkTable(string in_f)
        {
            int curr_char;
            int index = 0;
            Chars = InitIO(in_f);

            while ((curr_char = GetChar(index)) != -1)
            {
                index++;

                if (ContainsNode(curr_char)) { continue; }

                CountFreq(curr_char, index);
            }

            Sort(); 
            return 0;
        }

        private List<char> InitIO(string in_f)
        {
            byte[] file;
            file = ValidateRdr(in_f);
            MemoryStream mem = new MemoryStream(file);
            BinaryReader rdr = new BinaryReader(mem);
            while (true)
            {
                try
                {
                    Chars.Add(rdr.ReadChar());
                }
                catch (EndOfStreamException)
                {
                    break;
                }

            }
            return Chars;
        }


        private int GetChar(int index)
        {
            int curr_char;
            try
            {
                curr_char = Chars[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
            return curr_char;
        }

        private void CountFreq(int curr_char, int index)
        {
            int count = 1;
            int next_char;
            while ((next_char = GetChar(index)) != -1)
            {
                if (curr_char == next_char) { count++; }
                index++;
            }
            //count complete, create new entry.
            Table.Add(new Node{Symbol = curr_char, Count = count});
        }

        private void Sort()
        {
          Table = Table.OrderBy(node => node.Count).ToList<Node>();
        }

        private byte[] ValidateRdr(string in_f)
        {
            try
            {
                return File.ReadAllBytes(in_f);
            }
            catch (IOException)
            {
                Console.Out.WriteLine("in_f is invalid");
                Console.ReadLine();
                Environment.Exit(-1);
                return null;
            }
        }

        public void Print()
        {
            foreach(Node item in Table)
            {
                Console.WriteLine("{0}:{1}", item.Symbol, item.Count); 

            }
        }

        private bool ContainsNode(int c)
        {
            foreach (Node node in Table)
            {
                if (node.Symbol == c) { return true; }

            }
            return false;
        }
    }
}
