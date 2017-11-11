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

    class FixturesCalculations
    {
        public static List<CommonTypes.Fixture> filterAllFixtures(List<CommonTypes.TwoTeams> all_fixtures,
                                                                  CommonTypes.TeamName filter_team)
        {
            var result = new List<CommonTypes.Fixture>();

            foreach(CommonTypes.TwoTeams curr_res in all_fixtures)
            {
                // Match for home team?
                if (GenUtils.getInstance().ToTeamName(curr_res.home) == filter_team)
                {
                    var curr_fixture
                        = new CommonTypes.Fixture(GenUtils.getInstance().ToTeamName(curr_res.away),
                                                  true);
                    result.Add(curr_fixture);
                }
                else if (GenUtils.getInstance().ToTeamName(curr_res.away) == filter_team)
                {
                    var curr_fixture
                        = new CommonTypes.Fixture(GenUtils.getInstance().ToTeamName(curr_res.home),
                                                  false);
                    result.Add(curr_fixture);
                } // end else if

            }
            return result;
        }

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