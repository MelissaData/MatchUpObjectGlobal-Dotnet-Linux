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
  /// <summary>
  /// MatchUp Object Global is an extremely fast and powerful programmer's tool that can 
  /// be integrated into custom applications to eliminate duplicate records.
  /// </summary>
  /// <remarks>
  /// High-level flow of this sample:
  ///   1. SETUP     - create an mdReadWrite instance, license it, point it at the data
  ///                  files, choose a matchcode, then InitializeDataFiles() (one time).
  ///   2. INPUT     - read a pipe-delimited input file record by record.
  ///   3. PROCESS   - for each record map its fields, BuildKey() and WriteRecord(); then
  ///                  Process() runs the dedupe pass across all records.
  ///   4. READ      - ReadRecord() each processed record and read its result codes, dupe
  ///                  group, and key; write them to an output file.
  ///   5. INTERPRET - a result code of "MS03" marks a record as a duplicate.
  ///
  /// The pieces in this file map onto that flow:
  ///   - Program             : console harness (argument parsing + the interactive loop).
  ///   - MatchUpObjectGlobal : wrapper around mdReadWrite (setup + the dedupe pipeline).
  ///   - DataContainer       : holds the input/output file paths and small path helpers.
  ///
  /// Where mdReadWrite comes from:
  ///   The MelissaData namespace and its mdReadWrite class live in mdMatchup_cSharpCode.cs,
  ///   a generated C# wrapper over libmdMatchup.so (shipped with libmdGlobalParse.so) that the accompanying
  ///   MelissaMatchupObjectGlobalLinuxDotnet.sh script downloads on every run.
  ///
  /// Reference:
  ///   Quickstart    : https://docs.melissa.com/on-premise-api/matchup-object/matchup-object-quickstart.html
  ///   Release notes : https://releasenotes.melissa.com/on-premise-api/matchup-object/
  ///   Result codes  : https://docs.melissa.com/on-premise-api/matchup-object-global/result-codes.html
  /// </remarks>
  class Program
  {
    /// <summary>
    /// Entry point. Reads the optional command-line arguments, then hands control to
    /// RunAsConsole, which performs the actual MatchUp Object Global setup and processing.
    /// </summary>
    /// <param name="args">The raw command-line arguments</param>
    static void Main(string[] args)
    {
      // Populated by ParseArguments below.
      string license = "";
      string testGlobalFile = "";
      string testUsFile = "";
      string dataPath = "";

      ParseArguments(ref license, ref testGlobalFile, ref testUsFile, ref dataPath, args);
      RunAsConsole(license, testGlobalFile, testUsFile, dataPath);
    }

    /// <summary>
    /// Reads the supported command-line options into the ref parameters.
    ///
    /// Recognized flags (each followed by its value):
    ///   --license / -l   : the Melissa license string
    ///   --global / -g    : path to the global input file to test in one-shot mode
    ///   --us / -u        : path to the US input file to test in one-shot mode
    ///   --dataPath / -d  : path to the MatchUp Object data files
    /// </summary>
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

    /// <summary>
    /// Sets up the MatchUp Object once, then drives the input -> process -> output cycle.
    ///
    /// In interactive mode (no file args) it loops, asking for the two input files each
    /// pass until the user answers "N". In one-shot mode (file args supplied) it runs a
    /// single pass and exits. Each input file is deduped to a matching "_output.txt" file.
    /// </summary>
    static void RunAsConsole(string license, string testGlobalFile, string testUsFile, string dataPath)
    {
      Console.WriteLine("\n\n============= WELCOME TO MELISSA MATCHUP OBJECT GLOBAL LINUX DOTNET =============\n");
      // Construct the wrapper. This is where the object is licensed, pointed at the
      // data files, configured, and initialized (see the MatchUpObjectGlobal constructor below).
      MatchUpObjectGlobal matchUpObjectGlobal = new MatchUpObjectGlobal(license, dataPath);

      bool shouldContinueRunning = true;

      // Gate the program on a successful initialization. If the data files could not
      // be loaded (bad/expired license, missing or wrong-path data files, ...),
      // GetInitializeErrorString() returns the reason instead of "No Error" and we
      // skip the processing loop entirely.
      if (matchUpObjectGlobal.mdMatchUpObjGlobal.GetInitializeErrorString() != "No Error")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        // Holder for this pass's input/output file paths.
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testGlobalFile) && string.IsNullOrEmpty(testUsFile))
        {
          // Interactive mode: prompt the user for the two input file paths.
          Console.WriteLine("\nFill in each value to see the MatchUp Object Global results");

          Console.Write("Global Input File: ");
          dataContainer.InputFilePath1 = Console.ReadLine();

          Console.Write("US Input File: ");
          dataContainer.InputFilePath2 = Console.ReadLine();
        }
        else
        {
          // One-shot mode: use the file paths passed on the command line.
          dataContainer.InputFilePath1 = testGlobalFile;
          dataContainer.InputFilePath2 = testUsFile;
        }

        // Derive each output path from its input path (e.g. foo.txt -> foo_output.txt).
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
        // Dedupe each input file independently, writing results to its output file.
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

        // In one-shot mode there is nothing more to do after a single pass: mark the
        // input handled and stop the outer loop.
        if (!string.IsNullOrEmpty(testGlobalFile + testUsFile))
        {
          isValid = true;
          shouldContinueRunning = false;
        }

        // Interactive mode: ask whether to process another pair of files. Keep prompting
        // until we get a valid Y/N. "N" ends the program; "Y" falls through to another pass.
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

  /// <summary>
  /// Wrapper that owns a single Melissa MatchUp Object Global instance (an mdReadWrite) and
  /// encapsulates one-time setup (license + data files + matchcode) and the per-file dedupe
  /// pipeline. Reuse one instance across many files; do NOT re-initialize per file.
  /// </summary>
  class MatchUpObjectGlobal
  {
    // Path to the MatchUp Object Global data files.
    string dataFilePath;

    // The underlying Melissa MatchUp Object Global instance.
    public mdReadWrite mdMatchUpObjGlobal = new mdReadWrite();

    /// <summary>
    /// Performs the mandatory one-time setup, in this required order:
    ///   1. SetLicenseString        - authorize the object.
    ///   2. SetPathToMatchUpFiles   - tell it where the data files live.
    ///   3. SetKeyFile / SetMatchcodeName / SetMaximumCharacterSize - configure the run.
    ///   4. InitializeDataFiles     - load the data into memory.
    /// </summary>
    /// <param name="license">The Melissa license string used to authorize the object.</param>
    /// <param name="dataPath">Path to the folder containing the MatchUp Object data files.</param>
    public MatchUpObjectGlobal(string license, string dataPath)
    {
      dataFilePath = dataPath;

      // Set license string and set path to data files
      mdMatchUpObjGlobal.SetLicenseString(license);
      mdMatchUpObjGlobal.SetPathToMatchUpFiles(dataFilePath);
      mdMatchUpObjGlobal.SetKeyFile("temp.key");
      mdMatchUpObjGlobal.SetMatchcodeName("Global Address");
      mdMatchUpObjGlobal.SetMaximumCharacterSize(1);

      // Load the data files. A non-ErrorNone status means initialization failed - commonly
      // an invalid/expired license or missing/wrong-path data files.
      if (mdMatchUpObjGlobal.InitializeDataFiles() != MelissaData.mdReadWrite.ProgramStatus.ErrorNone)
      {
        Console.WriteLine("Failed to Initialize Object.");
        return;
      }

      // Diagnostic information, handy for confirming the object loaded the data you expect:

      // Build date of the data files
      Console.WriteLine($"                   DataBase Date: {mdMatchUpObjGlobal.GetDatabaseDate()}");

      // When the license stops working
      Console.WriteLine($"                 Expiration Date: {mdMatchUpObjGlobal.GetLicenseExpirationDate()}");

      // This number should match with the file properties of the Melissa Object binary file.
      // If TEST appears with the build number, there may be a license key issue.
      Console.WriteLine($"                  Object Version: {mdMatchUpObjGlobal.GetBuildNumber()}\n");
    }

    /// <summary>
    /// Runs the MatchUp dedupe pipeline over one input file and writes results to an
    /// output file. Per file: map the matchcode fields, then for each input record
    /// ClearFields -> AddField -> SetUserInfo -> BuildKey -> WriteRecord; call Process()
    /// to dedupe; then ReadRecord() each result and write its codes / dupe group / key.
    /// </summary>
    /// <param name="inputFilePath">Pipe-delimited input file to read.</param>
    /// <param name="outputFilePath">File to write the per-record results to.</param>
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

        // All records are loaded; Process() runs the match/dedupe pass across them.
        mdMatchUpObjGlobal.Process();

        string[] arr = new string[4];

        // Write a header row, then read each processed record back and record its result
        // codes, dupe group, and key. "MS03" in the results flags the record as a duplicate.
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
      // https://docs.melissa.com/on-premise-api/matchup-object-global/result-codes.html
    }
  }

  /// <summary>
  /// Holds the input/output file paths for one pass, plus small path helpers
  /// (FormatOutputFile derives the output name; GetWrapped wraps a long path for display).
  /// </summary>
  public class DataContainer
  {
    public string InputFilePath1 { get; set; } = "";
    public string InputFilePath2 { get; set; } = "";
    public string OutputFilePath1 { get; set; } = "";
    public string OutputFilePath2 { get; set; } = "";
    public string ResultCodes { get; set; } = "";

    /// <summary>
    /// Derives the output file path from the input path by inserting "_output" before the
    /// ".txt" extension. Exits the program if the input file does not exist.
    /// </summary>
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

    /// <summary>
    /// Splits a long file path into chunks no longer than maxLineLength so it prints
    /// neatly across several lines in the console output. Display-only helper.
    /// </summary>
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
