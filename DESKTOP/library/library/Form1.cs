using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace library
{
    public partial class Form1 : Form
    {
        public int customerId { get; private set; }
        public string title { get; private set; }
        public Form1()
        {
            InitializeComponent();
            FillListBoxFromCSV();
        }
        private void FillListBoxFromCSV()
        {
            try
            {
                string filePath = "Kolcsonzok.csv";

                if (File.Exists(filePath))
                {
                    List<string> names = new List<string>();

                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        sr.ReadLine();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                           
                            string[] parts = line.Split(';');

                           
                            if (parts.Length > 0)
                            {
                                names.Add(parts[0]);
                            }
                        }
                    }

                    
                    listBox1.Items.AddRange(names.ToArray());
                }
                else
                {
                    MessageBox.Show("A fájl nem található!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a fájl olvasása közben: " + ex.Message);
            }
        }
        

        

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listBox1.SelectedItem != null)
            {

                string selectedName = listBox1.SelectedItem.ToString();

                int id = GetCustomerId(selectedName);
                customerId = GetCustomerId(selectedName); 

                List<string> books = GetBooksById(id);


                listBox2.Items.Clear();
                listBox2.Items.AddRange(books.ToArray());
            }

            int GetCustomerId(string name)

            {
                Console.WriteLine(name + ' ');
                int id = 0;


                string connectionString = "server=localhost;user=root;database=kolcsonzesek;port=3306;password=;";


                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT id FROM kolcsonzok WHERE nev = @name;";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            id = reader.GetInt32(0); 
                            Console.WriteLine(id);
                        }
                    }
                }
                return id;
            }

            List<string> GetBooksById(int id)
            {
                List<string> books = new List<string>();

                string connectionString = "server=localhost;user=root;database=kolcsonzesek;port=3306;password=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT iro, cim FROM kolcsonzesek WHERE kolcsonzokId = @id;";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string author = reader.GetString(0);
                                string title = reader.GetString(1); 
                                books.Add(author + " - " + title); 
                            }
                        }
                    }
                }

                return books;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (listBox2.SelectedItem != null)
            {
                
                button2.Enabled = true;
            }
            else
            {
                
                button2.Enabled = false;
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
           
            if (listBox2.SelectedItem != null)
            {
              
                string selectedBook = listBox2.SelectedItem.ToString();

                
                
                DeleteBook(customerId, selectedBook);

              
                listBox2.Items.Remove(selectedBook);
            }
        }
        private void DeleteBook(int customerId, string title)
        {
            string connectionString = "server=localhost;user=root;database=kolcsonzesek;port=3306;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM kolcsonzesek WHERE kolcsonzokId = @customerId AND cim = @bookName;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@customerId", customerId);
                    command.Parameters.AddWithValue("@bookName", title);
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
