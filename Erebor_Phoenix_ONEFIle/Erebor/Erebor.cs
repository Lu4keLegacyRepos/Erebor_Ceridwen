using Phoenix.EreborPlugin.Extensions;
using Phoenix.WorldData;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin.Erebor

{
    public delegate void CheckAll();
    [PhoenixWindowTabPage("Erebor")]
    public partial class Erebor : UserControl
    {
        public static Erebor instance;
        RuneTree rt;
        public Erebor()
        {
            try
            {
                InitializeComponent();
                rt = new RuneTree();
                instance = this;
                tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }

        }



        private void btn_Load_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    rt.GetRunes();
                    rt.FillTreeView( Runes);
                    break;
                case 1:
                    Main.instance.EqipSet.Add();
                    break;
                case 2:
                    Main.instance.Weapons.Add();
                    break;
                case 3:
                    Main.instance.AHeal.Add();
                    break;
                case 4:
                    Main.instance.Track.Add();
                    break;
            }
            Main.instance.EqipSet.fillListBox(listBox1);
            Main.instance.Weapons.fillListBox(listBox2);
            Main.instance.AHeal.fillListBox(listBox3);
            Main.instance.Track.fillListBox(listBox4);

        }

        private void btn_Refrash_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    rt.FillTreeView( Runes);
                    break;
                case 1:
                    Main.instance.EqipSet.fillListBox(listBox1);
                    break;
                case 2:
                    Main.instance.Weapons.fillListBox(listBox2);
                    break;
                case 3:
                    Main.instance.AHeal.fillListBox(listBox3);
                    break;
                case 4:
                    Main.instance.Track.fillListBox(listBox4);
                    break;
                case 5:
                    Main.instance.Items.fillListView(listView1);
                    break;
            }


        }
        private void button9_Click(object sender, EventArgs e)
        {

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (Runes.SelectedNode == null) return;
                    foreach (Rune r in rt.Runes.Where(run => run.id.ToString() == Runes.SelectedNode.Tag.ToString()))
                    {
                        RuneTree.instance.findRune(r);
                        r.RecallSvitek();
                    }
                    break;
                case 1:
                    if (listBox1.SelectedIndex >= 0)
                    {
                        UO.PrintInformation("Zamer odkladaci batoh");
                        Main.instance.EqipSet.equipy[listBox1.SelectedIndex].DressOnly();
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (Runes.SelectedNode == null) return;
                    foreach (Rune r in rt.Runes.Where(run => run.id.ToString() == Runes.SelectedNode.Tag.ToString()))
                    {
                        RuneTree.instance.findRune(r);
                        r.Recall();
                    }
                    break;
                case 1:
                    if (listBox1.SelectedIndex >= 0)
                    {
                        UO.PrintInformation("Zamer odkladaci batoh");
                        Main.instance.EqipSet.equipy[listBox1.SelectedIndex].Dress(new UOItem(UIManager.TargetObject()));
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (Runes.SelectedNode == null) return;
                    foreach (Rune r in rt.Runes.Where(run => run.id.ToString() == Runes.SelectedNode.Tag.ToString()))
                    {
                        RuneTree.instance.findRune(r);
                        r.Gate();
                    }
                    break;
                case 1:
                    if(listBox1.SelectedIndex>=0)
                    Main.instance.EqipSet.Remove(listBox1.SelectedIndex);
                    break;
                case 2:
                    if (listBox2.SelectedIndex >= 0)
                        Main.instance.Weapons.Remove(listBox2.SelectedIndex);
                    break;
                case 3:
                    if (listBox3.SelectedIndex >= 0)
                        Main.instance.AHeal.Remove(listBox3.SelectedIndex);
                    break;
                case 4:
                    if (listBox4.SelectedIndex >= 0)
                        Main.instance.Track.Remove(listBox4.SelectedIndex);
                    break;
            }
            Main.instance.EqipSet.fillListBox(listBox1);
            Main.instance.Weapons.fillListBox(listBox2);
            Main.instance.AHeal.fillListBox(listBox3);
            Main.instance.Track.fillListBox(listBox4);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    button9.Enabled = true;
                    button9.Visible = true;
                    button9.Text = "Recall-S";

                    button8.Enabled = true;
                    button8.Visible = true;
                    button8.Text = "Recall";

                    button7.Enabled = true;
                    button7.Visible = true;
                    button7.Text = "Gate";

                    btn_Refrash.Enabled = true;
                    btn_Refrash.Visible = true;
                    btn_Refrash.Text = "Refresh";

                    btn_Load.Visible = true;
                    btn_Load.Enabled = true;
                    btn_Load.Text = "Scan";

                    break;
                case 1:
                    button9.Enabled = true;
                    button9.Visible = true;
                    button9.Text = "Dress";

                    button8.Enabled = true;
                    button8.Visible = true;
                    button8.Text = "Un/Dress";

                    button7.Enabled = true;
                    button7.Visible = true;
                    button7.Text = "Delete";

                    btn_Refrash.Enabled = true;
                    btn_Refrash.Visible = true;
                    btn_Refrash.Text = "Refresh";

                    btn_Load.Visible = true;
                    btn_Load.Enabled = true;
                    btn_Load.Text = "Add";

                    break;
                case 2:
                    button9.Enabled = false;
                    button9.Visible = false;
                    button9.Text = "Recall-S";

                    button8.Enabled = false;
                    button8.Visible = false;
                    button8.Text = "Recall";

                    button7.Enabled = true;
                    button7.Visible = true;
                    button7.Text = "Delete";

                    btn_Refrash.Enabled = true;
                    btn_Refrash.Visible = true;
                    btn_Refrash.Text = "Refresh";

                    btn_Load.Visible = true;
                    btn_Load.Enabled = true;
                    btn_Load.Text = "Add";

                    break;
                case 3:
                    button9.Enabled = false;
                    button9.Visible = false;
                    button9.Text = "Recall-S";

                    button8.Enabled = false;
                    button8.Visible = false;
                    button8.Text = "Recall";

                    button7.Enabled = true;
                    button7.Visible = true;
                    button7.Text = "Delete";

                    btn_Refrash.Enabled = true;
                    btn_Refrash.Visible = true;
                    btn_Refrash.Text = "Refresh";

                    btn_Load.Visible = true;
                    btn_Load.Enabled = true;
                    btn_Load.Text = "Add";

                    break;
                case 4:
                    button9.Enabled = false;
                    button9.Visible = false;
                    button9.Text = "Recall-S";

                    button8.Enabled = false;
                    button8.Visible = false;
                    button8.Text = "Recall";

                    button7.Enabled = true;
                    button7.Visible = true;
                    button7.Text = "Delete";

                    btn_Refrash.Enabled = true;
                    btn_Refrash.Visible = true;
                    btn_Refrash.Text = "Refresh";

                    btn_Load.Visible = true;
                    btn_Load.Enabled = true;
                    btn_Load.Text = "Add";
                    break;

            }
            button7.Refresh();
            button8.Refresh();
            button9.Refresh();
            btn_Load.Refresh();
            btn_Refrash.Refresh();
        }








        //Settings parts
        private void chbAutodrink_CheckedChanged(object sender, EventArgs e)
        {
            if(Main.instance.AutoDrink!= chbAutodrink.Checked)
            Main.instance.AutoDrink = chbAutodrink.Checked;

        }

        private void chbAutoArrow_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Spells.AutoArrow != chbAutoArrow.Checked)
                Main.instance.Spells.AutoArrow = chbAutoArrow.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.HitBandage != checkBox6.Checked)
                Main.instance.HitBandage = checkBox6.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Amorf.Amorf != checkBox1.Checked)
                Main.instance.Amorf.Amorf = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.CorpseHide != checkBox2.Checked)
                Main.instance.CorpseHide = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.DoLot != checkBox3.Checked)
                Main.instance.DoLot = checkBox3.Checked;

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Food != checkBox5.Checked)
                Main.instance.Lot.Food = checkBox5.Checked;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Leather != checkBox4.Checked)
                Main.instance.Lot.Leather = checkBox4.Checked;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Bolts != checkBox13.Checked)
                Main.instance.Lot.Bolts = checkBox13.Checked;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Extend1 != checkBox12.Checked)
                Main.instance.Lot.Extend1 = checkBox12.Checked;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Extend2 != checkBox11.Checked)
                Main.instance.Lot.Extend2 = checkBox11.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UO.PrintWarning("Zamer item, ktery chces lotit");
            UOItem it = new UOItem(UIManager.TargetObject());
            Main.instance.Lot.extend1_type = new Graphic(it.Graphic);
            button1.Text = Main.instance.Lot.extend1_type.ToString();
            button1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UO.PrintWarning("Zamer item, ktery chces lotit");
            UOItem it = new UOItem(UIManager.TargetObject());
            Main.instance.Lot.extend2_type = new Graphic(it.Graphic);
            button2.Text = Main.instance.Lot.extend1_type.ToString();
            button2.Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UO.PrintInformation("Zamer Kuchatko");
            Main.instance.Lot.CarvTool = new UOItem(UIManager.TargetObject());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            UO.PrintInformation("Zamer batoh");
            Main.instance.Lot.LotBag = new UOItem(UIManager.TargetObject());
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Feathers != checkBox9.Checked)
                Main.instance.Lot.Feathers = checkBox9.Checked;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Gems != checkBox8.Checked)
                Main.instance.Lot.Gems = checkBox8.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.Lot.Reageants != checkBox7.Checked)
                Main.instance.Lot.Reageants = checkBox7.Checked;
        }




        public void CheckAll()
        {

                textBox5.Text = Main.instance.GWWidth.ToString();
                textBox7.Text = Main.instance.GWHeight.ToString();
                textBox3.Text = Main.instance.ActualClass.minHP.ToString();
                textBox2.Text = Main.instance.ActualClass.criticalHits.ToString();
                textBox1.Text = Main.instance.GoldLimit.ToString();
                checkBox7.Checked = Main.instance.Lot.Reageants;
                checkBox8.Checked = Main.instance.Lot.Gems;
                checkBox9.Checked = Main.instance.Lot.Feathers;
                button2.Text = Main.instance.Lot.extend1_type.ToString();
                button1.Text = Main.instance.Lot.extend1_type.ToString();
                checkBox11.Checked = Main.instance.Lot.Extend2;
                checkBox12.Checked = Main.instance.Lot.Extend1;
                checkBox13.Checked = Main.instance.Lot.Bolts;
                checkBox4.Checked = Main.instance.Lot.Leather;
                checkBox5.Checked = Main.instance.Lot.Food;
                checkBox3.Checked = Main.instance.DoLot;
                checkBox2.Checked = Main.instance.CorpseHide;
                checkBox1.Checked = Main.instance.Amorf.Amorf;
                checkBox6.Checked = Main.instance.HitBandage;
                chbAutoArrow.Checked = Main.instance.Spells.AutoArrow;
                chbAutodrink.Checked = Main.instance.AutoDrink;
                checkBox10.Checked = Main.instance.PrintAnim;
                checkBox14.Checked = Main.instance.HitTrack;
                textBox4.Text = Main.instance.ActualClass.hidDelay.ToString();
                rt.FillTreeView(Runes);

                Main.instance.EqipSet.fillListBox(listBox1);
                Main.instance.Weapons.fillListBox(listBox2);
                Main.instance.AHeal.fillListBox(listBox3);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SwitchabeHotkeys.instance.Add();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SwitchabeHotkeys.instance.Clear();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);

        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Main.instance.GoldLimit != ushort.Parse(textBox1.Text))
                Main.instance.GoldLimit = ushort.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Main.instance.ActualClass.criticalHits != uint.Parse(textBox2.Text))
                Main.instance.ActualClass.criticalHits = uint.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Main.instance.ActualClass.minHP != uint.Parse(textBox3.Text))
                Main.instance.ActualClass.minHP = uint.Parse(textBox3.Text);
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.PrintAnim != checkBox10.Checked)
                Main.instance.PrintAnim = checkBox10.Checked;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Main.instance.ActualClass.hidDelay != uint.Parse(textBox4.Text))
                Main.instance.ActualClass.hidDelay = uint.Parse(textBox4.Text);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (Main.instance.HitTrack != checkBox14.Checked)
                Main.instance.HitTrack = checkBox14.Checked;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (Main.instance.GWWidth != int.Parse(textBox5.Text))
                Main.instance.GWWidth = int.Parse(textBox5.Text);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (Main.instance.GWHeight != int.Parse(textBox7.Text))
                Main.instance.GWHeight = int.Parse(textBox7.Text);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UO.PrintInformation("Zamer Poison");
            Main.instance.Poisoning.PoisonBottle = new UOItem(UIManager.TargetObject());
            Main.instance.Poisoning.PoisonBottle.Click();
            UO.Wait(200);
            label10.Text = Main.instance.Poisoning.PoisonBottle.Name;
        }
    }
}
