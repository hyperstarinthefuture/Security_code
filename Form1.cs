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
            this.ActiveControl = tbx0;
            //tbx0.Focus();
        }
        //Declaration
        private string passs = "1234";
        private string path_dir = @"..\102190143.txt";
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
                        listBox1.Items.Add(s);
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
                    for (int i = listBox1.Items.Count - num_access_log; i < listBox1.Items.Count; i++)
                    {
                        sw.WriteLine(listBox1.Items[i].ToString());
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
                    foreach (var item in listBox1.Items)
                    {
                        sw.WriteLine(item.ToString());
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
            if (password.Length == 1)
            {
                MessageBox.Show("Restricted Access!");
                login_result += dt.ToString() + " - Restricted Access";
            }  
            else if (password.Length == 0)
            {
                MessageBox.Show("Please input password before enter!");
            }    
            else if (password == passs)
            {
                login_result += dt.ToString() + " - Granted";
            }
            else
            {
                login_result += dt.ToString() + " - Access Denied";
            }
            this.listBox1.Items.Add(login_result);
            num_access_log += 1;
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
            if (!(char.IsDigit(e.KeyChar)) && !(char.IsControl(e.KeyChar)) && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                showlog(tbx0.Text);
                tbx0.Text = "";
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button11_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void tbx0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Shift || e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9 || e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
            {
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                showlog(tbx0.Text);
                tbx0.Text = "";
            }
        }
    }
}
