using System;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace DataAccessTier
{
  // Store the data

  public static class PermitData
  {
      // static fields
      // instead of a real database, we our going to store the data in a simple array
      // which would lose all the data every time the program is stopped!!
      // but you will imrove this with your homework
      private static string[,] fakeDB = new string[10, 3];  // hold ten entries 
      private static int index = 0;  // this index will be used to keep track of which new element of the db (array) we should add
                                     // because it is static, we can depend on it to hold a real value between calls, as there is only one array and one pointer
        
 
    // static method  called by middle tier, used to add another row of data.
    public static bool Save( string userName, string zip)
          
    {
            /* Fixes the problem when you delete the file (file not found error)! */
            if (!File.Exists(@"C:\Users\Christopher\Documents\arrayData.txt"))
            {
                FileStream fs = File.Create(@"C:\Users\Christopher\Documents\arrayData.txt");
                fs.Close();
            }

            fakeDB = DiskStore.ReadStringArray();
            
            //==================== Added ============================
            index = 0; //set the index back to 0, and now figure out what it should be
            for (int i = 0; i < 10; i++) // find the first empty slot, and set index to point to it
            {
                if (fakeDB[i, 0] == null)
                {
                    break; //if the file has never been written to, value may be null
                }
                else if (fakeDB[i, 0].Length < 2)
                {
                    break; //or maybe its an emptry string, like ""
                }
                else
                {
                    index = index + 1; // bump up the index until we find an empty row
                }
            }
            //=====================================================


        if (index < 10 )
	    {
		    fakeDB[index,0] = userName;  
            fakeDB[index,1] = zip;  
            fakeDB[index,2] = DateTime.Now.ToShortDateString();   // adding a time stamp from our DB

            DiskStore.WriteStringArray(fakeDB); // Call the DiskStore WriteStringArrry method while passing fakeDB.
            index = index + 1;  // bump our pointer

                return true;
	    }
        else
        {
            return false;  // when we fill all 10 rows, we return the bool false = error
        }
    }

    // Public Static Method GetApplications
    public static string[,] GetApplications()
    {
            // instead of a real SQL or file system operation, we just return our ReadStringArray method.
            return DiskStore.ReadStringArray();
    }
  }
}
