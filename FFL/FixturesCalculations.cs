using System;
using System.Collections.Generic;

/// <summary>
/// Utility methods to process fixtures
/// </summary>
/// 
namespace FFL
{
    using FixturesByTeam =
        Dictionary<CommonTypes.TeamName, List<CommonTypes.Fixture>>;

    /// <summary>
    /// Utility methods to manipulate fixtures
    /// </summary>
    class FixturesCalculations
    {
        /// <summary>
        /// Returns all the fixtures that involved the filter_team
        /// </summary>
        /// <param name="all_fixtures"></param>
        /// <param name="filter_team"></param>
        /// <returns></returns>
        public static List<CommonTypes.Fixture> filterAllFixtures(List<CommonTypes.TwoTeams> all_fixtures,
                                                                  CommonTypes.TeamName filter_team)
        {
            var result = new List<CommonTypes.Fixture>();

            foreach(CommonTypes.TwoTeams curr_res in all_fixtures)
            {
                // Match for home team?
                if (GenUtils.ToTeamName(curr_res.home) == filter_team)
                {
                    var curr_fixture
                        = new CommonTypes.Fixture(GenUtils.ToTeamName(curr_res.away),
                                                  true);
                    result.Add(curr_fixture);
                }
                else if (GenUtils.ToTeamName(curr_res.away) == filter_team)
                {
                    var curr_fixture
                        = new CommonTypes.Fixture(GenUtils.ToTeamName(curr_res.home),
                                                  false);
                    result.Add(curr_fixture);
                } // end else if

            }
            return result;
        }

        /// <summary>
        /// Looks through all the fixtures and provides a lists of fixtures by team
        /// </summary>
        /// <param name="all_fixtures"></param>
        /// <returns></returns>
        public static FixturesByTeam calcResultsByTeam(List<CommonTypes.TwoTeams> all_fixtures)
        {
            var result = new FixturesByTeam();

            foreach (CommonTypes.TeamName team_nm in
                       Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                var fixtures_this_team = filterAllFixtures(all_fixtures, team_nm);
                result.Add(team_nm, fixtures_this_team);
            }

            return result;
        }
    }
}