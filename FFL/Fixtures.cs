using System;
using System.Collections.Generic;

/// <summary>
/// Used to interface with the Fixtures store
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

        /// <summary>
        /// Used in error messages
        /// </summary>
        /// <returns></returns>
        abstract public string getPathToFile();
        /// <summary>
        /// A FixturesBlock represents one set of fixtures (normally one week's worth)
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

        abstract public void addBlock(FixturesBlock new_fixtures);

        abstract public FixturesBlock readFirstBlock();
        abstract public void deleteFirstBlock();

        abstract public List<FixturesBlock> readAllFixtureBlocks();
        abstract public List<CommonTypes.TwoTeams> readAllFixtures();
    }
}