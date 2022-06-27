using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Grocery_List
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                OracleConnection connection = GetConnection();
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection connection = GetConnection();
            Form2 form2 = new Form2();
            OracleCommand cmd = new OracleCommand("select count(*) " +
                                                  "from user_tables " +
                                                  "where table_name='GROCERIES'", connection);
            //OracleCommand cmd = new OracleCommand("SELECT * FROM EMPLOYEES", connection);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet data = new DataSet();
            adapter.Fill(data);
            form2.SetDataSource(data);
            form2.Show();
            if (cmd.RowSize == 0)
            {
                cmd = new("CREATE TABLE GROCERIES (Product varchar(100), Zloty int, Groszy int)");
            }
            else
            {
                cmd = new("Select * FROM GROCERIES");
                adapter.Fill(data);
                form2.SetDataSource(data);
            }
        }

        private static OracleConnection GetConnection()
        {
            return new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                                                                   "(HOST=155.158.112.45)" +
                                                                   "(PORT=1521))" +
                                                                   " (CONNECT_DATA=(SERVICE_NAME=oltpstud)));" +
                                                                   "User Id=msbd13;" +
                                                                   "Password=haslo2022;");
        }
    }
}