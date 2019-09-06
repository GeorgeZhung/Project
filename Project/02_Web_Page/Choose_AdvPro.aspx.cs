using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Project._02_Web_Page {
    public partial class Choose_AdvPro : System.Web.UI.Page {

		//�s����Ʈw�A�R�W��cnStr
		//@"��ƨӷ�=(���a��Ʈw)\"
        String cnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                    "AttachDBFilename=|DataDirectory|Project_Db.mdf";

        protected void Page_Load (object sender, EventArgs e) {

        }

        //���m�Ҧ����ɱб�
        protected void Clean (object sender, System.EventArgs e) 
        {
            int Row, Col;

            for (Row = 1; Row <= 6; Row++) {
                for (Col = 1; Col <= 3; Col++) {
                    this.Select_AP_Table.Rows [Row].Cells [Col].Text = null;
                }
            }
        }

        protected void Update (object sender, System.EventArgs e) {
            using (SqlConnection cn = new SqlConnection ()) {
                cn.ConnectionString = cnStr;
                cn.Open ();
                SqlCommand cmd = new SqlCommand ("", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add (new SqlParameter("@", SqlDbType.));
            }
        }

        protected void Adv_Changed (object sender, EventArgs e) {
            DropDownList ddl = (DropDownList) sender;
            string DDL_Id = ddl.ID;
            string DDL_Value = ddl.SelectedValue;
            int DDL_Index = ddl.SelectedIndex;

            Table Tb;
            TextBox TB;

            //�N�U�U�Ԧ����P�U�۪��ۭq���ɱб��������p
            if (DDL_Id == "Select_Adv_1") {
                Tb = this.Table_Costom_1;
                TB = this.Adv_PointRemain_1;
            }
            else if (DDL_Id == "Select_Adv_2") {
                Tb = this.Table_Costom_2;
                TB = this.Adv_PointRemain_2;
            }
            else {
                Tb = this.Table_Costom_3;
                TB = this.Adv_PointRemain_3;
            }

            //�̷ӤU�Ԧ���檺���e�A�M�w�ۭq���ɱб���쪺��ܻP�_
            if (DDL_Value == "Custom") {
                Tb.Visible = true;
            }
            else {
                Tb.Visible = false;
            }

            //�ھڤU�Ԧ���椺�e�A�۸�Ʈw���X�۹������u�Ѿl�I�ơv
            if (DDL_Index > 1) {
                int R = DDL_Index - 2;

                using (SqlConnection cn = new SqlConnection ()) {
                    cn.ConnectionString = cnStr;
                    SqlDataAdapter AP_Name = new SqlDataAdapter
                        ("SELECT * FROM Adv_Pro ", cn);
                    DataSet ds = new DataSet ();
                    AP_Name.Fill (ds, "Adv_Pro");

                    DataTable dt = ds.Tables ["Adv_Pro"];

                    TB.Text = dt.Rows [R] [7].ToString ();
                }
            }
        }

        protected void Choose (object sender, EventArgs e) {
            Button BT = (Button) sender;
            string BT_Id = BT.ID;

            DropDownList ddl;

            Table Tb = this.Select_AP_Table;
            int Col;


            if (BT_Id == "Choose_Adv_1" || BT_Id == "Choose_Adv_2" || BT_Id == "Choose_Adv_3") {
                if (BT_Id == "Choose_Adv_1") {
                    ddl = this.Select_Adv_1;
                    Col = 1;
                }

                else if (BT_Id == "Choose_Adv_2") {
                    ddl = this.Select_Adv_2;
                    Col = 2;
                }

                else {
                    ddl = this.Select_Adv_3;
                    Col = 3;
                }

                int DDL_Index = ddl.SelectedIndex;
                int DDL_Sum = ddl.Items.Count;

                if (DDL_Index > 1) {
                    int R = DDL_Index - 2;

                    using (SqlConnection cn = new SqlConnection ()) {
                        cn.ConnectionString = cnStr;
                        SqlDataAdapter AP_Name = new SqlDataAdapter
                            ("SELECT * FROM Adv_Pro ", cn);
                        DataSet ds = new DataSet ();
                        AP_Name.Fill (ds, "Adv_Pro");

                        DataTable dt = ds.Tables ["Adv_Pro"];

                        int count;
                        for (count = 0; count <= 5; count++) {
                           Tb.Rows [count+1].Cells [Col].Text = dt.Rows [R] [count].ToString ();
                        }
                    }
                }
            }

            //�I�諸���䬰�u�ۭq���ɱб¡v
            else {

            }
        }
    }
}