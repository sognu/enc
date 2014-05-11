using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace huff
{
    class h_table
    {
        internal struct Entry
        {
            internal int c;
            internal int count;
        }

        public Dictionary<int, int> table { get; private set; }
        private HashSet<int> counted_chars;
        List<char> chars;

        public h_table(string in_f)
        {
            table = new Dictionary<int, int>();
            counted_chars = new HashSet<int>();
            chars = new List<char>();
            mk_table(in_f);
        }


        private int mk_table(string in_f)
        {
            int curr_char;
            int index = 0;
            chars = init_io(in_f);

            while ((curr_char = get_byte(index)) != -1)
            {
                index++;

                if (counted_chars.Contains(curr_char)) { continue; }

                count_freq(curr_char, index);
            }

            sort(); 
            return 0;
        }

        private List<char> init_io(string in_f)
        {
            byte[] file;
            file = validate_rdr(in_f);
            MemoryStream mem = new MemoryStream(file);
            BinaryReader rdr = new BinaryReader(mem);
            while (true)
            {
                try
                {
                    chars.Add(rdr.ReadChar());
                }
                catch (EndOfStreamException)
                {
                    break;
                }

            }
            return chars;
        }


        private int get_byte(int index)
        {
            int curr_char;
            try
            {
                curr_char = chars[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
            return curr_char;
        }

        private void count_freq(int curr_char, int index)
        {
            int count = 1;
            int next_char;
            while ((next_char = get_byte(index)) != -1)
            {
                if (curr_char == next_char) { count++; }
                index++;
            }
            //count complete, create new entry.
            //add to table and counted chars.
            table[curr_char] = count;
            counted_chars.Add(curr_char);
        }

        private void sort()
        {
          table = table.OrderBy(key => key.Value).ToDictionary(t=>t.Key, t=>t.Value);
        }

        private byte[] validate_rdr(string in_f)
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

        public void print()
        {
            foreach(KeyValuePair<int,int> item in table)
            {
                Console.WriteLine("{0}:{1}", item.Key, item.Value); 

            }
        }
    }
}
