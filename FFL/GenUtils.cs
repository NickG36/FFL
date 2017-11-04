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

    static Dictionary<CommonTypes.TeamName, string> long_team_names = new Dictionary<CommonTypes.TeamName, string>();
    static Dictionary<CommonTypes.TeamName, string> short_team_names = new Dictionary<CommonTypes.TeamName, string>();

    private GenUtils()
	{
        short_team_names.Add(CommonTypes.TeamName.ARS, ARS_STR);
        short_team_names.Add(CommonTypes.TeamName.BOU, BOU_STR);
        short_team_names.Add(CommonTypes.TeamName.BRI, BRI_STR);
        short_team_names.Add(CommonTypes.TeamName.BUR, BUR_STR);
        short_team_names.Add(CommonTypes.TeamName.CHE, CHE_STR);
        short_team_names.Add(CommonTypes.TeamName.EVE, EVE_STR);
        short_team_names.Add(CommonTypes.TeamName.HUD, HUD_STR);
        short_team_names.Add(CommonTypes.TeamName.LEI, LEI_STR);
        short_team_names.Add(CommonTypes.TeamName.LIV, LIV_STR);
        short_team_names.Add(CommonTypes.TeamName.MANC, MNC_STR);
        short_team_names.Add(CommonTypes.TeamName.MANU, MNU_STR);
        short_team_names.Add(CommonTypes.TeamName.NEW, NEW_STR);
        short_team_names.Add(CommonTypes.TeamName.PAL, PAL_STR);
        short_team_names.Add(CommonTypes.TeamName.SOT, SOT_STR);
        short_team_names.Add(CommonTypes.TeamName.STO, STO_STR);
        short_team_names.Add(CommonTypes.TeamName.SWA, SWA_STR);
        short_team_names.Add(CommonTypes.TeamName.TOT, TOT_STR);
        short_team_names.Add(CommonTypes.TeamName.WAT, WAT_STR);
        short_team_names.Add(CommonTypes.TeamName.WBA, WBA_STR);
        short_team_names.Add(CommonTypes.TeamName.WHA, WHA_STR);

        long_team_names.Add(CommonTypes.TeamName.ARS, ARSENAL_STR);
        long_team_names.Add(CommonTypes.TeamName.BOU, BOURNEMOUTH_STR);
        long_team_names.Add(CommonTypes.TeamName.BRI, BRIGHTON_STR);
        long_team_names.Add(CommonTypes.TeamName.BUR, BURNLEY_STR);
        long_team_names.Add(CommonTypes.TeamName.CHE, CHELSEA_STR);
        long_team_names.Add(CommonTypes.TeamName.EVE, EVERTON_STR);
        long_team_names.Add(CommonTypes.TeamName.HUD, HUDDERSFIELD_STR);
        long_team_names.Add(CommonTypes.TeamName.LEI, LEICESTER_STR);
        long_team_names.Add(CommonTypes.TeamName.LIV, LIVERPOOL_STR);
        long_team_names.Add(CommonTypes.TeamName.MANC, MAN_CITY_STR);
        long_team_names.Add(CommonTypes.TeamName.MANU, MAN_UTD_STR);
        long_team_names.Add(CommonTypes.TeamName.NEW, NEWCASTLE_STR);
        long_team_names.Add(CommonTypes.TeamName.PAL, PALACE_STR);
        long_team_names.Add(CommonTypes.TeamName.SOT, SOUTHAMPTON_STR);
        long_team_names.Add(CommonTypes.TeamName.STO, STOKE_STR);
        long_team_names.Add(CommonTypes.TeamName.SWA, SWANSEA_STR);
        long_team_names.Add(CommonTypes.TeamName.TOT, TOTTENHAM_STR);
        long_team_names.Add(CommonTypes.TeamName.WAT, WATFORD_STR);
        long_team_names.Add(CommonTypes.TeamName.WBA, WEST_BROM_STR);
        long_team_names.Add(CommonTypes.TeamName.WHA, WEST_HAM_STR);
    }

    public string ToLongString(CommonTypes.TeamName team) => long_team_names[team];
    public string ToShortString(CommonTypes.TeamName team) => short_team_names[team];

    // TO DO: If performance becomes an issue, store a mapping from strings to 
    // TeamNames.
    public CommonTypes.TeamName ToTeamName(string team)
    {
        CommonTypes.TeamName result = new CommonTypes.TeamName();

        Array values = Enum.GetValues(typeof(CommonTypes.TeamName));

        Boolean found = false;

        foreach (CommonTypes.TeamName team_name in values)
        {
            if(long_team_names[team_name] == team)
            {
                result = team_name;
                found = true;
                break;
            }
            else if(short_team_names[team_name] == team)
            {
                result = team_name;
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new EntryPointNotFoundException("Can't find team:" + team);
        }
        return result;
    }

//    const ushort NUM_TEAMS = 20; // Needed?

    // Long team names:
    private const string ARSENAL_STR = "Arsenal";
    private const string BOURNEMOUTH_STR = "Bournemouth";
    private const string BRIGHTON_STR = "Brighton";
    private const string BURNLEY_STR = "Burnley";
    private const string CHELSEA_STR = "Chelsea";
    private const string EVERTON_STR = "Everton";
    private const string HUDDERSFIELD_STR = "Huddersfield";
    private const string LEICESTER_STR = "Leicester";
    private const string LIVERPOOL_STR = "Liverpool";
    private const string PALACE_STR = "Crystal Palace";
    private const string SOUTHAMPTON_STR = "Southampton";
    private const string STOKE_STR = "Stoke";
    private const string SWANSEA_STR = "Swansea";
    private const string TOTTENHAM_STR = "Tottenham";
    private const string MAN_CITY_STR = "Man C";
    private const string MAN_UTD_STR = "Man U";
    private const string NEWCASTLE_STR = "Newcastle";
    private const string WATFORD_STR = "Watford";
    private const string WEST_BROM_STR = "WBA";
    private const string WEST_HAM_STR = "West Ham";

    // Short team names:
    private const string ARS_STR = "Ars";
    private const string BOU_STR = "Bou";
    private const string BRI_STR = "Bri";
    private const string BUR_STR = "Bur";
    private const string CHE_STR = "Che";
    private const string EVE_STR = "Eve";
    private const string HUD_STR = "Hud";
    private const string LEI_STR = "Lei";
    private const string LIV_STR = "Liv";
    private const string PAL_STR = "Cry";
    private const string SOT_STR = "Sou";
    private const string STO_STR = "Sto";
    private const string SWA_STR = "Swa";
    private const string TOT_STR = "Tot";
    private const string MNC_STR = "MnC";
    private const string MNU_STR = "MnU";
    private const string NEW_STR = "New";
    private const string WAT_STR = "Wat";
    private const string WBA_STR = "WBA";
    private const string WHA_STR = "WHa";

}
