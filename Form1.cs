using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Security_code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load_file();
        }
        //Declaration
        private string passs = "1234";
        private string path_dir = @"..\MSSV.txt";
        private int num_access_log = 0;
        //Initialize Function
        public void load_file()
        {
            if (File.Exists(path_dir))
            {
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(path_dir))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        listView1.Items.Add(s);
                    }
                }
            }
        }
        //Save to file function
        public void save_to_file()
        {
            if (File.Exists(path_dir))
            {
                using (StreamWriter sw = File.AppendText(path_dir))
                {
                    for (int i = listView1.Items.Count - num_access_log; i < listView1.Items.Count; i++)
                    {
                        sw.WriteLine(listView1.Items[i].Text);
                    }
                    //Close the file
                    sw.Close();
                }
            }
            else
            {
                try
                {
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter(path_dir);
                    foreach (ListViewItem item in listView1.Items)
                    {
                        sw.WriteLine(item.Text);
                    }
                    //Close the file
                    sw.Close();
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    //Console.WriteLine("Executing finally block.");
                }
            }

        }
        //Check security code & show log function
        public void showlog(string password)
        {
            DateTime dt = DateTime.Now;
            string login_result = "";
            if (password == passs)
            {
                login_result += dt.ToString() + " - Granted";
            }
            else
            {
                login_result += dt.ToString() + " - Restricted Access";
            }
            this.listView1.Items.Add(login_result);
        }
        //Form Event
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            save_to_file();
        }

        private void btn1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tbx0.TextLength < 4)
            {
                tbx0.Text += ((Button)sender).Text;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            //tbx0.Text = "";
            // www.it-swarm-vi.com/vi/c%23/cat-ky-tu-cuoi-cung-tu-mot-chuoi/969273032/
            if (tbx0.Text != "")
            {
                tbx0.Text = (tbx0.Text).Remove(tbx0.TextLength - 1);
            }    
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            showlog(tbx0.Text);
            tbx0.Text = "";
            num_access_log += 1;
        }

        private void tbx0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers 
            /*
             * if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                showlog(tbx0.Text);
                tbx0.Text = "";
            }
            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
             */
            // stackoverflow.com/questions/1191698/how-can-i-accept-the-backspace-key-in-the-keypress-event
            if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && !(char.IsControl(e.KeyChar)) && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                showlog(tbx0.Text);
                tbx0.Text = "";
            }   
        }
    }
}
