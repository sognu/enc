using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace rle
{
    public class Class1
    {
        BinaryReader rdr;
        BinaryWriter wrt;
        const int COUNT_MAX = 127;

        /// <summary>
        /// Bit level rle encoder
        /// </summary>
        public int rle_encode(string in_f, string out_f)
        {
            byte[] file;
            file = validate_rdr(in_f);
            MemoryStream mem = new MemoryStream(file);
            rdr = new BinaryReader(mem);
            wrt = validate_wrt(out_f);
            int curr_char = 0;
            int curr_bit, prev_bit = -1;
            int first_bit = 1;
            int index = -1;
            int bit_count = 0;
            while ((curr_bit = get_bit(ref curr_char, ref index)) != -1)
            {
                bit_count = 1;

                if (curr_bit == prev_bit)
                {
                    /* run has started */
                    bit_count++;
                    count_run(ref bit_count, ref prev_bit, ref curr_bit, ref first_bit, ref index, ref curr_char);
                    if (rdr.BaseStream.Position >= rdr.BaseStream.Length)
                    {
                        run_end(ref bit_count, ref prev_bit, ref curr_bit);
                        goto END;
                    }

                }
                else if (first_bit == 1)
                {
                    /* keep looking for run */
                    prev_bit = curr_bit;
                    first_bit = 0;
                }
                else
                {
                    /* no run -- write single bit */
                    run_end(ref bit_count, ref prev_bit, ref curr_bit);
                }

                index--;
            }

            if (rdr.BaseStream.Position >= rdr.BaseStream.Length)
            {
                bit_count = 1;
                run_end(ref bit_count, ref prev_bit, ref curr_bit);
            }

            //rdr.Close();
            //wrt.Close();
            //return 1;

        END:
            rdr.Close();
            wrt.Close();
            return 1;
        }

        public int rle_decode(string in_f, string out_f)
        {
            byte[] file;
            file = validate_rdr(in_f);
            MemoryStream mem = new MemoryStream(file);
            rdr = new BinaryReader(mem);
            wrt = validate_wrt(out_f);
            int curr_char;
            int curr_bit;
            int bit_count = 0;
            int bite = 0;
            int bite_index = 7;

            while ((curr_char = rdr.ReadByte()) != 0 && (rdr.BaseStream.Position != rdr.BaseStream.Length))
            {
                curr_bit = (curr_char >> 7) & 1;
                bit_count = (curr_char & 0x7F);
                write_count(ref bit_count, ref bite_index, ref bite, curr_bit);
            }

            rdr.Close();
            wrt.Close();
            return 1;
        }

        private int get_bit(ref int curr_char, ref int index)
        {
            int[] bit_array = new int[8];
            if (index == -1)
            {
                curr_char = rdr.ReadByte();

                if (rdr.BaseStream.Position >= rdr.BaseStream.Length)
                {
                    return -1;
                }

                index = 7;
                to_array(curr_char, ref bit_array);
                return bit_array[index];
            }

            to_array(curr_char, ref bit_array);
            int temp = bit_array[index];
            return temp;
        }

        private void count_run(ref int bit_count, ref int prev_bit, ref int curr_bit, ref int first_bit, ref int index, ref int curr_char)
        {
            index--;
            int temp = 0;
            while ((curr_bit = get_bit(ref curr_char, ref index)) != -1)
            {
                if (curr_bit == prev_bit)
                {
                    bit_count++;

                    if (bit_count == COUNT_MAX)
                    {
                        temp |= ((prev_bit << 7 & 0x80)) | bit_count;
                        wrt.Write((byte)temp);
                        prev_bit = -1;
                        bit_count = 0;
                        first_bit = 1;
                        break;
                    }

                }
                else
                {
                    /* run has reached its end */
                    run_end(ref bit_count, ref prev_bit, ref curr_bit);
                    break;
                }

                index--;

            }
        }

        private void run_end(ref int bit_count, ref int prev_bit, ref int curr_bit)
        {
            int bite = 0;
            bite |= ((prev_bit << 7 & 0x80)) | bit_count;
            wrt.Write((byte)bite);
            prev_bit = curr_bit;
        }

        private void to_array(int curr_char, ref int[] bit_array)
        {
            int i;
            for (i = 7; i >= 0; i--)
            {
                //int temp = curr_char >> i;
                bit_array[i] = ((curr_char) >> i) & 0x1;
            }
        }

        private void write_count(ref int bit_count, ref int bite_index, ref int bite, int curr_bit)
        {

            while (bit_count > 0)
            {
                bite |= (curr_bit) << bite_index;
                bite_index--;
                bit_count--;

                if (bite_index == -1)
                {
                    wrt.Write((byte)bite);
                    bite_index = 7;
                    bite = 0;
                }
            }
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

        private BinaryWriter validate_wrt(string out_f)
        {
            try
            {
                wrt = new BinaryWriter(File.Open(out_f, FileMode.Create));
            }
            catch (IOException)
            {
                Console.Out.WriteLine("out_f is invalid");
                Console.ReadLine();
                Environment.Exit(-1);
                return null;
            }
            return wrt;
        }

        private int pow(int x, int y){

            int i = 0;
            for(;i < y; i++){
                x*=x;
            }
            return x;

        }
    }
}