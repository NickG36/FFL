using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Fixtures
/// </summary>
namespace FFL
{ 
    public abstract class Fixtures
    {
        abstract public void setupReader();

        abstract public void addText(String str);
        abstract public void addTeams(String home, String away);

        public struct FixturesBlock
        {
            public string week_description;
            public List<CommonTypes.TwoTeams> fixtures;
        }

        abstract public FixturesBlock readFirstBlock();
    }
}