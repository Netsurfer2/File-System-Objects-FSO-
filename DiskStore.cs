/**********************************************************************************************
 * Class: Programming 120  (Classes and Objects)                                              *
 * Project: HW3 Console Presentation 3-Tier                                                   *
 * Professor: Kurt Friedrich                                                                  *
 * Name: Chris Singleton                                                                      *
 * Date: 10/08/2016                                                                           *
 **********************************************************************************************
 * Summary: 1. Create a new class called DiskStore.cs                                         *
 *          2. DiskStore.cs holds two methods (Filestream Reader / Writer).                   *
 *          3. The objective is to write to a text file and read the contents to the console. *
 *          4. Using a two dimensional array (10, 3), will only write 10 lines three across.  *
 *          5. If you write over ten lines, then it will notify you and not write to the file.*
 *********************************************************************************************/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessTier
{
    public class DiskStore
    {
        //======================== Write to Text File ===========================//

        // Please Note: Void because no return, were writing to a file, not returning.
        /* Writes to the text file by looping the two dimensional array using StreamWriter
           directly to a text file called arrayData.txt on the hard drive. Then the StreamWriter
           closes.*/
        public static void WriteStringArray(string[,] streamWriteArray)
        {
            // define new object form system supplied objects
            StreamWriter fileWriter;

            // Call the constuctor - Instantiates a StreamWriter and calls it to a variable.
            // 
            fileWriter = new StreamWriter(@"C:\Users\Christopher\Documents\arrayData.txt");
            for (int i = 0; i < streamWriteArray.GetLength(0); i++)
            {
                for (int j = 0; j < streamWriteArray.GetLength(1); j++)
                {
                    fileWriter.WriteLine(streamWriteArray[i, j]);
                }
            }
            fileWriter.Close();
        }

        //======================= Read from the Text File =========================//

        // Instantiate a StreamReader object with a 2D Array.
        /* Read using StreamReader directly into a 2D Array variable from the file to
           the console while looping through each line to the entire length of the variable
           readArray through both dimensions. Then write both dimensions of the readArray
           out to the console after closing the reader using another four loop and return
           the readArray.*/
        public static string[,] ReadStringArray()

        {
            string[,] streamReadArray = new string[10, 3];

            StreamReader fileReader;
            fileReader = new System.IO.StreamReader(@"C:\Users\Christopher\Documents\arrayData.txt");

            //string oneLineOfData;

            for (int i = 0; i < streamReadArray.GetLength(0); i++)
            {
                for (int j = 0; j < streamReadArray.GetLength(1); j++)
                {
                    streamReadArray[i, j] = fileReader.ReadLine();
                }
            }

            fileReader.Close();

            for (int i = 0; i < streamReadArray.GetLength(0); i++)
            {
                for (int j = 0; j < streamReadArray.GetLength(1); j++)
                {
                    Console.WriteLine(streamReadArray[i, j]);
                }
            }
            return streamReadArray;
        }
        
        
         
    }
}
