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
    public partial class Form5 : Form
    {
        string Price;
        string Product;
        public Form5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            //It's easier too see
            string NewProduct = this.textBox1.Text.ToString();
            string NewPrice = this.textBox2.Text.ToString();

            //Check if Price is correct format also if someone uses , change it to .
            if (NewPrice.Length > 0 && NewPrice.Length < 8 && form3.RegexPriceCheck(NewPrice) == true)
            {
                NewPrice = NewPrice.Replace(',', '.');
                //MessageBox.Show(Price);
            }
            else
            {
                MessageBox.Show("Incorrect price value");
                return;
            }

            if (form3.RegexProductCheck(NewProduct) == false)
            {
                MessageBox.Show("Product can't include spaces");
                return;
            }

            if (Product.Equals(NewProduct))
            {
                //Nothing to see here moving on
            }
            else if (form3.duplicateCheck(NewProduct) == true)
            {
                MessageBox.Show("Please choose another name");
                return;
            }

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = Form1.GetConnection();
            connection.Open();
            try
            {
                cmd = new("UPDATE GROCERIES" +
                          "SET Product='" + NewProduct + "', Price=" + NewPrice +
                          "WHERE Price='" + Price + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message /*+ double.Parse(this.textBox2.Text.ToString()).ToString()*/);
            }

            NewPrice = NewPrice.Replace('.', ',');
            NewPrice = Form1.correction(NewPrice);
            string finalForm = NewProduct + " " + NewPrice + " PLN";

            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            form1.addItem(finalForm);

            connection.Close();
            Close();
        }

        private void GetProductPrice(string Product, string Price)
        {
            this.Product = Product;
            this.Price = Price;
        }

        public void SetText(string Product, string Price)
        {
            GetProductPrice(Product, Price);
            this.label1.Text += Product;
            this.label2.Text += Price;
        }
    }
}
