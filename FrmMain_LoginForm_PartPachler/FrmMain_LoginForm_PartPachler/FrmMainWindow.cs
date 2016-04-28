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

namespace FrmMain_LoginForm_PartPachler
{
    public partial class FrmMainWindow : Form
    {
        #region Membervariables and Constants
        private const string pathDB = @"..\..\cDB.csv"; //cDB...Customer Database
        public List<Customer> customers = new List<Customer>();
        
        #endregion

        public FrmMainWindow()
        {
            InitializeComponent();
            FrmLogIn loginForm = new FrmLogIn();
            DialogResult res = loginForm.ShowDialog();
            if(res == DialogResult.OK)
            {
                MessageBox.Show("Enabled!!");
            }
            else
            {
                this.Close(); //The main application is closed, when the user
                              //does not enter the right password but instead 
                              //presses cancel! A different action does not make
                              //sense.
            }

            //Testzwecke:
            this.customers = new List<Customer>();
            for (int i = 0; i < 10; i++)
            {
                this.customers.Add(new Customer(((char)(i + 'A')).ToString()));
            }
            this.WriteCustomerDB();
            //End Testzwecke!
        }

        private bool InitializeForm()
        {
            bool initOK = true;

            //Enter Code here!!

            return initOK;
        }

        private string EncryptDB()
        {
            string dbEncrypted = "";
            foreach( Customer cus in this.customers)
            {
                dbEncrypted += EncryptCustomer(cus) + "\n";
            }

            return dbEncrypted;
        }

        private static string EncryptCustomer (Customer cus)
        {
            //Should work!
            string customerEncrypted = "";
            foreach(char c in cus.ToString())
            {
                char newChar = (char)((c + 3 )%123);
                customerEncrypted += newChar;
                // A = 65, z = 122
            }
            return customerEncrypted;
        }

        //Konstruktor Customer aus STRING notwendig!!
        private static Customer DecryptCustomer(string input)
        {
            //Should work!!
            string customerDecrypted = "";
            foreach (char c in input)
            {
                customerDecrypted += (char)((c - 3 + 123) % (123));
            }
            return new Customer(input);
        }

        private void WriteCustomerDB()
        {
            //bool dbExists = File.Exists(pathDB);
            StreamWriter file = new StreamWriter(pathDB);
            file.Write(this.EncryptDB());
            file.Close();
        }
    }

    public class Customer
    {
        string name;
        public Customer()
        {
            this.name = "testi";
        }
        public Customer(string props)
        {
            this.name = props;
        }
        public override string ToString()
        {
 	        return this.name;
        }
    }
}
