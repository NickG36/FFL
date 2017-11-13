using System;
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

        abstract public void addResultsBlock(ResultsBlock results_block);
            
        abstract public List<CommonTypes.Result> getAllResults();

        /// <summary>
        /// A ResultsBlock represents one set of results (normally one week's worth)
        /// </summary>
        public class ResultsBlock
        {
            public ResultsBlock()
            {
                results = new List<CommonTypes.Result>();
            }
            public string week_description;
            public List<CommonTypes.Result> results;
        }
        abstract public List<ResultsBlock> getAllResultsBlocks();

    }
}
