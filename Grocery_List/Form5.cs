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
