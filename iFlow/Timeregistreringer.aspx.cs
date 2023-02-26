using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iFlow
{
    public partial class Timeregistreringer : System.Web.UI.Page
    {
        Employee employeeInfo;
        static string sqlConnectionString = ConfigurationManager.ConnectionStrings["iFlowConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            if(phoneNumber.Value.Length >= 8)
            {
                employeeInfo = new Employee(phoneNumber.Value);
                GetTimeRegistrations(employeeInfo.EmployeeID);
            }
        }

        void GetTimeRegistrations(int employeeID)
        {
            gvTimeRegistrations.DataSource = null;
            gvTimeRegistrations.DataBind();
            
            if (employeeInfo.EmployeeID > 0)
            {
                SqlConnection con = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.CommandText = "SELECT TimeregistrationID, format(DateStamp, 'dd-MM-yyyy') as DateStamp, Hours, Notes, TimeRegistered FROM TimeRegistrations WHERE EmployeeID=@EmployeeID";
                cmd.Connection = con;
                var da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Open();
                cmd.ExecuteNonQuery();
                gvTimeRegistrations.DataSource = ds;
                gvTimeRegistrations.DataBind();
                con.Close();

                if (employeeInfo.TotalWorkHours > 100)
                    Response.Write("<script language=javascript>alert('Du har jobbet totalt " + employeeInfo.TotalWorkHours.ToString() + " arbeidstimer!');</script>");
                btnDeleteTime.Visible = true;
            }
            else
            {
                Response.Write("<script language=javascript>alert('Det finnes ingen ansatt med dette telefonnummeret');</script>");
            }
        }
        protected void ChcDeleteTimeAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chcHeader = (CheckBox)gvTimeRegistrations.HeaderRow.FindControl("chcDeleteTimeAll");
            foreach (GridViewRow row in gvTimeRegistrations.Rows)
            {
                CheckBox chcRow = (CheckBox)row.FindControl("chcDeleteTime");
                if (chcHeader.Checked == true)
                {
                    chcRow.Checked = true;

                }
                else
                {
                    chcRow.Checked = false;
                }
            }
        }
        protected void btnDeleteTime_Click(object sender, EventArgs e)
        {
            int rowsDeleted = 0;
            foreach (GridViewRow row in gvTimeRegistrations.Rows)
            {
                CheckBox chc = (CheckBox)row.Cells[3].FindControl("chcDeleteTime");

                if (chc.Checked == true)
                {
                    string sqlQuery = "DELETE FROM TimeRegistrations WHERE TimeRegistrationID=@TimeRegistrationID";

                    using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                    using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@TimeRegistrationID", row.Cells[0].Text);
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        rowsDeleted++;
                    }
                }
            }

            if (rowsDeleted == 0)
                Response.Write("<script LANGUAGE='JavaScript'>alert('Ingen rader er valgt for sletting!')</script>");
            else
                Response.Write("<script LANGUAGE='JavaScript'>alert('Du har slettet valgte timer')</script>");

            employeeInfo = new Employee(phoneNumber.Value);
            GetTimeRegistrations(employeeInfo.EmployeeID);
        }
    }
}