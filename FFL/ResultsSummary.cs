using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for ResultsSummary
/// This class is used to give an analysis of the results to date
/// </summary>

namespace FFL
{
    using ResultList = List<CommonTypes.TeamResult>;

    using ResultsByTeam =
       System.Collections.Generic.Dictionary<CommonTypes.TeamName, 
                                             List<CommonTypes.TeamResult>>;

    public class ResultsSummary
    {
        //List<CommonTypes.Result> all_results;
        //ResultsByTeam results_by_team;
        //GoalsByTeam attacking_rankings;

        public ResultsSummary(List<CommonTypes.Result> results)
        {
            calculate(results);
        }

        private void calculate(List<CommonTypes.Result> results)
        {
            // Calculate all results team by team
            ResultsByTeam results_by_team = ResultsCalculations.calcResultsByTeam(results);

            // Calculate goals per period team by team
            // 
            // Pass method callbacks in
            //
            var goals_scored_by_team =
                ResultsCalculations.calcGoalsScoredConcededByTeam(results_by_team, getGoalsScoredCB);

            var goals_conceded_by_team =
                ResultsCalculations.calcGoalsScoredConcededByTeam(results_by_team, getGoalsConcededCB);

            var clean_sheets_by_team =
                ResultsCalculations.calcGoalsScoredConcededByTeam(results_by_team, getCleanSheetsCB);

            var attacking_ranking = ResultsCalculations.calcAttackingDefRanking(goals_scored_by_team);
            var defending_ranking = ResultsCalculations.calcAttackingDefRanking(clean_sheets_by_team);
        }

        //
        // Define the delegates
        //
        private static ushort getGoalsScoredCB(CommonTypes.TeamResult score) => score.goals_for;
        private static ushort getGoalsConcededCB(CommonTypes.TeamResult score) => score.goals_against;

        private static ushort getCleanSheetsCB(CommonTypes.TeamResult score)
        {
            ushort result = 0;

            if (score.goals_against == 0)
                result = 1;

            return result;
        }

     }
}