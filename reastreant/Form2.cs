using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace reastreant
{
    public partial class Form2 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xhemo\source\repos\reastreant\database\DB_REST.mdf;Integrated Security=True;Connect Timeout=30");

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbx_dprt.Text != "اختار القسم")
            {
                connect.Open();
            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [Table] " +
           "(Id ,department,meal , price ) values (N'" + ID.Text + "' , N'" + cmbx_dprt.Text + "' , N'" + txtbx_meal.Text + "' ,'" + price.Text + "' )";
           
                cmd.ExecuteNonQuery();
                connect.Close();
                ID.Text = "";
                cmbx_dprt.Text = "اختار القسم";
                txtbx_meal.Text = "";
                price.Text = "";
                display_Data();
                MessageBox.Show("تم  ادخال البيانات ");
            }
            else
            {
                MessageBox.Show("ادخل القسم  ");
            }

               
          }

        public void display_Data()
        {
            connect.Open();
            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from [Table]";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter datadp = new SqlDataAdapter(cmd);
            datadp.Fill(dta);
            dataGridView1.DataSource = dta;
            connect.Close();


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmbx_dprt.Text = "اختار القسم";
           cmbx_dprt.Items.Add("مشروبات");
            cmbx_dprt.Items.Add("مأكولات");
            cmbx_dprt.Items.Add("حلويات");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from  [Table] where ID= '" + ID.Text + "' ";
            cmd.ExecuteNonQuery();
            connect.Close();
            ID.Text = "";
            cmbx_dprt.Text = "اختار القسم";
            txtbx_meal.Text = "";
            price.Text = "";
            display_Data();
            MessageBox.Show("تم مسح البيانات  ");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            display_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (cmbx_dprt.Text != "اختار القسم")
            {
                connect.Open();
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update [Table] set[department] = N'" + cmbx_dprt.Text + "' , meal = N'" + txtbx_meal.Text + "' ,price = '" + price.Text + "'   where ID= '" + ID.Text + "' ";              

                cmd.ExecuteNonQuery();
                connect.Close();
                ID.Text = "";
                cmbx_dprt.Text = "اختار القسم";
                txtbx_meal.Text = "";
                price.Text = "";
                display_Data();
                MessageBox.Show("تم  ادخال البيانات ");
            }
            else
            {
                MessageBox.Show("ادخل القسم  ");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
