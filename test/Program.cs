﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rle;
using System.IO;
using System.Diagnostics;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 rle = new Class1(); // TODO: Initialize to an appropriate value
            string in_f = "C:/Users/chad/prj/encoder/TestRLE/files/ex-1.pbm";
            string out_f = "C:/Users/chad/prj/encoder/TestRLE/files/ex.dec";
            string out_temp = "C:/Users/chad/prj/encoder/TestRLE/files/ex.enc";
            rle.rle_encode(in_f, out_temp);
            rle.rle_decode(out_temp, out_f);
            Debug.Assert(FileCompare(in_f, out_f));
        }

        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
            fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }

    }
}
