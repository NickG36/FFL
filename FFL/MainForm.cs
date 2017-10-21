using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFLApp
{
    public partial class MainForm : Form
    {
        List<ComboBox> all_cbs = new List<ComboBox>();
        List<String> all_teams = new List<String>();
        List<String> remaining_teams = new List<String>();

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

            all_teams.Add("Arsenal");
            all_teams.Add("Chelsea");
            all_teams.Add("Liverpool");
            all_teams.Add("Man City");
            all_teams.Add("Man United");
            all_teams.Add("Stoke");
            all_teams.Add("Watford");
            all_teams.Add("West Ham");
            all_teams.Add("WBA");
            all_teams.Add("Newcastle");
            all_teams.Add("Tottenham");
            all_teams.Add("Everton");
            all_teams.Add("Southampton");
            all_teams.Add("Brighton");
            all_teams.Add("Bournemouth");
            all_teams.Add("Crystal Palace");
            all_teams.Add("Burnley");
            all_teams.Add("Huddersfield");
            all_teams.Add("Leicester");
            all_teams.Add("Swansea");

            remaining_teams = all_teams;

            foreach(ComboBox curr_cb in all_cbs)
            {
               foreach(String curr_str in all_teams)
                {

                    curr_cb.Items.Add(curr_str);
                }
            }
        }

        private void mouseClickFn(object sender, MouseEventArgs e)
        {
            textBox1.AppendText("Button pressed!");
            String firstStr = "one two three";

            List<String> myStrings = new List<string>();

            String currStr = "";

            Console.Write("In onLoad ");
            Console.Write("FirstStr:'" + firstStr + "'");

            for (int idx = 0; idx < firstStr.Length; ++idx)
            {
                Console.Write("Idx" + idx);

                Console.Write(firstStr[idx]);

                if (firstStr.Substring(idx, 1) != " ")
                {
                    currStr += firstStr[idx];
                }
                else
                {
                    currStr = "";
                    myStrings.Add(currStr);
                    Console.Write("Adding " + currStr);
                }
            }

         }

        private void onLoad(object sender, EventArgs e)
        {
            //byte[] byteArray = new byteArray(100);

            String firstStr = "one two three";

            List<String> myStrings = new List<string>();

            String currStr = "";

            Console.Write("In onLoad ");
            Console.Write("FirstStr:'" + firstStr + "'");

            for (int idx = 0; idx < firstStr.Length; ++idx)
            {
                Console.Write("Idx" + idx);

                Console.Write(firstStr[idx]);

                if (firstStr.Substring(idx, 1) != " ")
                {
                    currStr += firstStr[idx];
                }
                else
                {
                    currStr = "";
                    myStrings.Add(currStr);
                    Console.Write("Adding " + currStr);
                }
            }

        }

        private void valueChanged1(object sender, EventArgs e)
        {
            Console.Write("Chosen:" + homeCB1.SelectedItem);

            int idx = remaining_teams.IndexOf(homeCB1.SelectedItem.ToString() );

            Console.Write("Idx:" + idx);

            remaining_teams.RemoveAt(idx);

            resetCB(1);            
        }

        private void resetCB(int idx)
        {
            ComboBox curr_cb = all_cbs[idx];
            curr_cb.Items.Clear();

            foreach(String curr_str in remaining_teams)
            {
                curr_cb.Items.Add(curr_str);
            }
        }


        private void selIdxChanged1(object sender, EventArgs e)
        {
            Console.Write("selIdxChanged1");
        }

        private void selChangeCom1(object sender, EventArgs e)
        {
            Console.Write("selChangeCom1");
        }

        private void selValCh2(object sender, EventArgs e)
        {
            Console.Write("selChangeCom2");
        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
