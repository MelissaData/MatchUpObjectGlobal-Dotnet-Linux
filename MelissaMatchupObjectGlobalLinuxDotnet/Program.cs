using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using MelissaData;
using System.Text.RegularExpressions;

namespace MelissaMatchUpObjectGlobalLinuxDotnet
{
  class Program
  {
    static void Main(string[] args)
    {
      // Variables
      string license = "";
      string testGlobalFile = "";
      string testUsFile = "";
      string dataPath = "";

      ParseArguments(ref license, ref testGlobalFile, ref testUsFile, ref dataPath, args);
      RunAsConsole(license, testGlobalFile, testUsFile, dataPath);
    }

    static void ParseArguments(ref string license, ref string testGlobalFile, ref string testUsFile, ref string dataPath, string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Equals("--license") || args[i].Equals("-l"))
        {
          if (args[i + 1] != null)
          {
            license = args[i + 1];
          }
        }
        if (args[i].Equals("--global") || args[i].Equals("-g"))
        {
          if (args[i + 1] != null)
          {
            testGlobalFile = args[i + 1];
          }
        }
        if (args[i].Equals("--us") || args[i].Equals("-u"))
        {
          if (args[i + 1] != null)
          {
            testUsFile = args[i + 1];
          }
        }
        if (args[i].Equals("--dataPath") || args[i].Equals("-d"))
        {
          if (args[i + 1] != null)
          {
            dataPath = args[i + 1];
          }
        }
      }
    }

    static void RunAsConsole(string license, string testGlobalFile, string testUsFile, string dataPath)
    {
      Console.WriteLine("\n\n============= WELCOME TO MELISSA MATCHUP OBJECT GLOBAL LINUX DOTNET =============\n");
      MatchUpObjectGlobal matchUpObjectGlobal = new MatchUpObjectGlobal(license, dataPath);

      bool shouldContinueRunning = true;

      if (matchUpObjectGlobal.mdMatchUpObjGlobal.GetInitializeErrorString() != "No Error")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testGlobalFile) && string.IsNullOrEmpty(testUsFile))
        {
          Console.WriteLine("\nFill in each value to see the MatchUp Object Global results");

          Console.Write("Global Input File: ");
          dataContainer.InputFilePath1 = Console.ReadLine();

          Console.Write("US Input File: ");
          dataContainer.InputFilePath2 = Console.ReadLine();
        }
        else
        {
          dataContainer.InputFilePath1 = testGlobalFile;
          dataContainer.InputFilePath2 = testUsFile;
        }

        dataContainer.OutputFilePath1 = dataContainer.FormatOutputFile(dataContainer.InputFilePath1);
        dataContainer.OutputFilePath2 = dataContainer.FormatOutputFile(dataContainer.InputFilePath2);

        // Print user input
        Console.WriteLine("\n===================================== INPUTS ====================================\n");
        
        List<string> sections = dataContainer.GetWrapped(dataContainer.InputFilePath1, 50);

        Console.WriteLine($"\t        Global Input File: {sections[0]}");

        for (int i = 1; i < sections.Count; i++)
        {
          if ((i == sections.Count - 1) && sections[i].EndsWith("/"))
          {
            sections[i] = sections[i].Substring(0, sections[i].Length - 1);
          }
          Console.WriteLine($"\t                           {sections[i]}");
        }

        sections = dataContainer.GetWrapped(dataContainer.InputFilePath2, 50);

        Console.WriteLine($"\t            US Input File: {sections[0]}");

        for (int i = 1; i < sections.Count; i++)
        {
          if ((i == sections.Count - 1) && sections[i].EndsWith("/"))
          {
            sections[i] = sections[i].Substring(0, sections[i].Length - 1);
          }
          Console.WriteLine($"\t                           {sections[i]}");
        }

        // Execute MatchUp Object Global
        matchUpObjectGlobal.ExecuteObjectAndResultCodes(dataContainer.InputFilePath1, dataContainer.OutputFilePath1);
        matchUpObjectGlobal.ExecuteObjectAndResultCodes(dataContainer.InputFilePath2, dataContainer.OutputFilePath2);

        // Print output
        Console.WriteLine("\n===================================== OUTPUT ====================================\n");
        
        sections = dataContainer.GetWrapped(dataContainer.FormatOutputFile(dataContainer.InputFilePath1), 50);

        Console.WriteLine("\n  MatchUp Object Global Information:");

        Console.WriteLine($"\t       Global Output File: {sections[0]}");

        for (int i = 1; i < sections.Count; i++)
        {
          if ((i == sections.Count - 1) && sections[i].EndsWith("/"))
          {
            sections[i] = sections[i].Substring(0, sections[i].Length - 1);
          }
          Console.WriteLine($"\t                           {sections[i]}");
        }

        sections = dataContainer.GetWrapped(dataContainer.FormatOutputFile(dataContainer.InputFilePath2), 50);

        Console.WriteLine($"\t           US Output File: {sections[0]}");

        for (int i = 1; i < sections.Count; i++)
        {
          if ((i == sections.Count - 1) && sections[i].EndsWith("/"))
          {
            sections[i] = sections[i].Substring(0, sections[i].Length - 1);
          }
          Console.WriteLine($"\t                           {sections[i]}");
        }


        bool isValid = false;
        if (!string.IsNullOrEmpty(testGlobalFile + testUsFile))
        {
          isValid = true;
          shouldContinueRunning = false;
        }
        while (!isValid)
        {
          Console.WriteLine("\nTest another file? (Y/N)");
          string testAnotherResponse = Console.ReadLine();

          if (!string.IsNullOrEmpty(testAnotherResponse))
          {
            testAnotherResponse = testAnotherResponse.ToLower();
            if (testAnotherResponse == "y")
            {
              isValid = true;
            }
            else if (testAnotherResponse == "n")
            {
              isValid = true;
              shouldContinueRunning = false;
            }
            else
            {
              Console.Write("Invalid Response, please respond 'Y' or 'N'");
            }
          }
        }
      }
      Console.WriteLine("\n==================== THANK YOU FOR USING MELISSA DOTNET OBJECT ==================\n");
    }
  }

  class MatchUpObjectGlobal
  {
    // Path to MatchUp Object Global data files (.dat, etc)
    string dataFilePath;

    // Create instance of Melissa MatchUp Object Global
    public mdReadWrite mdMatchUpObjGlobal = new mdReadWrite();

    public MatchUpObjectGlobal(string license, string dataPath)
    {
      dataFilePath = dataPath;

      // Set license string and set path to data files  (.dat, etc)
      mdMatchUpObjGlobal.SetLicenseString(license);
      mdMatchUpObjGlobal.SetPathToMatchUpFiles(dataFilePath);
      mdMatchUpObjGlobal.SetKeyFile("temp.key");
      mdMatchUpObjGlobal.SetMatchcodeName("Global Address");
      mdMatchUpObjGlobal.SetMaximumCharacterSize(1);

      if (mdMatchUpObjGlobal.InitializeDataFiles() != MelissaData.mdReadWrite.ProgramStatus.ErrorNone)
      {
        Console.WriteLine("Failed to Initialize Object.");
        return;
      }

      Console.WriteLine($"                   DataBase Date: {mdMatchUpObjGlobal.GetDatabaseDate()}");
      Console.WriteLine($"                 Expiration Date: {mdMatchUpObjGlobal.GetLicenseExpirationDate()}");

      /**
       * This number should match with the file properties of the Melissa Object binary file.
       * If TEST appears with the build number, there may be a license key issue.
       */
      Console.WriteLine($"                  Object Version: {mdMatchUpObjGlobal.GetBuildNumber()}\n");
    }

    // This will call the lookup function to process the input file as well as generate the result codes
    public void ExecuteObjectAndResultCodes(string inputFilePath, string outputFilePath)
    {
      FileStream InFile;
      StreamReader File;
      StreamWriter OutFile;

      string Record;
      string[] Fields;

      long total = 0, dupes = 0;

      // Establish field mappings: when you change the matchcode, you will change these
      mdMatchUpObjGlobal.ClearMappings();

      if (mdMatchUpObjGlobal.AddMapping(mdReadWrite.MatchcodeMapping.Country) == 0 ||
          mdMatchUpObjGlobal.AddMapping(mdReadWrite.MatchcodeMapping.Address) == 0 ||
          mdMatchUpObjGlobal.AddMapping(mdReadWrite.MatchcodeMapping.Address) == 0 ||
          mdMatchUpObjGlobal.AddMapping(mdReadWrite.MatchcodeMapping.Address) == 0 ||
          mdMatchUpObjGlobal.AddMapping(mdReadWrite.MatchcodeMapping.Address) == 0)
      {
        Console.WriteLine("\nError: Incorrect AddMapping() parameter");
        Environment.Exit(1);
      }

      // Proccess the sample data file
      try
      {
        InFile = new FileStream(inputFilePath, FileMode.Open);
        File = new StreamReader(InFile, Encoding.UTF8);

        OutFile = new StreamWriter(outputFilePath, false, Encoding.UTF8);

        Record = File.ReadLine();
        while ((Record = File.ReadLine()) != null)
        {
          // Read and parse pipe delimited record
          Fields = Record.Split(new char[] { '|' });

          // Load up the fields
          mdMatchUpObjGlobal.ClearFields();

          mdMatchUpObjGlobal.AddField(Fields[7]);
          mdMatchUpObjGlobal.AddField(Fields[3]);
          mdMatchUpObjGlobal.AddField(Fields[4]);
          mdMatchUpObjGlobal.AddField(Fields[5]);
          mdMatchUpObjGlobal.AddField(Fields[6]);

          // Create a UserInfo string which uniquely identifies the records
          mdMatchUpObjGlobal.SetUserInfo(Fields[0]);

          // Build the key and submit it
          mdMatchUpObjGlobal.BuildKey();
          mdMatchUpObjGlobal.WriteRecord();
        }

        mdMatchUpObjGlobal.Process();

        string[] arr = new string[4];

        OutFile.WriteLine("Id|ResultCodes|DupeGroup|Key");

        while (mdMatchUpObjGlobal.ReadRecord() != 0)
        {
          if (mdMatchUpObjGlobal.GetResults().Contains("MS03") == true)
          {
            dupes++;
          }

          mdMatchUpObjGlobal.ClearFields();

          arr[0] = mdMatchUpObjGlobal.GetUserInfo();
          arr[1] = mdMatchUpObjGlobal.GetResults();
          arr[2] = mdMatchUpObjGlobal.GetDupeGroup().ToString();
          arr[3] = Regex.Replace(mdMatchUpObjGlobal.GetKey(), @"\s+", " ");

          OutFile.WriteLine(string.Join("|", arr));

          total++;
        }

        InFile.Close();
        OutFile.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      // ResultsCodes explain any issues MatchUp Object Global has with the object.
      // List of result codes for MatchUp Object Global
      // https://wiki.melissadata.com/index.php?title=Result_Code_Details#MatchUp_Object
    }
  }

  public class DataContainer
  {
    public string InputFilePath1 { get; set; } = "";
    public string InputFilePath2 { get; set; } = "";
    public string OutputFilePath1 { get; set; } = "";
    public string OutputFilePath2 { get; set; } = "";
    public string ResultCodes { get; set; } = "";

    public string FormatOutputFile(string inputFilePath)
    {
      FileInfo file = new FileInfo(inputFilePath);
      string filePath = file.FullName;

      if (!System.IO.File.Exists(filePath))
      {
        Console.WriteLine("\nError: The input file does not exist");
        Console.WriteLine(filePath + "\n");
        Environment.Exit(1);
      }

      int location = inputFilePath.IndexOf(".txt");
      string outputFilePath = inputFilePath.Substring(0, location) + "_output.txt";

      return outputFilePath;
    }

    public List<string> GetWrapped(string path, int maxLineLength)
    {
      FileInfo file = new FileInfo(path);
      string filePath = file.FullName;

      string[] lines = filePath.Split(new string[] { "/" }, StringSplitOptions.None);
      string currentLine = "";
      List<string> wrappedString = new List<string>();

      foreach (string section in lines)
      {
        if ((currentLine + section).Length > maxLineLength)
        {
          wrappedString.Add(currentLine.Trim());
          currentLine = "";
        }

        if (section.Contains(path))
        {
          currentLine += section;
        }
        else
        {
          currentLine += section + "/";
        }
      }

      if (currentLine.Length > 0)
      {
        wrappedString.Add(currentLine.Trim());
      }

      return wrappedString;
    }
  }
}
