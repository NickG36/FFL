using System;

/// <summary>
/// Summary description for Class1
/// </summary>

public class CommonTypes
{
    public struct TeamResult
    {
        public TeamResult(ushort   goals_for,
                          ushort   goals_against,
                          TeamName opponent,
                          Boolean  at_home)
        {
            this.goals_for = goals_for;
            this.goals_against = goals_against;
            this.opponent = opponent;
            this.at_home = at_home;
        }

        public readonly ushort goals_for;
        public readonly ushort goals_against;
        public readonly TeamName opponent;
        public readonly Boolean at_home;

        public override string ToString()
        {
            String result;
            if (at_home)
                result = "H";
            else
                result = "A";

            result += $"{goals_for} - {goals_against} v {opponent}";
            return result;
        }
    }

    public struct TwoTeams
    {
        public TwoTeams(string home, string away)
        {
            this.home = home;
            this.away = away;
        }
        public string home { get; }
        public string away { get; }

        public override string ToString() => $"{home} v {away}";
    }

    public enum TeamName { ARS, BOU, BRI, BUR, CHE,
                           PAL, EVE, HUD, LEI, LIV, 
                           SOT, STO, SWA, TOT, MANC,
                           MANU, NEW, WAT, WBA, WHA};


    public struct Result
    {
        readonly public TeamName home_team;
        readonly public TeamName away_team;
        readonly public ushort home_score;
        readonly public ushort away_score;

        public Result(TeamName home_team,
                      TeamName away_team, 
                      ushort   home_score,
                      ushort   away_score)
        {
            this.home_team = home_team;
            this.away_team = away_team;
            this.home_score = home_score;
            this.away_score = away_score;
        }
        public override string ToString() =>
            GenUtils.ToLongString(home_team) + " " + home_score + "-" + away_score + " " + GenUtils.ToLongString(away_team);

    }

    public struct Fixture
    {
        public readonly TeamName opponent;
        public readonly bool is_home;

        public Fixture(TeamName opponent, bool is_home)
        {
            this.opponent = opponent;
            this.is_home = is_home;
        }
    }

    // TO DO: Add ctor and make fields properties. Rm set
    public struct GoalsCount
    {
        public GoalsCount(ushort last6,
                          ushort last10,
                          ushort total)
        {
            this.last6 = last6;
            this.last10 = last10;
            this.total = total;
        }
        readonly public ushort last6;
        readonly public ushort last10;
        readonly public ushort total;

        public override string ToString() => $"All: {total}, last 10: {last10}, last 6: {last6}";
    }

    /// <summary>
    /// GoalsCountWithTeam is useful for storing in a SortedDictionary, so that teams
    /// with identical records (but different team names) can be differentiated
    /// </summary>
    public struct GoalsCountWithTeam
    {
        public GoalsCountWithTeam(GoalsCount goals_count, TeamName name)
        {
            this.goals = goals_count;
            this.name = name;
        }

        public GoalsCount goals { get; }
        public TeamName name { get; }

        public override string ToString() => base.ToString() + ", team:" + name;
    }

}
