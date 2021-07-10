using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Class;

namespace WindowsFormsApp1
{

    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        backEndHouseKeeping BE = new backEndHouseKeeping();
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = BE.Select();
            DataTable.DataSource = dt;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create Format Time 
            var time = DateTime.Now;
            string formattedTime = time.ToString("yyyyMMdd_hhmmss");
            string finish = "finish";
            string nameDb = "Activo_" + formattedTime;
            //Data Value
            BE.ParameterYear =paramTxtBox.Text;
            BE.Message = finish;
            BE.Checked = false;
            bool insert = BE.Insert(BE);

            if(checkBox1.CheckState == CheckState.Checked)
            {
                BE.Checked = true;
            }
            else
            {
                BE.Checked = false;
            }

            bool backup = BE.Backup(BE);

            if (insert == true )
            {
                MessageBox.Show("success");
                Console.WriteLine("success");
                DataTable dt = BE.Select();
                DataTable.DataSource = dt;
            }
            else
            {
                MessageBox.Show("fail");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


     
