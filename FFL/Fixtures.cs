using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Fixtures
/// </summary>
namespace FFL
{ 
    public abstract class Fixtures
    {
        /// <summary>
        /// Does the Fixtures file exist?
        /// </summary>
        /// <returns></returns>
        abstract public bool doesFileExist();

        /// <summary>
        /// Does the Fixtures file exist? Gives user the option to
        /// create it if not.
        /// </summary>
        /// <returns></returns>
        abstract public bool fileExistsCanCreate();

        abstract public void addText(String str);
        abstract public void addTeams(String home, String away);

        /// <summary>
        /// A FixturesBlock represents one week's set of fixtures
        /// </summary>
        public class FixturesBlock
        {
            public FixturesBlock()
            {
                fixtures = new List<CommonTypes.TwoTeams>();
            }
            public string week_description;
            public List<CommonTypes.TwoTeams> fixtures;
        }

        abstract public FixturesBlock readFirstBlock();
        abstract public void deleteFirstBlock();

        abstract public List<FixturesBlock> readAllFixtureBlocks();
        abstract public List<CommonTypes.TwoTeams> readAllFixtures();
    }
}