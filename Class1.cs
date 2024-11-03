using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DalLibrary
{
    public class EmployeeClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Dept { get; set; }
    }
    public class CDal
    {
        private readonly string cnnStr;
        SqlConnection cnn;
        SqlCommand cmd;

        public CDal(string cnnstr)
        {
            this.cnnStr = cnnstr;
            this.cnn = new SqlConnection(cnnstr);
            this.cmd = new SqlCommand();
            cmd.Connection = this.cnn;
        }
        public List<EmployeeClass> GetAllEmployees()
        {
            List<EmployeeClass> lstEmps = new List<EmployeeClass>();
            cmd.CommandText = "select * from Employee";
            cnn.Open();
            SqlDataReader reader = this.cmd.ExecuteReader();
            while (reader.Read())
            {
                lstEmps.Add(new EmployeeClass { Id = (int)reader[0], Name = reader[1].ToString(), Dept = (int)reader[2] });
            }
            cnn.Close();
            return lstEmps;
        }
        public EmployeeClass GetEmployeeById(int id)
        {
            EmployeeClass emp = null;
            cmd.CommandText = $"select * from Employee where eid={id}";
            cnn.Open();
            SqlDataReader reader = this.cmd.ExecuteReader();
            if (reader.Read())
            {
                emp = new EmployeeClass { Id = (int)reader[0], Name = reader[1].ToString(), Dept = (int)reader[2] };
            }
            cnn.Close();
            return emp;
        }
        public bool ModifyEmployee(EmployeeClass emp)
        {
            cmd.CommandText = $"update Employee set EName='{emp.Name}',Dept={emp.Dept} where Eid={emp.Id}";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }
        public bool RemoveEmployee(int id)
        {
            cmd.CommandText = $"delete  Employee where Eid={id}";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }
        public bool AddEmployee(EmployeeClass emp)
        {
            cmd.CommandText = $"insert into Employee values({emp.Id},'{emp.Name}',{emp.Dept})";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }
    }
}