﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BehComponents;

namespace Hesabdari_Darbast
{
    public partial class frmKarbar : Form
    {
        public frmKarbar()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source=EN2\\SQL2019;initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {

            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Tbl_Karbar";
            adp.Fill(ds, "Tbl_Karbar");
            dgvKarbar.DataSource = ds;
            dgvKarbar.DataMember = "Tbl_Karbar";
        }
            private void frmKarbar_Load(object sender, EventArgs e)
        {
            Display();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Tbl_Karbar (Username,Password)values(@a,@b)";
                cmd.Parameters.AddWithValue("@a", txtUserName.Text);
                cmd.Parameters.AddWithValue("@b", txtPassword.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBoxFarsi.Show("اطلاعات با موفقیت ثبت شد", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                txtUserName.Text = "";
                txtPassword.Text = "";

                Display();


            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("مشکلی رخ داده", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Warning, MessageBoxFarsiDefaultButton.Button1);

            }




        }

        private void dgvKarbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvKarbar[0, dgvKarbar.CurrentRow.Index].Value.ToString();
            txtUserName.Text = dgvKarbar[1, dgvKarbar.CurrentRow.Index].Value.ToString();
            txtPassword.Text = dgvKarbar[2, dgvKarbar.CurrentRow.Index].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvKarbar.SelectedCells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Delete from Tbl_Karbar Where IdKarbar=@N";
                cmd.Parameters.AddWithValue("@N", txtId.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBoxFarsi.Show("حذف با موفقیت انجام شد", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                Display();
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("مشکلی رخ داده", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);

            }



        }
    }
}
