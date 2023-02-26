using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace iFlow
{
    public class Employee
    {
        private string sqlConnectionString = ConfigurationManager.ConnectionStrings["iFlowConnectionString"].ConnectionString;
        private int totalWorkHour;
        public int EmployeeID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int TotalWorkHours { get { return totalWorkHour; }}
        public Employee(string userName)
        {
            GetUserInfo(userName);
        }
        public void GetUserInfo(string userName)
        {
            using(SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            using(SqlCommand sqlCommand = new SqlCommand("select * from employees where username=@UserName", sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    EmployeeID = Convert.ToInt32(sqlDataReader["EmployeeID"].ToString());
                    UserName = sqlDataReader["UserName"].ToString();
                    FullName = sqlDataReader["FullName"].ToString();
                    Email = sqlDataReader["Email"].ToString();
                    GetTotalWorkHours(EmployeeID);
                }
            }
        }
        private void GetTotalWorkHours(int employeeID)
        {

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            using (SqlCommand sqlCommand = new SqlCommand("select ISNULL(SUM(Hours),0) as 'TotalWorkHour' from Timeregistrations where EmployeeID=@EmployeeID", sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    totalWorkHour = Convert.ToInt32(sqlDataReader["TotalWorkHour"].ToString());
                }
            }
        }
    }
}