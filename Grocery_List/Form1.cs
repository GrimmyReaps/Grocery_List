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
            try
            {
                //Fill the DataAdapter with information stating if table GROCERIES exists
                cmd = new("select count(*) " +
                "from user_tables " +
                "where table_name='GROCERIES'", connection);
                adapter = new(cmd);
                adapter.Fill(data);
                form2.SetDataSource(data);
                //form2.Show();
                //MessageBox.Show(form2.dataGridView1.Rows[0].Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Check if the table indeed is in the Database
            if (!form2.dataGridView1.Rows[0].Cells[0].Value.ToString().Equals("0"))
            {
                try
                {
                    //if table exists get info from it
                    cmd = new("Select Product, Price FROM GROCERIES", connection);
                    OracleDataReader reader = cmd.ExecuteReader();
                    string Price;
                    string readData;
                    while (reader.Read())
                    {
                        Price = reader.GetString(1).Replace('.', ',');
                        Price = correction(Price);
                        readData = reader.GetString(0) + " " + Price + " PLN";
                        addItem(readData);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.Close();
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            form3.refreshWindow();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleConnection connection = GetConnection();
            connection.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("DROP TABLE GROCERIES", connection);
                cmd.ExecuteNonQuery();
            }
            catch(OracleException OrEx)
            {
                MessageBox.Show("Database is already deleted");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int idx = checkedListBox1.SelectedIndex;
            string name = checkedListBox1.Items[idx].ToString();
            name = name.Substring(0, name.IndexOf(' '));
            //MessageBox.Show(name);
            Form4 form4 = new Form4();
            form4.fillPrompt(name);
            form4.GetHelper(name);

            DialogResult result = form4.ShowDialog();
            if (result == DialogResult.Yes)
            {
                this.checkedListBox1.Items.RemoveAt(idx);
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OracleConnection connection = GetConnection();
            connection.Open();
            try
            {
                OracleCommand cmd = new OracleCommand("Commit", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        public void addItem(string newItem)
        {
            this.checkedListBox1.Items.Add(newItem);
        }

        public static string correction(string toCorrect)
        {
            string Placeholder;
            if (toCorrect.Contains(','))
            {
                Placeholder = toCorrect.Substring(toCorrect.IndexOf(','));
                //MessageBox.Show(Placeholder);
                if (Placeholder.Length < 3)
                {
                    toCorrect = toCorrect + "0";
                }
            }
            else
            {
                toCorrect = toCorrect + ",00";
            }

            return toCorrect;
        }

    }
}