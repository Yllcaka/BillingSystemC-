using BillingSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.DAL
{
    class userDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Selekton te dhena nga Databaze
        public DataTable Select() {
            //Metod statike per me lidh me Databaze
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Me mbajt te dhenat nga Databaza
            DataTable dt = new DataTable();
            try
            {
                //SQL Quesry per mi marr te gjitha te dhenat nga Databaza
                String sql = "SELECT * FROM tbl_users";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open(); // E qel lidhjen e Databazes me C#
                adapter.Fill(dt);// Mbush te dhena ne Tabelen e te edhenave

            }
            catch (Exception ex) {
            
                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();// E perfundon lidhjen me Databaze
            }
            return dt;
        }
        #endregion
        #region Fute te dhena ne DataBaze

        public bool Insert(userBLL u) {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try {
                String sql = "INSERT INTO tbl_userS (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) " +
                    "VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0 ? true : false; //Kallxon nese Query eshte ekzekutuar me sukses

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region I bon Update(Perditeson) te dhenate ne Databaze
        public bool Update(userBLL u) {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, " +
                    "email=@email, username=@username, password=@password, " +
                    "contact=@contact, address=@address, gender=@gender, user_type=@user_type, " +
                    "added_date=@added_date, added_by=@added_by" +
                    " WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0; //Kallxon nese Query eshte ekzekutuar me sukses
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region I Fshin te dhenat ne Databaze
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0; //Kallxon nese Query eshte ekzekutuar me sukses
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion
        #region Kerko User-in ne Database
        public DataTable Search(string keywords)
        {
            //Metod statike per me lidh me Databaze
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Me mbajt te dhenat nga Databaza
            DataTable dt = new DataTable();
            try
            {
                //SQL Quesry per mi marr te gjitha te dhenat nga Databaza
                String sql = $"SELECT * FROM tbl_users WHERE id LIKE '%{keywords}%' OR first_name LIKE '%{keywords}%' OR last_name LIKE '%{keywords}%' OR username LIKE '%{keywords}%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open(); // E qel lidhjen e Databazes me C#
                adapter.Fill(dt);// Mbush te dhena ne Tabelen e te edhenave

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();// E perfundon lidhjen me Databaze
            }
            return dt;
        }
        #endregion
        #region Marrja e User Id nga Usernam
        public userBLL GetIDFromUsername(string username) {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = $"SELECT id FROM tbl_users WHERE username '{username}'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                if (dt.Rows.Count > 0) u.id = int.Parse(dt.Rows[0]["id"].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();
            }
            return u;
        }
        #endregion
    }
}
