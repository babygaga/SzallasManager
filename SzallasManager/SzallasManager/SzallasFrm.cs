using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SzallasManager
{
    public partial class SzallasFrm : Form
    {
        Szallashely szh;

        internal Szallashely Szh { get => szh; set => szh = value;}
        public SzallasFrm()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Szallasfajta));
            comboBox2.Items.AddRange(
                new object[]
                {
                    "camping",
                    "Szálloda",
                    "Panzió"
                }
                );
            comboBox2.SelectedIndex = 0;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        szh = new Camping(textBox1.Text, new Cim((short)numericUpDown1.Value, textBox2.Text, textBox3.Text, (short)numericUpDown2.Value), (Szallasfajta)comboBox1.SelectedItem, checkBox1.Checked);
                        break;
                    case 1:
                        szh = new Szalloda(textBox1.Text, new Cim((short)numericUpDown1.Value, textBox2.Text, textBox3.Text, (short)numericUpDown2.Value), (Szallasfajta)comboBox1.SelectedItem, (byte)numericUpDown3.Value, (int)numericUpDown4.Value, checkBox2.Checked);
                        break;
                    case 2:
                        szh = new Panzio(textBox1.Text, new Cim((short)numericUpDown1.Value, textBox2.Text, textBox3.Text, (short)numericUpDown2.Value), (Szallasfajta)comboBox1.SelectedItem, (byte)numericUpDown3.Value, (int)numericUpDown4.Value, checkBox3.Checked);
                        break;
                }
                ABKezelo.UjSzallashely(szh);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = false;
                    groupBox5.Enabled = false;
                    groupBox6.Enabled = false;
                    break;
                case 1:
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = true;
                    groupBox5.Enabled = true;
                    groupBox6.Enabled = false;
                    break;
                case 2:
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = true;
                    groupBox5.Enabled = false;
                    groupBox6.Enabled = true;
                    break;
            }
           
        }
    }
}
