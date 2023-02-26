using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace iFlow
{ 
    public partial class TimeregistreringASP : System.Web.UI.Page
    {
        static string sqlConnectionString = ConfigurationManager.ConnectionStrings["iFlowConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
                btnInsertHour.Attributes.Add("display", "hidden");
                txtDatePicker.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnRegisterTime_Click(object sender, EventArgs e)
        {
            int hoursWorked = 0; try { hoursWorked = Convert.ToInt32(hoursRegistered.Value); } catch { }
            
            if(hoursWorked > 0)
            {
                int userID = GetUserID(phoneNumber.Value);
                if (userID > 0)
                {
                    InsertTime(userID, hoursWorked, Convert.ToDateTime(txtDatePicker.Text), commentField.Value);
                    Response.Write("<script language=javascript>alert('Arbeidstimene dine er registrert!')</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Telefonnummer " + phoneNumber.Value + " er ikke registrert!');</script>");
                }
            }   
        }

        public static int GetUserID(string phoneNumber)
        {
            int userID = 0;
            string sqlQuery = "select EmployeeID FROM Employees WHERE UserName=@UserName";
            
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserName", phoneNumber);
                    
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    
                    while(sqlDataReader.Read())
                    {
                        userID = Convert.ToInt32(sqlDataReader[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("< script language = javascript > alert('Telefonnummer" + AphoneNumber.Value + " er ikke registrert!');</ script >");
            }

            return userID;
        }
        private void InsertTime(int employeeID, int hours, DateTime date, string note)
        {
            string sqlQuery = "INSERT INTO TImeRegistrations(EmployeeID, Hours, DateStamp, Notes, TimeRegistered) VALUES (@EmployeeID, @Hours, @DateStamp, @Notes, GETDATE())";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmployeeID", employeeID.ToString());
                    sqlCommand.Parameters.AddWithValue("@Hours", hours.ToString());
                    sqlCommand.Parameters.Add("@DateStamp", date.ToString("yyyy-MM-dd"));
                    sqlCommand.Parameters.AddWithValue("@Notes", commentField.Value);

                    sqlCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}