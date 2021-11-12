using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace reastreant
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xhemo\source\repos\reastreant\database\DB_REST.mdf;Integrated Security=True;Connect Timeout=30");

       
        

        public double sum = 0;
        public double sum_qunt =1; 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cmbx_dprt.Items.Add("مشروبات");
            cmbx_dprt.Items.Add("مأكولات");
            cmbx_dprt.Items.Add("حلويات");
            cmbx_dprt.SelectedIndex = 0;
            dvg_Get.ColumnCount = 4;
            dvg_Get.Columns[0].Name = " الصنف";
            dvg_Get.Columns[1].Name = " السعر  ";
            dvg_Get.Columns[2].Name = " الكمية ";
            dvg_Get.Columns[3].Name = " اجمالي ";





        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtbx_price.Text == "0") return;
           
           sum_qunt = Convert.ToDouble(txtbx_price.Text) * Convert.ToDouble(textqunt.Text);
            sum += sum_qunt;
            txtbx_sum.Text = sum.ToString();

           

            object[] row = { cmbx_meal.Text, txtbx_price.Text, textqunt.Text, sum_qunt.ToString() };

            dvg_Get.Rows.Add(row);
         

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void cmbx_meal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("SELECT  price  FROM [Table]  where meal = N'" + cmbx_meal.Text +"' ");
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connect;
            connect.Open();
            SqlDataReader sdr = cmd1.ExecuteReader();
            while (sdr.Read())
            {
                
                txtbx_price.Text = sdr["price"].ToString();

            }
           
            connect.Close();

        }

        private void cmbx_dprt_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbx_meal.Items.Clear();           
            
            SqlCommand cmd1 = new SqlCommand("SELECT  department , meal   FROM [Table] ");

            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connect;
            connect.Open();
            SqlDataReader sdr = cmd1.ExecuteReader();

            while (sdr.Read())
            {
                if (sdr["department"].ToString() == cmbx_dprt.Text)
                    cmbx_meal.Items.Add(sdr["meal"].ToString());
              
            }
            
              
            connect.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            

            foreach (DataGridViewRow item in this.dvg_Get.SelectedRows)
            {
               dvg_Get.Rows.RemoveAt(item.Index);
            }

            txtbx_sum.Text = "";


        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          
            
            int num = RandomNumber(10000, 1000000);  /// Random  Number
            float margin = 40; //  paper margin 

            /////////////////////////////////// head titles  //////////////////////////////////////////////
            string strNo = "#NO " + num.ToString();
            string strDate = "التاريخ : " + DateTime.Now.ToShortDateString();
            string strdepart = "القسم :  السفري ";
            string strtitle = " الطلب في الفاتورة ";

            ////////////////////////////////////////  Fonts style  ////////////////////////////////////////////
          
            Font f = new Font("Arial" , 18 , FontStyle.Bold);

            //////////////////////////////////////// fonts Size //////////////////////////////////////////////
            
            SizeF FontSizeNO = e.Graphics.MeasureString(strNo, f);
            SizeF FontSizeDate = e.Graphics.MeasureString(strDate, f);
            SizeF FontSizedepart = e.Graphics.MeasureString(strdepart, f);
            SizeF FontSizetitle = e.Graphics.MeasureString(strtitle, f);

            ////////////////////////////////////////// Draw head titles /////////////////////////////////////
            e.Graphics.DrawString(strNo, f , Brushes.Red , (e.PageBounds.Width - FontSizeNO.Width)/2 , margin);
            e.Graphics.DrawString(strDate, f, Brushes.Black,  margin , margin + FontSizeNO.Height);
            e.Graphics.DrawString(strdepart, f, Brushes.Black, (e.PageBounds.Width - FontSizedepart.Width - margin), margin  + FontSizeNO.Height);
            e.Graphics.DrawString(strtitle, f, Brushes.Black, (e.PageBounds.Width - FontSizeNO.Width ) / 2 , margin + FontSizedepart.Height + FontSizeNO.Height + FontSizedepart.Height);
         
            
            float preHeights = margin + FontSizedepart.Height + FontSizeNO.Height + FontSizedepart.Height + FontSizetitle.Height +5;

            //////////////////////////////////////////// Draw Rectangle ////////////////////////////////////////////////////////
            
            e.Graphics.DrawRectangle(Pens.Black , margin , preHeights  ,e.PageBounds.Width - margin * 2 , e.PageBounds.Height - margin  - preHeights );

            ///////////////////////////////////////// Dimensions of columns //////////////////////////////////////////////////// 
            
            float colHeight = 50; // Hight
            float col1_width =50; // Column_1  width
            float col2_width =200 + col1_width; // Column_2  width
            float col3_width = 100 + col2_width; // Column_3  width
            float col4_width = 100 + col3_width; // Column_4  width
            float col5_width = 100 + col4_width; // Column_5  width
            ////////////////////////////////////////////  Head Row Line  ///////////////////////////////////////////////////////
            e.Graphics.DrawLine(Pens.Black, margin , preHeights + colHeight, e.PageBounds.Width - margin, preHeights + colHeight );

            /////////////////////////////////////////////////////columns /////////////////////////////////////////////////////////////
            e.Graphics.DrawString("الرقم ", f, Brushes.Black, e.PageBounds.Width - margin  - col1_width, preHeights + 20);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - (margin * 2) - col1_width, preHeights, e.PageBounds.Width - (margin * 2) - col1_width, e.PageBounds.Height - margin);

            e.Graphics.DrawString("الصنف ", f, Brushes.Black, e.PageBounds.Width - margin  - col2_width, preHeights + 20);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - (margin * 2) - col2_width, preHeights, e.PageBounds.Width - (margin * 2) - col2_width, e.PageBounds.Height - margin);


            e.Graphics.DrawString("السعر ", f, Brushes.Black, e.PageBounds.Width - margin * 2  - col3_width, preHeights + 20);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - (margin * 2) - col3_width, preHeights, e.PageBounds.Width - (margin * 2) - col3_width, e.PageBounds.Height - margin);

            e.Graphics.DrawString("الكمية ", f, Brushes.Black, e.PageBounds.Width - margin * 2 - col4_width, preHeights + 20);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - (margin * 2) - col4_width, preHeights, e.PageBounds.Width - (margin * 2) - col4_width, e.PageBounds.Height - margin);

            e.Graphics.DrawString("اجمالي ", f, Brushes.Black, e.PageBounds.Width - margin * 4 - col5_width, preHeights + 20);

            ///////////////////////////////////////// Dimensions of ROWS //////////////////////////////////////////////////// 

            float RowHight = 60;

            /////////////////////////////////////////////////////  ROWs ///////////////////////////////////////////////////////////// 
           
            for (int i = 0; i < dvg_Get.Rows.Count-1 ; i++)
            {
                e.Graphics.DrawString((i+1).ToString(), f, Brushes.Navy, e.PageBounds.Width - (margin * 2) - col1_width, preHeights + RowHight);

              e.Graphics.DrawString(dvg_Get.Rows[i].Cells[0].Value.ToString(), f, Brushes.Navy, e.PageBounds.Width - (margin * 2) - col2_width, preHeights + RowHight);
               e.Graphics.DrawString(dvg_Get.Rows[i].Cells[1].Value.ToString(), f, Brushes.Navy, e.PageBounds.Width - (margin * 2) - col3_width, preHeights + RowHight);
               e.Graphics.DrawString(dvg_Get.Rows[i].Cells[2].Value.ToString(), f, Brushes.Navy, e.PageBounds.Width - (margin * 2) - col4_width, preHeights + RowHight);
                e.Graphics.DrawString(dvg_Get.Rows[i].Cells[3].Value.ToString(), f, Brushes.Navy, e.PageBounds.Width - (margin * 2) - col5_width, preHeights + RowHight);

                e.Graphics.DrawLine(Pens.Black, margin , preHeights + RowHight + colHeight, e.PageBounds.Width - margin , preHeights + RowHight + colHeight);
                RowHight += 50;
            }

            e.Graphics.DrawString(" الاجمالي ", f, Brushes.Red, e.PageBounds.Width - (margin * 2) - col4_width, preHeights + RowHight);
            e.Graphics.DrawString(sum.ToString(), f, Brushes.Red, e.PageBounds.Width - (margin * 2) - col5_width, preHeights + RowHight);
            e.Graphics.DrawLine(Pens.Black, margin, preHeights + RowHight + colHeight, e.PageBounds.Width - margin, preHeights + RowHight + colHeight);

        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
    
}
