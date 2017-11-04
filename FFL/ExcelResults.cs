using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Used to interact with an Excel-format results file.
/// </summary>

namespace FFL
{ 
    public class ExcelResults : Results
    {
        const string fileName = "Results.csv";
        StreamReader reader;

        public override void addText(string newText)
        {
            if(!File.Exists(fileName))
            {
                using (File.CreateText(fileName))
                {
                    Console.WriteLine("Have created file");
                }
            }

            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(",,,," + newText);
            }
        }

        public override void addResult(string home_team, 
                                       string away_team,
                                       ushort home_score,
                                       ushort away_score)
        {
            if (!File.Exists(fileName))
            {
                using (File.CreateText(fileName))
                {
                    Console.WriteLine("Have created file");
                }
            }

            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(home_team);
                writer.WriteLine(",");
                writer.WriteLine(home_score);
                writer.WriteLine(",");
                writer.WriteLine(away_score);
                writer.WriteLine(",");
                writer.WriteLine(away_team);
            }
        }

        public override List<CommonTypes.Result> getAllResults()
        {
            List<CommonTypes.Result> result = new List<CommonTypes.Result>();

            reader = new StreamReader(fileName);
            String curr_line = "";

            while((curr_line = reader.ReadLine()) != null)
            {
                if(!curr_line.StartsWith(",,"))
                {
                    CommonTypes.Result curr_result;
                    string[] parts = curr_line.Split(',');

                    curr_result.home_team = GenUtils.getInstance().ToTeamName(parts[0]);
                    curr_result.home_score = ushort.Parse(parts[1]);
                    curr_result.away_score = ushort.Parse(parts[2]);
                    curr_result.away_team = GenUtils.getInstance().ToTeamName(parts[3]);

                    result.Add(curr_result);
                }
            }

            return result;
        }

    }

}