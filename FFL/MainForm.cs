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

            if(results.doesFileExist() )
                res_summary.reCalculate(results.getAllResults(),
                                        fixtures.readAllFixtures());
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

            var result = fixtures.fileExistsCanCreate();

            if (result)
            {
                fixtures.addText(week_name);

                // Alternate CBs are home and away
                for (int cb_idx = 0; cb_idx < GenUtils.NUM_TEAMS / 2; ++cb_idx)
                {
                    var home_str = all_cbs[cb_idx*2].Text;
                    var away_str = all_cbs[cb_idx*2 + 1].Text;

                    fixtures.addTeams(home: home_str, away: away_str);
                }
            }
            resetCBs();
            weekTB.Text = "";
            // Update the pane showing fixtures
            updateFixturesPane(false);
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
            if (!fixtures.fileExists())
            {
                if (inform)
                {
                    MessageBox.Show($"Can't find fixtures file. No fixtures found",
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
        private void updatePressed(object sender, EventArgs e)
        {
            updateFixturesPane(true);
        }

        private void commitResBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"This will put these results in the Results.csv file, remove these fixtures from the Fixtures.csv file and update the Results panes.",
                                         "Updating",
                                         MessageBoxButtons.OK);
            fixtures.deleteFirstBlock();
            updateFixturesPane(false);

            results.addText(ResultsWkLbl.Text);
            results.addResult(homeLbl1.Text, awayLbl1.Text,
                              (ushort)homeScore1.Value, (ushort)awayScore1.Value);

            results.addResult(homeLbl2.Text, awayLbl2.Text,
                              (ushort)homeScore2.Value, (ushort)awayScore2.Value);

            results.addResult(homeLbl3.Text, awayLbl3.Text,
                              (ushort)homeScore3.Value, (ushort)awayScore3.Value);

            results.addResult(homeLbl4.Text, awayLbl4.Text,
                              (ushort)homeScore4.Value, (ushort)awayScore4.Value);

            results.addResult(homeLbl5.Text, awayLbl5.Text,
                              (ushort)homeScore5.Value, (ushort)awayScore5.Value);

            results.addResult(homeLbl6.Text, awayLbl6.Text,
                              (ushort)homeScore6.Value, (ushort)awayScore6.Value);

            results.addResult(homeLbl7.Text, awayLbl7.Text,
                              (ushort)homeScore7.Value, (ushort)awayScore7.Value);

            results.addResult(homeLbl8.Text, awayLbl8.Text,
                              (ushort)homeScore8.Value, (ushort)awayScore8.Value);

            results.addResult(homeLbl9.Text, awayLbl9.Text,
                              (ushort)homeScore9.Value, (ushort)awayScore9.Value);

            results.addResult(homeLbl10.Text, awayLbl10.Text,
                              (ushort)homeScore10.Value, (ushort)awayScore10.Value);

            res_summary.reCalculate(results.getAllResults(),
                                    fixtures.readAllFixtures());

            commitResBtn.Enabled = false;
        }

        private void populateResultsClick(object sender, EventArgs e)
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

        private void fixturesResetClick(object sender, EventArgs e)
        {
            resetCBs();
        }
    }
}
