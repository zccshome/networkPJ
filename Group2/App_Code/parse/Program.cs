using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SharpICTCLAS;

public class Program
{
    /*
     *  分词功能入口！！！
     */
   public static List<string[]> stringParse(string args)
   {
     //string DictPath = Path.Combine(Environment.CurrentDirectory, "Data") + Path.DirectorySeparatorChar;
     //string DictPath = "Data" + Path.DirectorySeparatorChar;
     string DictPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\Data" + Path.DirectorySeparatorChar;
     WordSegmentSample sample = new WordSegmentSample(DictPath, 2);

      List<WordResult[]> parseResult = sample.Segment(@args);

      List<string[]> output = PrintResult(parseResult);

     return output;

   }

   static List<string[]> PrintResult(List<WordResult[]> result)
   {
      List<string[]> output = new List<string[]>() ;
      for (int i = 0; i < result.Count; i++)
      {
         for (int j = 1; j < result[i].Length - 1; j++) 
             output.Add(new string[]{ result[i][j].sWord , Utility.GetPOSString(result[i][j].nPOS) } );
         //   output += result[i][j].sWord + " / " + Utility.GetPOSString(result[i][j].nPOS) ;

         //output += "\n";
      }
      return output;
   }
}
