using System;
using System.Collections.Generic;

/// <summary>
/// Utility methods to process fixtures
/// </summary>
/// 
namespace FFL
{
    using FixturesByTeam =
        System.Collections.Generic.Dictionary<CommonTypes.TeamName,
                                              List<CommonTypes.Fixture>>;

    class FixturesCalculations
    {
        public static FixturesByTeam calcResultsByTeam(List<CommonTypes.TwoTeams> all_fixtures)
        {
            var result = new FixturesByTeam();
            // TO DO: add in
            return result;
        }
    }
}