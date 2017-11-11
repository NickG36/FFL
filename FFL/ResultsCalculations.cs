using System;
using System.Collections.Generic;

/// <summary>
/// Utility methods to process results
/// </summary>
namespace FFL
{
    using GoalsByTeam = Dictionary<CommonTypes.TeamName, CommonTypes.GoalsCount>;
    using ResultList = List<CommonTypes.TeamResult>;
    using ResultsByTeam =
       System.Collections.Generic.Dictionary<CommonTypes.TeamName,
                                             List<CommonTypes.TeamResult>>;

    public class ResultsCalculations
    {
        /// <summary>
        /// Used to filter the complete set of results to form a list containing just
        /// a particular team's results
        /// </summary>
        /// <param name="all_res"></param>
        /// <param name="filter_team"></param>
        /// <returns>ResultList</returns>
        private static ResultList filterAllResults(List<CommonTypes.Result> all_results,
                                                   CommonTypes.TeamName filter_team)
        {
            ResultList result = new ResultList();

            foreach (CommonTypes.Result curr_res in all_results)
            {
                // Match for home team?
                if (curr_res.home_team == filter_team)
                {
                    var filtered_result = new CommonTypes.TeamResult(goals_for: curr_res.home_score,
                                                                     goals_against: curr_res.away_score,
                                                                     opponent: curr_res.away_team,
                                                                     at_home: true);
                    result.Add(filtered_result);
                }
                else if (curr_res.away_team == filter_team)
                {
                    var filtered_result = new CommonTypes.TeamResult(goals_for: curr_res.away_score,
                                                                     goals_against: curr_res.home_score,
                                                                     opponent: curr_res.home_team,
                                                                     at_home: false);
                    result.Add(filtered_result);
                } // end else if
            } // end foreach
            return result;

        } // end filterAllResults

        /// <summary>
        /// calcResultsByTeam returns a dictionary type which maps each team onto a list
        /// of results for that team
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static ResultsByTeam calcResultsByTeam(List<CommonTypes.Result> all_results)
        {
            ResultsByTeam results_by_team = new ResultsByTeam();

            foreach (CommonTypes.TeamName team_nm in
                       Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                var results_this_team = filterAllResults(all_results, team_nm);
                results_by_team.Add(team_nm, results_this_team);
            }
            return results_by_team;
        }

        /// <summary>
        /// Calculates the number of goals scored or conceded by a particular team, dependant on the
        /// delegate passed in.
        /// </summary>
        /// <param name="results_by_team"></param>
        /// <param name="goals_callback"></param>
        /// <returns></returns>
        public static GoalsByTeam calcGoalsScoredConcededByTeam(ResultsByTeam results_by_team,
                                                                  Func<CommonTypes.TeamResult, ushort> goals_callback)
        {
            var result = new GoalsByTeam();
            foreach (CommonTypes.TeamName curr_team in results_by_team.Keys)
            {
                CommonTypes.GoalsCount goals_count = calcGoalsEachPeriod(results_by_team[curr_team],
                                                                         goals_callback);
                result.Add(curr_team, goals_count);
            }

            return result;
        }

        /// <summary>
        /// Calculate the numbers of goals scored/conceded/ clean sheets each time period,
        /// dependant on the delegate used.
        /// </summary>
        /// <param name="team_results"></param>
        /// <param name="goals_method"></param>
        /// <returns></returns>
        private static CommonTypes.GoalsCount calcGoalsEachPeriod(List<CommonTypes.TeamResult> team_results,
                                                                  Func<CommonTypes.TeamResult, ushort> goals_method)
        {
            const ushort SIX_WEEKS = 6;
            const ushort TEN_WEEKS = 10;

            // Loop through last 6 matches, last 10 and all season
            int num_results = team_results.Count;

            ushort total = 0;
            ushort last6 = 0;
            ushort last10 = 0;

            for (int week = num_results - 1; week >= 0; week--)
            {
                // 
                // Use delegate to calculate either goals scored/conceded or clean sheets as appropriate
                //
                ushort goals_value = goals_method(team_results[week]);
                total += goals_value;

                if ((num_results - week) < SIX_WEEKS)
                    last6 += goals_value;

                if ((num_results - week) < TEN_WEEKS)
                    last10 += goals_value;
            }
            return new CommonTypes.GoalsCount(last6 : last6,
                                              last10: last10,
                                              total : total);
        }

        public static Dictionary<CommonTypes.TeamName, ushort> calcAttackingDefRanking(GoalsByTeam goals_scored_conceded)
        {
            var result = new Dictionary<CommonTypes.TeamName, ushort>();

            var ranked_order = new SortedDictionary<CommonTypes.GoalsCountWithTeam,
                                                    CommonTypes.TeamName>(new GoalsScoredComparitor());
            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                CommonTypes.GoalsCount goals = goals_scored_conceded[team_nm];

                var goals_with_team = new CommonTypes.GoalsCountWithTeam(goals, team_nm);

                ranked_order.Add(goals_with_team, team_nm);
            }

            // Add by team into result
            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                ushort rank = 0;
                foreach (var curr_item in ranked_order)
                {
                    ++rank;
                    if (curr_item.Value == team_nm)
                    {
                        result.Add(team_nm, rank);
                        break;
                    }
                }
            }
            return result;

        }

        class GoalsScoredComparitor : IComparer<CommonTypes.GoalsCountWithTeam>
        {
            public int Compare(CommonTypes.GoalsCountWithTeam lhs,
                               CommonTypes.GoalsCountWithTeam rhs)
            {
                // Rate on the basis of goals scored/clean sheets in last 6 matches.
                // The other time periods are considered to be lower priority
                int result = 0;

                if (lhs.goals.last6 != rhs.goals.last6)
                {
                    if (lhs.goals.last6 > rhs.goals.last6)
                        result = -1;
                    else
                        result = 1;
                }
                else if (lhs.goals.last10 != rhs.goals.last10)
                {
                    if (lhs.goals.last10 > rhs.goals.last10)
                        result = -1;
                    else
                        result = 1;
                }
                else if (lhs.goals.total != rhs.goals.total)
                {
                    if (lhs.goals.total > rhs.goals.total)
                        result = -1;
                    else
                        result = 1;
                }
                else if (lhs.name != rhs.name)
                {
                    if (lhs.name > rhs.name)
                        result = -1;
                    else
                        result = 1;
                }
                return result;
            } // end Compare
        } // end class GoalsScoredComparitor
    }
}