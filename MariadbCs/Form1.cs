using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MariadbCs
{
    public partial class Form1 : Form
    {
        Mariadb mariadb;
        public Form1()
        {
            InitializeComponent();

            mariadb = Mariadb.Instance;
            mariadb.server = "125.132.149.3";
            mariadb.port = "33060";
            mariadb.user = "vcanus";
            mariadb.database = "testdb";
            mariadb.password = "1234";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            string quary = txt_select.Text;
            DataTable dt = mariadb.SelectDB(quary);
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
                lbl_result.Text = "select complete";
            }
            

            //dataGridView1 수동입력금지
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            string quary = txt_insert.Text;
            if (mariadb.CommandDB(quary))
            {
                lbl_result.Text = "insert complete";
            }
        }



        /// <ibatis>
        /// //////////////////
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    DBManager mg = new DBManager();
        //    List<testBean> list = mg.SelectTest(txt_where.Text);
        //    dataGridView1.DataSource = list;
        //}
    }
}
