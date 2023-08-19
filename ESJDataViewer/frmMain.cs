using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using static System.Net.Mime.MediaTypeNames;

namespace ESJDataViewer
{
    public partial class frmMain : Form
    {
        List<VOListing> Sourcerecords;
        List<VOListing> filteredlist;
        public frmMain()
        {
            InitializeComponent();
            Getdata();
        }

        private DataTable Getdata()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("VO NAME");
            dt.Columns.Add("OwnerNumber");
            dt.Columns.Add("UnitNumber");
            dt.Columns.Add("IntervalNumber");
            dt.Columns.Add("Address1");
            dt.Columns.Add("Address2");
            dt.Columns.Add("City");
            dt.Columns.Add("State");
            dt.Columns.Add("Zip");
            dt.Columns.Add("Country");
            dt.Columns.Add("Phone1");
            dt.Columns.Add("Phone2");
            dt.Columns.Add("Email");
            dt.Columns.Add("CUorVO");
            dt.Columns.Add("EF or 1B");            
            dt.Columns.Add("BalanceOwing");


            var reader = new StreamReader("VO LISTING INCLUDING BALANCE OWED_not including 2023_2024 fees.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<VOListing>();

            Sourcerecords = records.ToList();

            dataGridView1.DataSource = Sourcerecords;
            int value = 0;
            List<string> Interval = (from val in Sourcerecords
                                     orderby Int32.Parse(val.IntervalNumber.Trim() == ""?"0":val.IntervalNumber.Trim())
                                     //orderby val.IntervalNumber 
                                     select val.IntervalNumber).Distinct().ToList();
            cbInterval.DataSource = Interval;

            foreach (var r in records)
            { 
            
            }

            return dt;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            
            if (txt.Name == txtName.Name)
            {
                if (txt.Text == "")
                {
                    dataGridView1.DataSource = Sourcerecords;
                }
                else
                {
                    //List<VOListing> filteredlist;

                    //if (txtInterval.Text != "" || txtOwnerNumber.Text != "")
                    //{filteredlist = filteredlist.FindAll(x => x.VO_NAME.ToUpper().Contains(txt.Text.ToString().ToUpper())).OrderBy(z => z.VO_NAME).ToList(); }
                    //else
                    //{filteredlist = Sourcerecords.FindAll(x => x.VO_NAME.ToUpper().Contains(txt.Text.ToString().ToUpper())).OrderBy(z => z.VO_NAME).ToList(); }

                    List<VOListing> filteredlist = (from val in Sourcerecords
                                                    where val.VO_NAME.ToUpper().Contains((txt.Text == "" ? val.VO_NAME.ToUpper() : txt.Text.ToUpper()))
                                                    && val.IntervalNumber.ToUpper().Contains(txtInterval.Text == "" ? val.IntervalNumber.ToUpper() : txtInterval.Text.ToUpper())
                                                    && val.OwnerNumber.ToUpper().Contains(txtOwnerNumber.Text == "" ? val.OwnerNumber.ToUpper() : txtOwnerNumber.Text.ToUpper())
                                                    select val).ToList();


                    dataGridView1.DataSource = filteredlist;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Name == txtOwnerNumber.Name)
            {
                if (txt.Text == "")
                {
                    dataGridView1.DataSource = Sourcerecords;
                }
                else
                {
                    //List<VOListing> filteredlist;
                    if (txtInterval.Text != "" || txtName.Text != "")
                    {filteredlist = filteredlist.FindAll(x => x.OwnerNumber.ToUpper().Equals(txt.Text.ToString().ToUpper())).OrderByDescending(z => z.OwnerNumber).ToList(); }
                    else
                    {filteredlist = Sourcerecords.FindAll(x => x.OwnerNumber.ToUpper().Equals(txt.Text.ToString().ToUpper())).OrderByDescending(z => z.OwnerNumber).ToList(); }
                        
                    dataGridView1.DataSource = filteredlist;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Name == txtInterval.Name)
            {
                if (txt.Text == "")
                {
                    dataGridView1.DataSource = Sourcerecords;
                }
                else
                {
                    //List<VOListing> filteredlist;
                    if (txtOwnerNumber.Text != "" || txtName.Text != "")
                    {filteredlist = filteredlist.FindAll(x => x.IntervalNumber.ToUpper().Equals(txt.Text.ToString().ToUpper())).OrderBy(z => z.IntervalNumber).ToList();}
                    else
                    {filteredlist = Sourcerecords.FindAll(x => x.IntervalNumber.ToUpper().Equals(txt.Text.ToString().ToUpper())).OrderBy(z => z.IntervalNumber).ToList(); }                        
                    dataGridView1.DataSource = filteredlist;
                }
            }
        }


        private void textbox_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;


            //if (txt.Name == txtName.Name)
            //{
                //if (txt.Text == "")
                //{
                //    dataGridView1.DataSource = Sourcerecords;
                //}
                //else
                //{
                    //List<VOListing> filteredlist;

                    //if (txtInterval.Text != "" || txtOwnerNumber.Text != "")
                    //{filteredlist = filteredlist.FindAll(x => x.VO_NAME.ToUpper().Contains(txt.Text.ToString().ToUpper())).OrderBy(z => z.VO_NAME).ToList(); }
                    //else
                    //{filteredlist = Sourcerecords.FindAll(x => x.VO_NAME.ToUpper().Contains(txt.Text.ToString().ToUpper())).OrderBy(z => z.VO_NAME).ToList(); }

                    List<VOListing> filteredlist = (from val in Sourcerecords
                                                    where val.VO_NAME.ToUpper().Contains((txtName.Text == "" ? val.VO_NAME.ToUpper() : txtName.Text.ToUpper()))
                                                    && val.IntervalNumber.ToUpper().Equals(txtInterval.Text == "" ? val.IntervalNumber.ToUpper() : txtInterval.Text.ToUpper())
                                                    && val.OwnerNumber.ToUpper().Contains(txtOwnerNumber.Text == "" ? val.OwnerNumber.ToUpper() : txtOwnerNumber.Text.ToUpper())
                                                    select val).ToList();


                    dataGridView1.DataSource = filteredlist;
                }

        private void txtButton_Click(object sender, EventArgs e)
        {
            txtInterval.Text = "";
            txtName.Text = "";
            txtOwnerNumber.Text = "";
            cbInterval.SelectedIndex = 0;
        }

        private void cbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInterval.Text = cbInterval.SelectedValue.ToString();
        }
        //}
        //}

    }
    class VOListing
    {

        public string VO_NAME {get;set;}
        public string OwnerNumber { get; set; }
        public string UnitNumber { get; set; }
        public string IntervalNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string CUorVO { get; set; }
        public string EF_or_1B {get;set;}
        public string BalanceOwing { get; set; }
}
}
