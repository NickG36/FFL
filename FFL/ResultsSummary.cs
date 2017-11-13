using System;
using System.Collections.Generic;
using System.Windows.Forms;

/// <summary>
/// Summary description for ResultsSummary
/// This class is used to give an analysis of the results to date
/// </summary>

namespace FFL
{
    using ResultsByTeam =
       System.Collections.Generic.Dictionary<CommonTypes.TeamName, 
                                             List<CommonTypes.TeamResult>>;

    using GoalsByTeam = Dictionary<CommonTypes.TeamName, CommonTypes.GoalsCount>;

    using RankingByTeam = Dictionary<CommonTypes.TeamName, ushort>;

    using FixturesByTeam = Dictionary<CommonTypes.TeamName, List<CommonTypes.Fixture>>;

    public class ResultsSummary
    {
        /// <summary>
        /// Details of next few fixtures for a particular team
        /// </summary>
        private class FixtureDetails
        {
            public ushort num_fixtures_stored;
            public string[] next_fixtures;
            public ushort num_goals; // Conceded or scored dependant on use
            public ushort num_home_matches; // Num home matches in next few fixtures
            public ushort ave_opp_ranking; // Next 6 matches
            public const ushort NUM_MATCHES_CONSIDERED = 6;

            public FixtureDetails(ushort num_fixtures = NUM_MATCHES_CONSIDERED)
            {
                num_fixtures_stored = num_fixtures;
                next_fixtures = new string[num_fixtures_stored];
            }
        }

        /// <summary>
        /// formFixtureFields will return info needed to populate the upcoming fixture columns of
        /// the Attacking or Defending dataGridView. 
        /// It should be called with goals conceded for the goals_by_team parameter and 
        /// the defensive ranking list for the team_ranking to provide fixture data for the 
        /// Attacking dataGridView, and vice-versa for the Defending view.
        /// </summary>
        /// <param name="goals_by_team">Goals scored/conceded as appropriate</param>
        /// <param name="fixtures_by_team"></param>
        /// <param name="team_ranking">Attacking/defensive ranking</param>
        /// <param name="team_nm"></param>
        /// <returns></returns>
        private static FixtureDetails formFixtureFields(
                                    GoalsByTeam          goals_by_team,
                                    FixturesByTeam       fixtures_by_team,
                                    RankingByTeam        team_ranking,
                                    CommonTypes.TeamName team_nm)
        {
            var result = new FixtureDetails();

            List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

            ushort num_home_matches = 0;
            ushort tot_opp_goals = 0; // Scored/conceded as appropriate for this call
            ushort tot_opp_ranking = 0;

            for (int idx = 0; idx < FixtureDetails.NUM_MATCHES_CONSIDERED; ++idx)
            {
                // If we haven't got enough fixtures then just use what we have
                if ((next_fixtures.Count - idx - 1) < 0)
                {
                    result.next_fixtures[idx] = "-";
                    break;
                }

                CommonTypes.Fixture curr_fixture = next_fixtures[idx];
                CommonTypes.TeamName opponent = curr_fixture.opponent;

                tot_opp_goals += goals_by_team[opponent].last6;

                string opponent_str = GenUtils.getInstance().ToShortString(opponent);

                ushort ranking = team_ranking[opponent];
                opponent_str += ranking;

                tot_opp_ranking += ranking;

                if (curr_fixture.is_home)
                {
                    opponent_str += 'h';
                    ++num_home_matches;
                }
                else
                {
                    opponent_str += 'a';
                }

                result.next_fixtures[idx] = opponent_str;
            } // end opponent loop
            result.num_goals = tot_opp_goals;
            result.num_home_matches = num_home_matches;

            result.ave_opp_ranking = (ushort)(tot_opp_ranking / result.num_fixtures_stored);

            return result;
        }

        DataGridView attacking_grid;
        DataGridView defending_grid;

        public ResultsSummary(DataGridView att_grid, DataGridView def_grid)
        {
            attacking_grid = att_grid;
            defending_grid = def_grid;
        }

        private void resetGrid(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }

        /// <summary>
        /// Will add rows to the attacking dataGridView, with individual columns set up appropriately.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="fixtures_by_team"></param>
        /// <param name="goals_scored_by_team"></param>
        /// <param name="goals_conceded_by_team"></param>
        /// <param name="attacking_ranking"></param>
        /// <param name="defending_ranking"></param>
        private void populateAttackingTab(
                          DataGridView   dataGridView,
                          FixturesByTeam fixtures_by_team,
                          GoalsByTeam    goals_scored_by_team,
                          GoalsByTeam    goals_conceded_by_team,
                          RankingByTeam  attacking_ranking,
                          RankingByTeam  defending_ranking)
        {
            // Define column indices
            const int RANK_IDX = 0;
            const int TEAM_NAME_IDX = 1;
            const int GOALS_SCORED_10_IDX = 2;
            const int GOALS_SCORED_6_IDX = 3;
            const int GOALS_SCORED_TOT_IDX = 4;
            const int FIXTURE_1_IDX = 5;
            const int FIXTURE_2_IDX = 6;
            const int FIXTURE_3_IDX = 7;
            const int FIXTURE_4_IDX = 8;
            const int FIXTURE_5_IDX = 9;
            const int FIXTURE_6_IDX = 10;
            const int NUM_HOME_MATCHES_IDX = 11;
            const int AVE_OPP_RANK_IDX = 12;

            resetGrid(dataGridView);

            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

                string[] curr_row_str = new string[13];

                ushort att_rank = attacking_ranking[team_nm];

                if(att_rank < 10)
                  curr_row_str[RANK_IDX] = "0" + att_rank.ToString();
                else
                  curr_row_str[RANK_IDX] = att_rank.ToString();

                curr_row_str[TEAM_NAME_IDX] = GenUtils.getInstance().ToShortString(team_nm);
                curr_row_str[GOALS_SCORED_10_IDX] = goals_scored_by_team[team_nm].last10.ToString();
                curr_row_str[GOALS_SCORED_6_IDX] = goals_scored_by_team[team_nm].last6.ToString();
                curr_row_str[GOALS_SCORED_TOT_IDX] = goals_scored_by_team[team_nm].total.ToString();

                FixtureDetails fixture_dets = formFixtureFields(goals_conceded_by_team,
                                                                fixtures_by_team,
                                                                defending_ranking,
                                                                team_nm);
                curr_row_str[FIXTURE_1_IDX] = fixture_dets.next_fixtures[0];
                curr_row_str[FIXTURE_2_IDX] = fixture_dets.next_fixtures[1];
                curr_row_str[FIXTURE_3_IDX] = fixture_dets.next_fixtures[2];
                curr_row_str[FIXTURE_4_IDX] = fixture_dets.next_fixtures[3];
                curr_row_str[FIXTURE_5_IDX] = fixture_dets.next_fixtures[4];
                curr_row_str[FIXTURE_6_IDX] = fixture_dets.next_fixtures[5];

                curr_row_str[NUM_HOME_MATCHES_IDX] = fixture_dets.num_home_matches.ToString();
                curr_row_str[AVE_OPP_RANK_IDX] = fixture_dets.ave_opp_ranking.ToString();

                dataGridView.Rows.Add(curr_row_str);

            }

            for (int row_idx = 0; row_idx < 20; ++row_idx)
            {
                dataGridView.Rows[row_idx].ReadOnly = true;
            }

        }

        /// <summary>
        /// Will add rows to the defending dataGridView, with individual columns set up appropriately.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="fixtures_by_team"></param>
        /// <param name="goals_scored_by_team"></param>
        /// <param name="goals_conceded_by_team"></param>
        /// <param name="clean_sheets_by_team"></param>
        /// <param name="attacking_ranking"></param>
        /// <param name="defending_ranking"></param>

        private void populateDefendingTab(
                  DataGridView   dataGridView,
                  FixturesByTeam fixtures_by_team,
                  GoalsByTeam    goals_scored_by_team,
                  GoalsByTeam    goals_conceded_by_team,
                  GoalsByTeam    clean_sheets_by_team,
                  RankingByTeam  attacking_ranking,
                  RankingByTeam  defending_ranking)
        {
            // Define column indices
            const int RANK_IDX = 0;
            const int TEAM_NAME_IDX = 1;
            const int GOALS_CONCEDED_10_IDX = 2;
            const int GOALS_CONCEDED_6_IDX = 3;
            const int GOALS_CONCEDED_TOT_IDX = 4;
            const int CLEAN_SHEETS_10_IDX = 5;
            const int CLEAN_SHEETS_6_IDX = 6;
            const int CLEAN_SHEETS_TOT_IDX = 7;
            const int FIXTURE_1_IDX = 8;
            const int FIXTURE_2_IDX = 9;
            const int FIXTURE_3_IDX = 10;
            const int FIXTURE_4_IDX = 11;
            const int FIXTURE_5_IDX = 12;
            const int FIXTURE_6_IDX = 13;
            const int NUM_HOME_MATCHES_IDX = 14;
            const int AVE_OPP_RANK_IDX = 15;

            resetGrid(dataGridView);

            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

                string[] curr_row_str = new string[16];

                ushort att_rank = attacking_ranking[team_nm];

                if(att_rank < 10)
                    curr_row_str[RANK_IDX] = "0" + att_rank.ToString();
                else
                    curr_row_str[RANK_IDX] = att_rank.ToString();

                curr_row_str[TEAM_NAME_IDX] = GenUtils.getInstance().ToShortString(team_nm);
                curr_row_str[GOALS_CONCEDED_10_IDX]  = goals_conceded_by_team[team_nm].last10.ToString();
                curr_row_str[GOALS_CONCEDED_6_IDX]   = goals_conceded_by_team[team_nm].last6.ToString();
                curr_row_str[GOALS_CONCEDED_TOT_IDX] = goals_conceded_by_team[team_nm].total.ToString();

                curr_row_str[CLEAN_SHEETS_10_IDX]  = clean_sheets_by_team[team_nm].last10.ToString();
                curr_row_str[CLEAN_SHEETS_6_IDX]   = clean_sheets_by_team[team_nm].last6.ToString();
                curr_row_str[CLEAN_SHEETS_TOT_IDX] = clean_sheets_by_team[team_nm].total.ToString();

                FixtureDetails fixture_dets = formFixtureFields(goals_conceded_by_team,
                                                                fixtures_by_team,
                                                                defending_ranking,
                                                                team_nm);
                curr_row_str[FIXTURE_1_IDX] = fixture_dets.next_fixtures[0];
                curr_row_str[FIXTURE_2_IDX] = fixture_dets.next_fixtures[1];
                curr_row_str[FIXTURE_3_IDX] = fixture_dets.next_fixtures[2];
                curr_row_str[FIXTURE_4_IDX] = fixture_dets.next_fixtures[3];
                curr_row_str[FIXTURE_5_IDX] = fixture_dets.next_fixtures[4];
                curr_row_str[FIXTURE_6_IDX] = fixture_dets.next_fixtures[5];

                curr_row_str[NUM_HOME_MATCHES_IDX] = fixture_dets.num_home_matches.ToString();
                curr_row_str[AVE_OPP_RANK_IDX] = fixture_dets.ave_opp_ranking.ToString();

                dataGridView.Rows.Add(curr_row_str);
            }

            for (int row_idx = 0; row_idx < 20; ++row_idx)
            {
                dataGridView.Rows[row_idx].ReadOnly = true;
            }
        }

        public void reCalculate(List<CommonTypes.Result>   results,
                                List<CommonTypes.TwoTeams> fixtures)
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
            populateAttackingTab(attacking_grid, 
                                 fixtures_by_team,
                                 goals_scored_by_team  : goals_scored_by_team,
                                 goals_conceded_by_team: goals_conceded_by_team,
                                 attacking_ranking     : attacking_ranking,
                                 defending_ranking     : defending_ranking);

            populateDefendingTab(defending_grid,
                                 fixtures_by_team,
                                 goals_scored_by_team  : goals_scored_by_team,
                                 goals_conceded_by_team: goals_conceded_by_team,
                                 clean_sheets_by_team  : clean_sheets_by_team,
                                 attacking_ranking     : attacking_ranking,
                                 defending_ranking     : defending_ranking);
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