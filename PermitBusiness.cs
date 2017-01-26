using System;
using System.Collections;
using DataAccessTier;
using System.Data;

namespace BusinessTier
{
  // Summary description for Class1.

  public static class PermitBusiness
  {
      
      #region "_fields"
      //none, we won't store any state in this static class
      #endregion

      #region "constuctors"
      //none, let the system build a default
      #endregion


      // This method validates the string data 
      // <param "textToCheck">array contains the text strings passed in to be validated </param>
      // method returns a boolean value indicating that the validation and save were successful
      // method is static since this is just a function, it makes no sense to create more than one copy, as
      // other objects can all share this this one.  Note that it holds no state

      public static bool Validate(string[] textToCheck,  out string[] BusReturnMessages)
          // out lets you pass in a parameter that has not been given a value yet, 
          // notice out modifier is on both the method call and the method defintion
    {
      bool errorOccurred = false;  // initalize assuming success
      string userName = "";
      string zip = "";  
     
      try  // using a try block to catch any unanticipated errors
      {
          errorOccurred = false;
          BusReturnMessages = new string[3]; // we get to define it here, seems kind of late!

          //Validate name, at least 4 but not longer than 20 characters
          if (textToCheck[0].Length >= 4 && textToCheck[0].Length <= 20)
          {
              BusReturnMessages[0] = "";  // no error to report for 1st passed in value
              userName = textToCheck[0];
          }
          else
          {
              errorOccurred = true;
              BusReturnMessages[0] = "Name must be between 4 and 20 characters";
          }

          //Validate zip
          if (textToCheck[1].Length == 5) // making sure zip code is at least 5 characters, off course we could do better!
          {
              BusReturnMessages[1] = "";  // no error to report
              zip = textToCheck[1];
          }
          else  // note we can report either or both errors
          {
              BusReturnMessages[1] = "Not a valid zip";
              errorOccurred = true;
          }

          BusReturnMessages[2] = "";  // clear the 3rd return message
          if (!errorOccurred)
          {
              // use data tier to save data to database (array) if there were no validation errors
              if (PermitData.Save(userName, zip))  // calls data tier, and gets a status bool status back
              {
                  BusReturnMessages[2] = "Your data was submitted.";
              }
              else  // data tier returned a false
              {
                  BusReturnMessages[2] = "Sorry, database problem."; // we are lumping all possible errors into one
              }
          }

          // returning success value - true if all validated correctly, false otherwise
          return !errorOccurred;
      }

      catch (Exception ex)
      {
          throw ex;  // this will catch any exceptions, and then throw one up the chain to the calling program
                    // really should have a try catch back in the console program too
      }
      finally
      {
          // this phase of try catch always runs, good or bad.  But we don't have anything to close down or correct in this program
      }
     }


    // Our 2nd Business tier static method, called by console (UI) to get all data, and it in turn calls the 3rd, data tier
    public static string[,] GetPermits(string userKey)
    {
        string[,] badKeyDB = new string[1, 3]; // create a little 1x3 array to use to pass back an error if password is wrong
        badKeyDB[0, 0] = "invalid key";  // load up the first element with the error message
        // in the real world do some seriois validation
        if (userKey == "11")  // our  simple validation
        {
            // if passed, make a call to our 3rd "data" tier
            // note the return from teh dataq tier is the 10x3 matrix of data, so we call ther tier, get it, and
            // then just pass it right back to the UI (console) tier
            return PermitData.GetApplications();
        }
        else
        {
            return badKeyDB;  // if the password was wrong, pass the other array back to the UI tier
        }
        
    }
  }
}
