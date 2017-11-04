using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public ResultsSummary(List<CommonTypes.Result> results,
                              List<CommonTypes.TwoTeams> fixtures,
                              DataGridView att_grid)
        {
            reCalculate(results, fixtures, att_grid);
        }

        private void reCalculate(List<CommonTypes.Result> results,
                                 List<CommonTypes.TwoTeams> fixtures,
                                 DataGridView att_grid)
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

            var fixtures_by_team = FixturesCalculations.calcResultsByTeam(fixtures);

            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

                int curr_col_idx = 0;
                string[] curr_row_str = new string[15];

                curr_row_str[curr_col_idx] = GenUtils.getInstance().ToShortString(team_nm);
                ++curr_col_idx;

                curr_row_str[curr_col_idx] = goals_scored_by_team[team_nm].last10.ToString();
                ++curr_col_idx;

                curr_row_str[curr_col_idx] = goals_scored_by_team[team_nm].last6.ToString();
                ++curr_col_idx;

                curr_row_str[curr_col_idx] = goals_scored_by_team[team_nm].total.ToString();
                ++curr_col_idx;

                ushort tot_opp_goals_conceded = 0;
                for (int idx = next_fixtures.Count - 1; idx >= 0; --idx)
                {
                    CommonTypes.Fixture curr_fixture = next_fixtures[idx];
                    CommonTypes.TeamName opponent = curr_fixture.opponent;

                    tot_opp_goals_conceded += goals_conceded_by_team[opponent].last6;

                    string opponent_str = GenUtils.getInstance().ToShortString(opponent);

                    ushort ranking = defending_ranking[opponent];
                    opponent_str += ranking;

                    if (curr_fixture.is_home)
                    {
                        opponent_str += 'h';
                    }
                    else
                    {
                        opponent_str += 'a';
                    }

                    curr_row_str[curr_col_idx] = opponent_str;
                    curr_col_idx++;
                } // end opponent loop
                curr_row_str[curr_col_idx] = ", " + tot_opp_goals_conceded;
                curr_col_idx++;

                att_grid.Rows.Add(curr_row_str);

            }

            for(int row_idx = 0; row_idx < 20; ++row_idx)
            {
                att_grid.Rows[row_idx].ReadOnly = true;
            }
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