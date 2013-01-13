using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paper_Clip_App
{
    public partial class Form1 : Form
    {

        //variable declarations
        int maxTextCache = 15;
        bool textFoundInCache = false;
        String[] textCache = new String[53];
        int textCacheCounter;
        int textCacheChecker = 0;
        bool hideWhenSomethingIsSelected = false;


        public Form1()
        {

            //loads settings


            InitializeComponent();

            label2.Text = "Pop-up Info. Delay: " + trackBar2.Value.ToString() + "s";
            label1.Text = "Maximum Text Copies: " + maxTextCache;
            textCacheCounter = maxTextCache;

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width-378, Screen.PrimaryScreen.Bounds.Height-489);

            //sets tab labels
            tabPage1.Text = @" Copied Stuff ";
            tabPage2.Text = @"    Settings    ";
            tabPage3.Text = @"       About      ";

        }


        //bottom right button in 'settings' - closes P.C.
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }





        //context menu is menu that pops up when right clicked on icon in system tray
        //settings button context menu
        private void paperClipSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            button1.Text = @"Hide in Background";
            BringToFront();
        }

        //close paper clip button from context menu
        private void closePaperClipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //double clicked in system tray - should have same code as settings button from context menu (above)
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            button1.Text = @"Hide in Background";
            BringToFront();
        }


        


        //bottom left button in 'settings' - minimizes P.C.
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }




        //retrieves clipboard data every 100 ms
        private void timer1_Tick(object sender, EventArgs e)
        {

            hideWhenSomethingIsSelected = checkBox2.Checked;
            listBox1.Sorted = checkBox1.Checked;

            //text
            if (Clipboard.ContainsText() == true)
            {
                //check if text has been copied to paper clip before
                for (textCacheChecker = 0; textCacheChecker <= maxTextCache; textCacheChecker++)
                {
                    if (textCache[textCacheChecker] == Clipboard.GetText())
                    {
                        textFoundInCache = true;
                        textCacheChecker = maxTextCache + 1;
                    }
                    else
                    {
                        textFoundInCache = false;
                    }
                }

                //text hasn't been copied already before
                if (textFoundInCache == false)
                {

                    textCache[textCacheCounter] = Clipboard.GetText();

                    if (textCacheCounter == 0)
                    {
                        textCacheCounter = maxTextCache;
                    }

                        if (listBox1.Items.Count == maxTextCache)
                        {
                            try
                            {
                            listBox1.Items.RemoveAt(maxTextCache - textCacheCounter);
                            listBox1.Items.Insert(maxTextCache - textCacheCounter, Clipboard.GetText());
                            listBox1.SelectedIndex = maxTextCache - textCacheCounter;
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                            listBox1.Items.Add(Clipboard.GetText());
                            listBox1.SelectedIndex = maxTextCache - textCacheCounter;
                            }
                            catch { }
                        }

                        if (textCacheCounter == 0)
                        {
                            textCacheCounter = maxTextCache;
                        }
                        else
                        {
                            textCacheCounter--;
                        }
                }
            }


        }


        //called when something in listbox is clicked
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(listBox1.SelectedItem.ToString());
                toolTip1.Show(listBox1.Items[listBox1.SelectedIndex].ToString(), listBox1, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, trackBar2.Value*1000);
                if (hideWhenSomethingIsSelected == true && textFoundInCache == true)
                {
                    Hide();
                }
            }
            catch {}
        }





        //SETTINGS TAB

        //max. text slider
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                maxTextCache = trackBar1.Value;
                textCacheCounter = maxTextCache - 1;
                label1.Text = "Maximum Text Copies: " + maxTextCache;
                listBox1.Items.Clear();
                listBox1.Items.Add(Clipboard.GetText());
                Array.Clear(textCache, 0, 53);
                textCache[0] = Clipboard.GetText();
            }
            catch { }

        }

        //clear copied stuff button
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(Clipboard.GetText());
                maxTextCache = trackBar1.Value;
                textCacheCounter = maxTextCache - 1;
                Array.Clear(textCache, 0, 53);
                textCache[0] = Clipboard.GetText();
            }
            catch { }
        }

        //sort alphabetically checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                listBox1.Sorted = true;
            }
            else {
                listBox1.Sorted = false;
            }
        }

        //delay track bar
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Pop-up Info. Delay: "+trackBar2.Value.ToString()+"s";
        }



        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.ReshowDelay = 1;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                hideWhenSomethingIsSelected = true;
            }
            else
            {
                hideWhenSomethingIsSelected = false;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}
