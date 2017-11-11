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

        /// <summary>
        /// Does the Fixtures file exist?
        /// </summary>
        /// <returns></returns>
        abstract public bool fileExists();

        /// <summary>
        /// Does the Fixtures file exist? Gives user the option to
        /// create it if not.
        /// </summary>
        /// <returns></returns>
        abstract public bool fileExistsCanCreate();

        abstract public void addText(String str);
        abstract public void addTeams(String home, String away);

        public struct FixturesBlock
        {
            public string week_description;
            public List<CommonTypes.TwoTeams> fixtures;
        }

        abstract public FixturesBlock readFirstBlock();
        abstract public void deleteFirstBlock();

        abstract public List<FixturesBlock> readAllFixtureBlocks();
        abstract public List<CommonTypes.TwoTeams> readAllFixtures();
    }
}