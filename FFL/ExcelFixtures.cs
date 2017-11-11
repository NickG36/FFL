using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

/// <summary>
/// Used to interact with an Excel-format fixtures file.
/// </summary>
namespace FFL
{
    public class ExcelFixtures : Fixtures
    {
        const string fileName = "Fixtures.csv";
        const string tempFileName = "Fixtures_temp.csv";

        StreamReader reader;

        public override bool fileExists()
        {
            bool does_exist = true;

            if (!File.Exists(fileName))
            {
                does_exist = false;
            }
            return does_exist;
        }

        /// <summary>
        /// Returns true if the Fixtures file exists, false otherwise.
        /// If it doesn't exist, the user will be asked if it should be created.
        /// If user selects 'No' then file will not exist.
        /// </summary>
        /// <returns></returns>
        public override bool fileExistsCanCreate()
        {
            bool does_exist = true;

            if (!File.Exists(fileName))
            {
                var full_path = Path.GetFullPath(fileName);
                var result = MessageBox.Show($"Can't find file {full_path}. Shall I create it?",
                                           "Fixtures.csv not found",
                                            MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    using (File.CreateText(fileName))
                    {
                        Console.WriteLine("Have created file");
                    }
                }
                else
                {
                    does_exist = false;
                }

            }
            return does_exist;
        }

        /// <summary>
        /// It is assumed that the fixtures file exists before this method is called
        /// </summary>
        /// <param name="newText"></param>
        public override void addText(string newText)
        {
            //if (fileExists() )
            //{
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine(",," + newText);
                }
            //}
        }

        /// <summary>
        /// It is assumed that the fixtures file exists before this method is called
        /// </summary>
        /// <param name="home"></param>
        /// <param name="away"></param>
        public override void addTeams(string home, string away)
        {
//            if (fileExists())
//            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.Write(home);
                    w.Write(",");
                    w.WriteLine(away);
                }
//            }
        }

        public override void setupReader()
        {
            reader = new StreamReader(fileName);
        }

        public override void deleteFirstBlock()
        {
            bool startFound = false;

            string currLine = "";
            using (reader = new StreamReader(fileName))
            {
                while ((currLine = reader.ReadLine()) != null)
                {
                    Console.WriteLine("Read:'" + currLine + "'");
                    if (currLine.StartsWith(",,"))
                    {
                        Console.WriteLine("START BLOCK FOUND");
                        if (startFound)
                        {
                            break;
                        }
                        else
                        {
                            startFound = true;
                        }
                    }
                    else if (startFound)
                    {
                        Console.WriteLine("Data:'" + currLine + "'");
                    }
                }

                // Now read rest of file and write out to new file
                using (File.CreateText(tempFileName))
                {
                    Console.WriteLine("Have created temp file");
                }

                using (StreamWriter writer = File.AppendText(tempFileName))
                {   
                    writer.WriteLine(currLine);

                    while((currLine = reader.ReadLine()) != null)
                    {   
                        writer.WriteLine(currLine);
                    }
                }
            } // end using reader

            // Move temp file to orig file
            File.Move(tempFileName, fileName);
        }

        public override FixturesBlock readFirstBlock()
        {
            FixturesBlock result = new FixturesBlock();
            result.fixtures = new List<CommonTypes.TwoTeams>();

            bool startFound = false;
            //bool endFound = false;

            string curr_line = "";
            using (reader = new StreamReader(fileName))
            {
                while ((curr_line = reader.ReadLine()) != null)
                {
                    Console.WriteLine("Read:'" + curr_line + "'");
                    if (curr_line.StartsWith(",,"))
                    {
                        if (startFound)
                        {
                            break;
                        }
                        else
                        {
                            startFound = true;
                            // Strip off the leading commas
                            string[] parts = curr_line.Split(',');
                            result.week_description = parts[2];
                        }
                    }
                    else if (startFound)
                    {
                        string[] parts = curr_line.Split(',');

                        CommonTypes.TwoTeams teams = new CommonTypes.TwoTeams(home: parts[0],
                                                                              away: parts[1]);
                        result.fixtures.Add(teams);
                    }
                }

            } // end using reader

            return result;
        }

        public override List<FixturesBlock> readAllFixtureBlocks()
        {
            var result = new List<FixturesBlock>();

            string curr_line;

            bool pending_fixtures = false; // True if fixtures still need adding to result

            FixturesBlock fixtures_block = new FixturesBlock();
            fixtures_block.fixtures = new List<CommonTypes.TwoTeams>();

            using (reader = new StreamReader(fileName))
            {
                while ((curr_line = reader.ReadLine()) != null)
                {
                    Console.WriteLine("Read:'" + curr_line + "'");
                    if (curr_line.StartsWith(",,"))
                    {
                        if (pending_fixtures)
                        {
                            result.Add(fixtures_block);
                            pending_fixtures = false;
                        }

                        fixtures_block = new FixturesBlock();
                        fixtures_block.fixtures = new List<CommonTypes.TwoTeams>();

                        // Remove the initial commas and fetch the week description
                        string[] week_descr_line = curr_line.Split(',');
                        fixtures_block.week_description = "Week: " + week_descr_line[2];
                    }
                    else
                    {
                        Console.WriteLine("Data:'" + curr_line + "'");
                        string[] parts = curr_line.Split(',');

                        CommonTypes.TwoTeams teams = new CommonTypes.TwoTeams(home: parts[0],
                                                                              away: parts[1]);
                        fixtures_block.fixtures.Add(teams);
                        pending_fixtures = true;
                    }
                }

            }
            if (pending_fixtures)
                result.Add(fixtures_block);

            return result;
        }

        public override List<CommonTypes.TwoTeams> readAllFixtures()
        {
            var result = new List<CommonTypes.TwoTeams>();
            string curr_line;

            using (reader = new StreamReader(fileName))
            {
                while ((curr_line = reader.ReadLine()) != null)
                {
                    Console.WriteLine("Read:'" + curr_line + "'");
                    if (!curr_line.StartsWith(",,"))
                    {
                        Console.WriteLine("Data:'" + curr_line + "'");
                        string[] parts = curr_line.Split(',');

                        CommonTypes.TwoTeams teams = new CommonTypes.TwoTeams(home: parts[0],
                                                                              away: parts[1]);
                        result.Add(teams);
                    }
                }
            }

            return result;
        }
    };
}