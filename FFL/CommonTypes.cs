using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>

public class CommonTypes
{
    public struct TeamResult
    {
        public TeamResult(ushort goals_for,
                          ushort goals_against,
                          TeamName opponent,
                          Boolean  at_home)
        {
            this.goals_for = goals_for;
            this.goals_against = goals_against;
            this.opponent = opponent;
            this.at_home = at_home;
        }

        // TO DO: make properties with only a get:
        public Boolean at_home;
        public ushort goals_for;
        public ushort goals_against;
        public TeamName opponent;

        public override string ToString()
        {
            String result;
            if (at_home)
                result = "H";
            else
                result = "A";

            result += goals_for + "-" + goals_against + " v " + opponent;
            return result;
        }
    }

    public struct TwoTeams
    {
        public string home;
        public string away;

        public override string ToString() => home + " v " + away;
    }

    public enum TeamName { ARS, BOU, BRI, BUR, CHE,
                           EVE, HUD, LEI, LIV, PAL,
                           SOT, STO, SWA, TOT, MANC,
                           MANU, NEW, WAT, WBA, WHA};


    public struct Result
    {
        public TeamName home_team;
        public TeamName away_team;
        public ushort home_score;
        public ushort away_score;

        public override string ToString()
        {
            return GenUtils.getInstance().ToLongString(home_team) + " " + home_score + "-" + away_score + " " + GenUtils.getInstance().ToLongString(away_team);
        }
    }

    public struct Fixture
    {
        public TeamName opponent;
        public bool is_home;
    }

    // TO DO: Add ctor and make fields properties. Rm set
    public struct GoalsCount
    {
        public ushort last6;
        public ushort last10;
        public ushort total;

        public override string ToString() => "All: " + total + ", last 10: " + last10 + ", last 6:" + last6;
    }
    public struct GoalsCountWithTeam
    {
        public GoalsCountWithTeam(GoalsCount goals_count, TeamName name)
        {
            this.goals = goals_count;
            this.name = name;
        }

        public GoalsCount goals;
        public TeamName name;

        public override string ToString() => base.ToString() + ", team:" + name;
    }

}
