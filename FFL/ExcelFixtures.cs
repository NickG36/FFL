using System;
using System.IO;
using System.Collections.Generic;

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

        public override void addText(string newText)
        {
            //throw new NotImplementedException();

            if(!File.Exists(fileName))
            {
                using (File.CreateText(fileName))
                {
                    Console.WriteLine("Have created file");
                }
            }

            using (StreamWriter w = File.AppendText(fileName))
            {
                w.WriteLine(",," + newText);
            }
        }

        public override void addTeams(string home, string away)
        {
            if(!File.Exists(fileName))
            { 
                using (File.CreateText(fileName))
                {
                    Console.WriteLine("Have created file");
                }
            }

            using (StreamWriter w = File.AppendText(fileName))
            {
                w.WriteLine(home);
                w.WriteLine(",");
                w.WriteLine(away);
            }
        }

        public override void setupReader()
        {
            reader = new StreamReader(fileName);
        }

        public override FixturesBlock readFirstBlock()
        {
            FixturesBlock result = new FixturesBlock();
            result.fixtures = new List<CommonTypes.TwoTeams>();

            bool startFound = false;
            //bool endFound = false;

            string currLine = "";
            while((currLine = reader.ReadLine()) != null)
            {
                Console.WriteLine("Read:'" + currLine + "'");
                if(currLine.StartsWith(",,"))
                {
                    Console.WriteLine("START BLOCK FOUND");
                    if (startFound)
                    {
                        break;
                    }
                    else
                    {
                        startFound = true;
                        result.week_description = currLine;
                    }
                }
                else if(startFound)
                {
                    Console.WriteLine("Data:'" + currLine + "'");
                    CommonTypes.TwoTeams teams;
                    string[] parts = currLine.Split(',');

                    teams.home = parts[0];
                    teams.away = parts[1];

                    result.fixtures.Add(teams);
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

            // Move temp file to orig file
            File.Move(tempFileName, fileName);

            return result;
        }

    };
}