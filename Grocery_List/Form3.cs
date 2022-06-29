using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;

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
            //It's eaaier too see
            string Product = this.textBox1.Text.ToString();
            string Price = this.textBox2.Text.ToString();

            //Check if Price is correct format also if someone uses , change it to .
            if (Price.Length > 0 && Price.Length < 8 && RegexCheck(Price) == true)
            {
                Price = Price.Replace(',', '.');
                //MessageBox.Show(Price);
            }
            else
            {
                MessageBox.Show("Incorrect price value");
                return;
            }

            if (duplicateCheck(Product) == true)
            {
                MessageBox.Show("Name like that exists in database please choose a different name");
                return;
            }

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = Form1.GetConnection();
            connection.Open();
            try
            {
                cmd = new("INSERT INTO GROCERIES(Product, Price) " +
                          "VALUES ('" + Product + "', " + Price + ")", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + double.Parse(this.textBox2.Text.ToString()).ToString());
            }

            Price.Replace('.', ',');
            Price = Form1.correction(Price);
            string finalForm = Product + " " + Price + " PLN";

            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            form1.addItem(finalForm);

            connection.Close();
            Close();
        }
        private bool RegexCheck(string check)
        {
            string StrRegex = @"^(?:[0-9]{1,4}[.,]{1}[0-9]{1,2}|[0-9]{1,4})$";
            Regex rgx = new Regex(StrRegex);

            if (rgx.IsMatch(check))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void refreshWindow()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox1.Focus();
        }

        private bool duplicateCheck(string question)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = Form1.GetConnection();
            connection.Open();

            try
            {
                cmd = new("SELECT Product FROM GROCERIES", connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString(0).Equals(question))
                {
                    connection.Close();
                    return true;
                }
            }

            connection.Close();
            return false;
        }
    }
}
