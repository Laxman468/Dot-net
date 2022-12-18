using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EmployeeCRUD
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {

        #region variables
        string cs = ConfigurationManager.ConnectionStrings["EmployeeDetailsConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        #endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                departmentDatabind();
            }
        }
        #endregion

        #region Add button to populate add panel
        protected void btnAddUpdatePanel_Click(object sender, EventArgs e)
        {
            pnlAddEdit.Visible = true;
            pnlGivew.Visible = false;
            btnDelete.Visible = false;
        }
        #endregion

        #region Cancel button click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearData();
        }
        #endregion

        #region populate data to text fields
        protected void gvEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAddEdit.Visible = true;
            pnlGivew.Visible = false;
            ddlRole.Enabled = true;
            departmentDatabind();
            lblId.Text = gvEmployee.SelectedRow.Cells[1].Text;
            txtName.Text = gvEmployee.SelectedRow.Cells[2].Text;
            ddlDepartment.Items.FindByText(gvEmployee.SelectedRow.Cells[3].Text).Selected= true;
            roleDropdowndatabind();
            ddlRole.Items.FindByText(gvEmployee.SelectedRow.Cells[4].Text).Selected= true;
            if (gvEmployee.SelectedRow.Cells[5].Text == "Active")
            {
                ddlStatus.Text = "1";
            }
            else
            {
                ddlStatus.Text = "0";
            }
            btnAdd.Text = "Update";
        }
        #endregion

        #region Insert and update record
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                if (txtName.Text != "" && ddlDepartment.Text != "")
                {
                    using (con=new SqlConnection(cs))
                    {
                        con.Open();
                        cmd = new SqlCommand("INSERT INTO EmpDetails(name,department,role,status) Values(@name,@department,@role,@status)", con);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@department", ddlDepartment.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@role", ddlRole.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@status", ddlStatus.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataLoad();
                        clearData();
                    }
                }
                else
                {
                    lblMessage.Text = "Fill all Information";
                }
            }
            else
            {
                using(con=new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("update EmpDetails set name=@name,department=@department,role=@role,status=@status where id=@id", con);
                    cmd.Parameters.AddWithValue("@id", lblId.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@department", ddlDepartment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@role", ddlRole.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@status",ddlStatus.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DataLoad();
                    clearData();
                }
            }
            
        }
        #endregion

        #region Status drodown text method
        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.BackColor =System.Drawing.Color.Azure;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[5];
                if (statusCell.Text == "1")
                {
                    statusCell.Text = "Active";
                    e.Row.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    statusCell.Text = "in Active";
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

        #region delete employee record
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using(con=new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Delete from EmpDetails where id=@id", con);
                cmd.Parameters.AddWithValue("@id",lblId.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                DataLoad();
                clearData();
            }
        }
        #endregion

        #region Role drodown bind
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            roleDropdowndatabind();
        }
        #endregion

        #region Private Methods

        #region Grid View Data Load 
        public void DataLoad()
        {
            if (Page.IsPostBack)
            {
                gvEmployee.DataBind();
            }
        }
        #endregion

        #region Clear Controls
        public void clearData()
        {
            pnlAddEdit.Visible = false;
            pnlGivew.Visible = true;
            btnDelete.Visible = true;
            btnAdd.Text = "Add";
            txtName.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlRole.Items.Clear();
            lblMessage.Text = string.Empty;
            departmentDatabind();
        }

        #endregion

        #region Department dropdown data bind
        public void departmentDatabind()
        {
            using(con=new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand();
                da=new SqlDataAdapter("select * from department", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataBind();
                ddlDepartment.DataTextField= "name";
                ddlDepartment.DataValueField= "id";
                ddlDepartment.DataBind();
                con.Close();
            }
            ddlRole.Enabled= false;
            ddlRole.Items.Insert(0, new ListItem("--Select Role --", "0"));
            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        }

        #endregion


        #region role dropdown bing method
        public void roleDropdowndatabind()
        {
            ddlRole.Enabled = true;
            ddlRole.Items.Insert(0, new ListItem("--Select Role --", "0"));
            int dept_value = int.Parse(ddlDepartment.SelectedItem.Value);
            if (dept_value > 0)
            {
                string query = string.Format("select * from role where dept_id = {0}", dept_value);
                cmd = new SqlCommand(query);
                using (con = new SqlConnection(cs))
                {
                    using (da = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        ddlRole.DataSource = cmd.ExecuteReader();
                        ddlRole.DataTextField = "name";
                        ddlRole.DataValueField = "id";
                        ddlRole.DataBind();
                        con.Close();
                    }
                }

            }
            else
            {
                ddlRole.Enabled = false;
                ddlRole.SelectedValue = "0";
            }
        }
        #endregion

        #endregion

    }
}