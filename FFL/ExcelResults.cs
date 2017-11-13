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

        public override bool doesFileExist() => File.Exists(fileName);

        public override void addResultsBlock(ResultsBlock results_block)
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
                writer.WriteLine(",," + results_block.week_description);

                foreach (var curr_result in results_block.results)
                {
                    writer.Write(GenUtils.getInstance().ToLongString(curr_result.home_team) );
                    writer.Write(",");
                    writer.Write(curr_result.home_score);
                    writer.Write(",");
                    writer.Write(curr_result.away_score);
                    writer.Write(",");
                    writer.WriteLine(GenUtils.getInstance().ToLongString(curr_result.away_team) );
                }
            }
        }

        public override List<CommonTypes.Result> getAllResults()
        {
            List<CommonTypes.Result> result = new List<CommonTypes.Result>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                String curr_line = "";

                while ((curr_line = reader.ReadLine()) != null)
                {
                    if (!curr_line.StartsWith(",,"))
                    {
                        string[] parts = curr_line.Split(',');

                        var curr_result = new 
                            CommonTypes.Result(home_team  : GenUtils.getInstance().ToTeamName(parts[0]),
                                               home_score : ushort.Parse(parts[1]),
                                               away_score : ushort.Parse(parts[2]),
                                               away_team  : GenUtils.getInstance().ToTeamName(parts[3]));

                        result.Add(curr_result);
                    }
                }
            }
            return result;
        }

        public override List<ResultsBlock> getAllResultsBlocks()
        {
            var result = new List<ResultsBlock>();

            string curr_line;

            bool pending_fixtures = false; // True if fixtures still need adding to result

            ResultsBlock results_block = new ResultsBlock();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while ((curr_line = reader.ReadLine()) != null)
                {
                    // A 'week' row in the Excel file starts with two empty cells
                    if (curr_line.StartsWith(",,"))
                    {
                        if (pending_fixtures)
                        {
                            result.Add(results_block);
                            pending_fixtures = false;
                        }

                        results_block = new ResultsBlock();

                        // Remove the initial commas and fetch the week description
                        string[] week_descr_line = curr_line.Split(',');
                        results_block.week_description = "Week: " + week_descr_line[2];
                    }
                    else
                    {
                        string[] parts = curr_line.Split(',');

                        var curr_result = new
                            CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(parts[0]),
                                               home_score: ushort.Parse(parts[1]),
                                               away_score: ushort.Parse(parts[2]),
                                               away_team: GenUtils.getInstance().ToTeamName(parts[3]));

                        results_block.results.Add(curr_result);
                        pending_fixtures = true;
                    }
                }

            }
            if (pending_fixtures)
                result.Add(results_block);

            return result;

        }
    }

}