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
    public partial class Form1 : Form
    {
        Szallashelylista szhl;
        public Form1()
        {
            InitializeComponent();
            szhl = new Szallashelylista();
        }

        public void LBFrissit()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = szhl;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SzallasFrm frm = new SzallasFrm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                szhl.Add(frm.Szh);
                LBFrissit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && MessageBox.Show("Biztosan törli a kölcsönzőt?", "Biztosan?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ABKezelo.SzallasTorles((Szallashely)listBox1.SelectedItem);
                    szhl.Remove((Szallashely)listBox1.SelectedItem);
                    LBFrissit();
                }
                catch (ABKivetel ex)
                {
                    MessageBox.Show(ex.Message, "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ABKezelo.KapcsolatBontasa();
            }
            catch 
            {
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                szhl.AddRange(ABKezelo.TeljesFelolvasas());
                LBFrissit();
            }
            catch (ABKivetel ex)
            {
                MessageBox.Show(ex.Message, "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
    }
}
