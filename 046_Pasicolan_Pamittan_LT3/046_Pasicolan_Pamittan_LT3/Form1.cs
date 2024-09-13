using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace _046_Pasicolan_Pamittan_LT3
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeDataTable()
        {
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Department");
                grdData.DataSource = dt;
            }
        }

        private void excelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Open Excel";
            openExcel.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openExcel.Filter = "All files (*.*)|*.*|Excel File (*.xlsx)|*.xlsx";
            openExcel.FilterIndex = 2;
            openExcel.ShowDialog();

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + openExcel.FileName + "; Extended Properties='Excel 12.0 Xml;HDR=Yes'");
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cboDept.DataSource = dt;
            cboDept.DisplayMember = "dept";
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openText.Title = "Open Text";
            openText.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openText.Filter = "All files (*.*)|*.*|Text File (*.txt)|*.txt";
            openText.FilterIndex = 2;
            openText.ShowDialog();

            if (!String.IsNullOrWhiteSpace(openText.FileName))
            {
                string[] statement = File.ReadAllLines(openText.FileName);
                for (int i = 0; i < statement.Length; i++)
                {
                    cboDept.Items.Add(statement[i]);
                }
            }
        }

        private void btnAddtoList_Click(object sender, EventArgs e)
        {
            InitializeDataTable();

            if (!String.IsNullOrEmpty(txtFName.Text) && !String.IsNullOrEmpty(txtLName.Text) && !String.IsNullOrEmpty(cboDept.SelectedIndex.ToString()))
            {
                for (int i = 0; i < 1; i++)
                {
                    DataRow row = dt.NewRow();
                    row["First Name"] = txtFName.Text;
                    row["Last Name"] = txtLName.Text;
                    row["Department"] = cboDept.Text;
                    dt.Rows.Add(row);
                }

                txtFName.Text = string.Empty;
                txtLName.Text = string.Empty;
                cboDept.SelectedIndex = -1;
            }
        }

            private void Form1_Load(object sender, EventArgs e)
            {

            }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            txtFName.Clear();
            txtLName.Clear();
            cboDept.SelectedIndex = -1;
            grdData.DataSource = null;
        }

        private void btnSaveRec_Click(object sender, EventArgs e)
        {
            saveText.Title = "Save as file";
            saveText.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveText.DefaultExt = "txt";
            saveText.Filter = "All files (*.*)|*.*|Text File (*.txt)|*.txt";
            saveText.FilterIndex = 2;
            if (saveText.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = File.CreateText(saveText.FileName))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        writer.WriteLine($"{row["First Name"]}");
                        writer.WriteLine($"{row["Last Name"]}");
                        writer.WriteLine($"{row["Department"]}");
                    }
                }
                MessageBox.Show("Text file saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckRec_Click(object sender, EventArgs e)
        {
            openText.Title = "Open Text";
            openText.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openText.Filter = "All files (*.*)|*.*|Text File (*.txt)|*.txt";
            openText.FilterIndex = 2;
            openText.ShowDialog();

            if (!String.IsNullOrWhiteSpace(openText.FileName))
            {
                string[] information = File.ReadAllLines(openText.FileName);
                DataTable dt = new DataTable();
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Department");

                for (int i = 0; i < information.Length; i += 3)
                {
                    if (i + 2 < information.Length)
                    {
                        DataRow row = dt.NewRow();
                        row["First Name"] = information[i];
                        row["Last Name"] = information[i + 1];
                        row["Department"] = information[i + 2];
                        dt.Rows.Add(row);
                    }
                }

                grdData.DataSource = dt;
            }
        }
    }   
    }



