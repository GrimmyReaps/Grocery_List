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

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Setup
            OracleConnection connection = new OracleConnection();
            Form2 form2 = new Form2();
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet data = new DataSet();

            //Open connection
            try
            {
                connection = GetConnection();
                connection.Open();
                label1.Text = "Connected";
                label1.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                label1.Text = "Disconnected";
                label1.ForeColor = Color.DarkRed;
            }

            //Fill the DataAdapter with information stating if table GROCERIES exists
            cmd = new("select count(*) " +
            "from user_tables " +
            "where table_name='GROCERIES'", connection);
            adapter = new(cmd);
            adapter.Fill(data);
            form2.SetDataSource(data);
            //form2.Show();
            //MessageBox.Show(form2.dataGridView1.Rows[0].Cells[0].Value.ToString());

            //Check if the table indeed is in the Database
            if (!form2.dataGridView1.Rows[0].Cells[0].Value.ToString().Equals("0"))
            {
                //if table exists get info from it
                data = null;
                form2.SetDataSource(data);
                cmd = new("Select * FROM GROCERIES");
                if (data != null)
                {
                    adapter.Fill(data);
                    form2.SetDataSource(data);
                }
            }
            else
            {
                //If it doesn't create it

                //MessageBox.Show("Hello");
                try
                {  
                    cmd = new("CREATE TABLE GROCERIES (Product varchar(100), Price NUMBER(8,2))", connection);
                    cmd.ExecuteNonQuery();
                    form2.dataGridView1.DataSource = null;
                    form2.dataGridView1.Rows.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.Close();
            this.button2.Enabled = true;
        }

        public static OracleConnection GetConnection()
        {
            return new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                                                                   "(HOST=155.158.112.45)" +
                                                                   "(PORT=1521))" +
                                                                   " (CONNECT_DATA=(SERVICE_NAME=oltpstud)));" +
                                                                   "User Id=msbd13;" +
                                                                   "Password=haslo2022;");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleConnection connection = GetConnection();
            connection.Open();
            OracleCommand cmd = new OracleCommand("DROP TABLE GROCERIES", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}