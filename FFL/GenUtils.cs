using System;
using System.Collections.Generic;

/// <summary>
/// General Purpose utility functions
/// </summary>
public class GenUtils
{
    // 
    // Make class a singleton
    //
    private static GenUtils instance = null;

    static public GenUtils getInstance()
    {
        if (instance == null)
            instance = new GenUtils();
        return instance;
    }

    static Dictionary<CommonTypes.TeamName, string> team_names = new Dictionary<CommonTypes.TeamName, string>();

    private GenUtils()
	{
        team_names.Add(CommonTypes.TeamName.ARS, ARS_STR);
        team_names.Add(CommonTypes.TeamName.BOU, BOU_STR);
        team_names.Add(CommonTypes.TeamName.BRI, BRI_STR);
        team_names.Add(CommonTypes.TeamName.BUR, BUR_STR);
        team_names.Add(CommonTypes.TeamName.CHE, CHE_STR);
        team_names.Add(CommonTypes.TeamName.EVE, EVE_STR);
        team_names.Add(CommonTypes.TeamName.HUD, HUD_STR);
        team_names.Add(CommonTypes.TeamName.LEI, LEI_STR);
        team_names.Add(CommonTypes.TeamName.LIV, LIV_STR);
        team_names.Add(CommonTypes.TeamName.MANC, MNC_STR);
        team_names.Add(CommonTypes.TeamName.MANU, MNU_STR);
        team_names.Add(CommonTypes.TeamName.NEW, NEW_STR);
        team_names.Add(CommonTypes.TeamName.PAL, PAL_STR);
        team_names.Add(CommonTypes.TeamName.SOT, SOT_STR);
        team_names.Add(CommonTypes.TeamName.STO, STO_STR);
        team_names.Add(CommonTypes.TeamName.SWA, SWA_STR);
        team_names.Add(CommonTypes.TeamName.TOT, TOT_STR);
        team_names.Add(CommonTypes.TeamName.WAT, WAT_STR);
        team_names.Add(CommonTypes.TeamName.WBA, WBA_STR);
        team_names.Add(CommonTypes.TeamName.WHA, WHA_STR);
    }

    public string ToStr(CommonTypes.TeamName team) => team_names[team];

    // TO DO: If performance becomes an issue, store a mapping from strings to 
    // TeamNames.
    public CommonTypes.TeamName ToTeamName(string team)
    {
        CommonTypes.TeamName result = new CommonTypes.TeamName();

        Array values = Enum.GetValues(typeof(CommonTypes.TeamName));

        Boolean found = false;

        foreach (CommonTypes.TeamName team_name in values)
        {
            if(team_names[team_name] == team)
            {
                result = team_name;
                found = true;
                break;
            }
        }

        if(!found)
        {
            throw new EntryPointNotFoundException("Can't find team:" + team);
        }
        return result;
    }

//    const ushort NUM_TEAMS = 20; // Needed?
    private const string ARS_STR = "Arsenal";
    private const string BOU_STR = "Bournemouth";
    private const string BRI_STR = "Brighton";
    private const string BUR_STR = "Burnley";
    private const string CHE_STR = "Chelsea";
    private const string EVE_STR = "Everton";
    private const string HUD_STR = "Huddersfield";
    private const string LEI_STR = "Leicester";
    private const string LIV_STR = "Liverpool";
    private const string PAL_STR = "Crystal Palace";
    private const string SOT_STR = "Southampton";
    private const string STO_STR = "Stoke";
    private const string SWA_STR = "Swansea";
    private const string TOT_STR = "Tottenham";
    private const string MNC_STR = "Man C";
    private const string MNU_STR = "Man U";
    private const string NEW_STR = "Newcastle";
    private const string WAT_STR = "Watford";
    private const string WBA_STR = "WBA";
    private const string WHA_STR = "West Ham";
}
