using Oracle.ManagedDataAccess.Client;

namespace Grocery_List
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = Form1.GetConnection();
            connection.Open();
            try
            {
                cmd = new("INSERT INTO GROCERIES(Product, Price) " +
                          "VALUES ('" + this.textBox1.Text.ToString() + "', " + double.Parse(this.textBox2.Text.ToString()) + ")", connection);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + double.Parse(this.textBox2.Text.ToString()).ToString());
            }
            connection.Close();
            Close();
        }
    }
}
