using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFL
{
    public partial class MainForm : Form
    {
        List<ComboBox> all_cbs = new List<ComboBox>();
        List<String> all_teams_list = new List<String>();
        ReadOnlyCollection<String> all_teams;
        List<String> remaining_teams = new List<String>();
        Fixtures fixtures = new ExcelFixtures();
        Results results = new ExcelResults();
        ResultsSummary res_summary;

        public MainForm()
        {
            InitializeComponent();
            all_cbs.Add(homeCB1);
            all_cbs.Add(awayCB1);
            all_cbs.Add(homeCB2);
            all_cbs.Add(awayCB2);
            all_cbs.Add(homeCB3);
            all_cbs.Add(awayCB3);
            all_cbs.Add(homeCB4);
            all_cbs.Add(awayCB4);
            all_cbs.Add(homeCB5);
            all_cbs.Add(awayCB5);
            all_cbs.Add(homeCB6);
            all_cbs.Add(awayCB6);
            all_cbs.Add(homeCB7);
            all_cbs.Add(awayCB7);
            all_cbs.Add(homeCB8);
            all_cbs.Add(awayCB8);
            all_cbs.Add(homeCB9);
            all_cbs.Add(awayCB9);
            all_cbs.Add(homeCB10);
            all_cbs.Add(awayCB10);

            foreach (CommonTypes.TeamName team_nm in
                      Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                all_teams_list.Add(GenUtils.getInstance().ToLongString(team_nm));
            }

            all_teams = new ReadOnlyCollection<String>(all_teams_list);

            resetCBs();
            updateFixturesPane(false);
            res_summary = new ResultsSummary(att_grid: attDataGridView,
                                             def_grid: defDataGrid);

            if (results.doesFileExist())
            {
                res_summary.reCalculate(results.getAllResults(),
                                        fixtures.readAllFixtures());
                updateExistingResults(false);
            }
        }


        private void resetCBs()
        {
            remaining_teams = new List<String>(all_teams);

            foreach (ComboBox curr_cb in all_cbs)
            {
                curr_cb.Items.Clear();
                foreach (String curr_str in all_teams)
                {

                    curr_cb.Items.Add(curr_str);
                }
                curr_cb.Text = "";
            }

            enableCB(0);
        }

        private void onLoad(object sender, EventArgs e)
        {
            enableCB(0);
        }

        /// <summary>
        /// Enable the combo box with the given index and disable all others.
        /// If the index is too big then it will cope and not enable any combo box
        /// </summary>
        /// <param name="index"></param>
        private void enableCB(int index)
        {
            // Firstly, disable all combo boxes
            foreach (ComboBox curr_cb in all_cbs)
            {
                curr_cb.Enabled = false;
            }

            if(index < 20)
               all_cbs[index].Enabled = true;
        }

        private void valueChangedInCB(int CB_idx)
        {
            int idx = remaining_teams.IndexOf(all_cbs[CB_idx-1].SelectedItem.ToString());

            //Console.Write("Idx:" + idx);

            remaining_teams.RemoveAt(idx);

            if(CB_idx < GenUtils.NUM_TEAMS)
              resetCB(CB_idx);
        }

        private void resetCB(int idx)
        {
            ComboBox curr_cb = all_cbs[idx];
            curr_cb.Items.Clear();

            foreach(String curr_str in remaining_teams)
            {
                curr_cb.Items.Add(curr_str);
            }
            enableCB(idx);
        }

        // Give the option to hide all the valueChanged fns
        #region
        private void valueChanged1(object sender, EventArgs e)
        {
            valueChangedInCB(1);
        }

        private void valueChanged2(object sender, EventArgs e)
        {
            valueChangedInCB(2);
        }

        private void valueChanged3(object sender, EventArgs e)
        {
            valueChangedInCB(3);
        }

        private void valueChanged4(object sender, EventArgs e)
        {
            valueChangedInCB(4);
        }

        private void valueChanged5(object sender, EventArgs e)
        {
            valueChangedInCB(5);
        }

        private void valueChanged6(object sender, EventArgs e)
        {
            valueChangedInCB(6);
        }

        private void valueChanged7(object sender, EventArgs e)
        {
            valueChangedInCB(7);
        }

        private void valueChanged8(object sender, EventArgs e)
        {
            valueChangedInCB(8);
        }

        private void valueChanged9(object sender, EventArgs e)
        {
            valueChangedInCB(9);
        }

        private void valueChanged10(object sender, EventArgs e)
        {
            valueChangedInCB(10);
        }

        private void valueChanged11(object sender, EventArgs e)
        {
            valueChangedInCB(11);
        }

        private void valueChanged12(object sender, EventArgs e)
        {
            valueChangedInCB(12);
        }

        private void valueChanged13(object sender, EventArgs e)
        {
            valueChangedInCB(13);
        }

        private void valueChanged14(object sender, EventArgs e)
        {
            valueChangedInCB(14);
        }

        private void valueChanged15(object sender, EventArgs e)
        {
            valueChangedInCB(15);
        }

        private void valueChanged16(object sender, EventArgs e)
        {
            valueChangedInCB(16);
        }

        private void valueChanged17(object sender, EventArgs e)
        {
            valueChangedInCB(17);
        }

        private void valueChanged18(object sender, EventArgs e)
        {
            valueChangedInCB(18);
        }

        private void valueChanged19(object sender, EventArgs e)
        {
            valueChangedInCB(19);
        }

        private void valueChanged20(object sender, EventArgs e)
        {
            valueChangedInCB(20);
        }
        #endregion

        private void commitBtnPressed(object sender, EventArgs e)
        {
            var week_name = weekTB.Text;

            var new_fixtures = new Fixtures.FixturesBlock();

            if (fixtures.fileExistsCanCreate())
            {
                new_fixtures.week_description = week_name;

                // Alternate CBs are home and away
                for (int cb_idx = 0; cb_idx < GenUtils.NUM_TEAMS / 2; ++cb_idx)
                {
                    var home_str = all_cbs[cb_idx*2].Text;
                    var away_str = all_cbs[cb_idx*2 + 1].Text;
                    var new_fixture = new CommonTypes.TwoTeams(home: home_str,
                                                               away: away_str);

                    new_fixtures.fixtures.Add(new_fixture);
                }
            }

            fixtures.addBlock(new_fixtures);

            resetCBs();
            weekTB.Text = "";
            // Update the pane showing fixtures
            updateFixturesPane(false);
            updateExistingResults(false);
            res_summary.reCalculate(results.getAllResults(),
                                    fixtures.readAllFixtures());
        }

        /// <summary>
        /// This method is used during testing to populate the fixtures combo boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void populatePressed(object sender, EventArgs e)
        {
            foreach(ComboBox curr_cb in all_cbs)
            {
                // Select index 0 in each case
                curr_cb.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Updates the fixtures in fixtures pane.
        /// If no fixtures file is found, user will be informed if parameter is true
        /// </summary>
        /// <param name="inform">in</param>
        private void updateFixturesPane(bool inform)
        {
            if (!fixtures.doesFileExist())
            {
                if (inform)
                {
                    var path = fixtures.getPathToFile();

                    MessageBox.Show($"Can't find fixtures file at {path}. No fixtures found",
                                    "Fixtures file not found",
                                    MessageBoxButtons.OK);
                }
            }
            else
            {
                List<Fixtures.FixturesBlock> all_fixtures = fixtures.readAllFixtureBlocks();

                string existing_fix_str = "";

                foreach (var curr_fixture_block in all_fixtures)
                {
                    existing_fix_str += curr_fixture_block.week_description + "\n";

                    foreach (var curr_fix in curr_fixture_block.fixtures)
                    {
                        existing_fix_str += curr_fix + "\n";
                    }
                    existing_fix_str += "__________________________\n";
                }

                existingFixTB.Text = existing_fix_str;
            }
        }

        /// <summary>
        /// Updates the existing results in the results pane.
        /// If no results file is found, user will be informed if parameter is true
        /// </summary>
        /// <param name="inform">in</param>
        private void updateExistingResults(bool inform)
        {
            if (!results.doesFileExist())
            {
                if (inform)
                {
                    MessageBox.Show($"Can't find results file. No results found",
                                    "Results file not found",
                                    MessageBoxButtons.OK);
                }
            }
            else
            {
                existResTB.Text = "";

                var all_results_blocks = results.getAllResultsBlocks();

                string existing_res_str = "";

                foreach (var curr_block in all_results_blocks)
                {
                    existing_res_str += curr_block.week_description + "\n";

                    foreach (var curr_res in curr_block.results)
                    {
                        existing_res_str += curr_res + "\n";
                    }
                    existing_res_str += "___________________________\n";
                }

                existResTB.Text = existing_res_str;

            }
        }


        private void updatePressed(object sender, EventArgs e)
        {
            updateFixturesPane(true);
        }

        private void commitResBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"This will put these results in the Results.csv file, remove these fixtures from the Fixtures.csv file and update the Results panes.",
                            "Updating",
                            MessageBoxButtons.OK);

            fixtures.deleteFirstBlock();
            updateFixturesPane(false);

            var res_block = new Results.ResultsBlock();

            res_block.week_description = ResultsWkLbl.Text;

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl1.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl1.Text),
                                        home_score: (ushort)homeScore1.Value,
                                        away_score: (ushort)awayScore1.Value));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl2.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl2.Text),
                                        home_score: (ushort)homeScore2.Value,
                                        away_score: (ushort)awayScore2.Value));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl3.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl3.Text),
                                        home_score: (ushort)homeScore3.Value,
                                        away_score: (ushort)awayScore3.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl4.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl4.Text),
                                        home_score: (ushort)homeScore4.Value,
                                        away_score: (ushort)awayScore4.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl5.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl5.Text),
                                        home_score: (ushort)homeScore5.Value,
                                        away_score: (ushort)awayScore5.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl6.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl6.Text),
                                        home_score: (ushort)homeScore6.Value,
                                        away_score: (ushort)awayScore6.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl7.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl7.Text),
                                        home_score: (ushort)homeScore7.Value,
                                        away_score: (ushort)awayScore7.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl8.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl8.Text),
                                        home_score: (ushort)homeScore8.Value,
                                        away_score: (ushort)awayScore8.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl9.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl9.Text),
                                        home_score: (ushort)homeScore9.Value,
                                        away_score: (ushort)awayScore9.Value));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.getInstance().ToTeamName(homeLbl10.Text),
                                        away_team: GenUtils.getInstance().ToTeamName(awayLbl10.Text),
                                        home_score: (ushort)homeScore10.Value,
                                        away_score: (ushort)awayScore10.Value));

            results.addResultsBlock(res_block);
            updateExistingResults(false);

            res_summary.reCalculate(results.getAllResults(),
                                    fixtures.readAllFixtures());

            commitResBtn.Enabled = false;

            clearInputFields();
        }

        private void clearInputFields()
        {
            homeScore1.Value = 0; awayScore1.Value = 0;
            homeScore2.Value = 0; awayScore2.Value = 0;
            homeScore3.Value = 0; awayScore3.Value = 0;
            homeScore4.Value = 0; awayScore4.Value = 0;
            homeScore5.Value = 0; awayScore5.Value = 0;
            homeScore6.Value = 0; awayScore6.Value = 0;
            homeScore7.Value = 0; awayScore7.Value = 0;
            homeScore8.Value = 0; awayScore8.Value = 0;
            homeScore9.Value = 0; awayScore9.Value = 0;
            homeScore10.Value = 0; awayScore10.Value = 0;

            ResultsWkLbl.Text = "";
            homeLbl1.Text = ""; awayLbl1.Text = "";
            homeLbl2.Text = ""; awayLbl2.Text = "";
            homeLbl3.Text = ""; awayLbl3.Text = "";
            homeLbl4.Text = ""; awayLbl4.Text = "";
            homeLbl5.Text = ""; awayLbl5.Text = "";
            homeLbl6.Text = ""; awayLbl6.Text = "";
            homeLbl7.Text = ""; awayLbl7.Text = "";
            homeLbl8.Text = ""; awayLbl8.Text = "";
            homeLbl9.Text = ""; awayLbl9.Text = "";
            homeLbl10.Text = ""; awayLbl10.Text = "";
        }

        private void populateResultsClick(object sender, EventArgs e)
        {
            if (fixtures.doesFileExist())
            {
                Fixtures.FixturesBlock next_fixtures = fixtures.readFirstBlock();

                ResultsWkLbl.Text = next_fixtures.week_description;
                homeLbl1.Text = next_fixtures.fixtures[0].home;
                awayLbl1.Text = next_fixtures.fixtures[0].away;

                homeLbl2.Text = next_fixtures.fixtures[1].home;
                awayLbl2.Text = next_fixtures.fixtures[1].away;

                homeLbl3.Text = next_fixtures.fixtures[2].home;
                awayLbl3.Text = next_fixtures.fixtures[2].away;

                homeLbl4.Text = next_fixtures.fixtures[3].home;
                awayLbl4.Text = next_fixtures.fixtures[3].away;

                homeLbl5.Text = next_fixtures.fixtures[4].home;
                awayLbl5.Text = next_fixtures.fixtures[4].away;

                homeLbl6.Text = next_fixtures.fixtures[5].home;
                awayLbl6.Text = next_fixtures.fixtures[5].away;

                homeLbl7.Text = next_fixtures.fixtures[6].home;
                awayLbl7.Text = next_fixtures.fixtures[6].away;

                homeLbl8.Text = next_fixtures.fixtures[7].home;
                awayLbl8.Text = next_fixtures.fixtures[7].away;

                homeLbl9.Text = next_fixtures.fixtures[8].home;
                awayLbl9.Text = next_fixtures.fixtures[8].away;

                homeLbl10.Text = next_fixtures.fixtures[9].home;
                awayLbl10.Text = next_fixtures.fixtures[9].away;

                commitResBtn.Enabled = true;
            }
            else
            {
                var path = fixtures.getPathToFile();
                MessageBox.Show($"Can't find fixtures file: {path}. No fixtures found",
                                "Fixtures file not found",
                                MessageBoxButtons.OK);

            }
        }

        private void fixturesResetClick(object sender, EventArgs e)
        {
            resetCBs();
        }
    }
}
