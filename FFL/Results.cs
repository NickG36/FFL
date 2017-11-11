﻿using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Results
/// </summary>
/// 
namespace FFL
{
    public abstract class Results
    {
        abstract public bool doesFileExist();

        abstract public void addText(String str);
        abstract public void addResult(String home_team, 
                                       String away_team,
                                       ushort home_score,
                                       ushort away_score);

        abstract public List<CommonTypes.Result> getAllResults();
    }
}
