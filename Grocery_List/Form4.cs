using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grocery_List
{
    public partial class Form4 : Form
    {
        string helper;
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection connection = Form1.GetConnection();
            connection.Open();
            OracleCommand cmd = new OracleCommand();

            try{
                cmd = new("DELETE FROM GROCERIES WHERE Product='" + helper + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
            Close();
        }
        
        public void GetHelper(string helper)
        {
            this.helper = helper;
        }

        public void fillPrompt(string fill)
        {
            this.label1.Text = "Do you wish to delete " + fill + " from database?";
        }

    }
}
