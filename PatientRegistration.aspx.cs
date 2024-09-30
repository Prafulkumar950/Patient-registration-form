using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class PatientRegistration : System.Web.UI.Page
{
    DbFunctions objfun = new DbFunctions();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objfun.FillDropdownlist(ddlDoctor, "DoctorName", "DoctorId", "SELECT * FROM DoctorTable ", "Select Doctor");

            ddlTreatment.Items.Clear();
            ddlTreatment.Items.Add(new ListItem("Select Treatment", "0"));

            ddlHospital.Items.Clear();
            ddlHospital.Items.Add(new ListItem("Select Hospital", "0"));
            Grid();
            btnDltSelected.Visible = false;
            btnCancel.Visible = false;
        }
       
        lblMessage.Text = "";
    }

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDoctor.SelectedValue != "0")
        {
            //objfun.FillDropdownlist(ddlTreatment, "TreatmentName", "TreatmentId", "SELECT * FROM TreatmentTable WHERE DoctorId = '" + ddlDoctor.SelectedValue + "'", "Select Treatment ");

            string TID = objfun.Get_details("SELECT TreatmentId FROM DoctorTable WHERE DoctorId = '" + ddlDoctor.SelectedValue + "'");
            objfun.FillDropdownlist(ddlTreatment, "TreatmentName", "TreatmentId", "SELECT TreatmentId, TreatmentName FROM TreatmentTable WHERE TreatmentId = '" + TID + "'", "");

            string HID = objfun.Get_details("SELECT HospitalId FROM TreatmentTable WHERE TreatmentId = '" + TID + "'");
            objfun.FillDropdownlist(ddlHospital, "HospitalName", "HospitalId", "SELECT HospitalId, HospitalName FROM HospitalTable WHERE HospitalId = '" + HID + "'", "");
        }
        else
        {
            ddlTreatment.Items.Clear();
            ddlTreatment.Items.Add(new ListItem("Select Treatment", "0"));

            ddlHospital.Items.Clear();
            ddlHospital.Items.Add(new ListItem("Select Hospital", "0"));
        }
    }

    //protected void ddlTreatment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlTreatment.SelectedValue != "0")
    //    {
    //        objfun.FillDropdownlist(ddlHospital, "HospitalName", "HospitalId", "SELECT * FROM HospitalTable WHERE TreatmentId = '" + ddlTreatment.SelectedValue + "'", "Select Hospital");

    //    }
    //    else
    //    {

    //        ddlTreatment.Items.Clear();
    //        ddlTreatment.Items.Add(new ListItem("Select Treatment", "0"));
    //    }
    //}

    

    public void Grid()
    {
        dt = objfun.FillDataTable("SELECT  * FROM PatientRegistration order by Id DESC  ");
        if (dt.Rows.Count > 0)
        {
            griddetail.DataSource = dt;
            griddetail.DataBind();

        }
        else
        {
            griddetail.DataSource = null;
            griddetail.DataBind();

        }
    }

    protected void griddetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddetail.PageIndex = e.NewPageIndex;
        Grid();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnDelete = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btnDelete.NamingContainer;
        int rowIndex = row.RowIndex;

        //string id = (griddetail.DataKeys[rowIndex].Values["Name"].ToString());
        int id = Convert.ToInt32(griddetail.DataKeys[rowIndex].Values["Id"]);
       // objfun.Fill_FormDeleteDML(id);
        objfun.ExecuteDML("DELETE  FROM PatientRegistration  WHERE Id = '" + id + "'");
        //objfun.ExecuteDML("DELETE  FROM PatientRegistration  WHERE Name = '" + id + "'");

        Grid();
        objfun.MsgBox("Record Deleted SuccessFully", this);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        LinkButton btnEdit = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btnEdit.NamingContainer;
        int rowIndex = row.RowIndex;
        int id = Convert.ToInt32(griddetail.DataKeys[rowIndex].Values ["Id"]);
        DataTable dt = objfun.FillDataTable("SELECT Name, Age, Address, Contact, Doctor, Treatment, Hospital FROM PatientRegistration WHERE Id = '" + id + "'");
        if (dt.Rows.Count > 0)
        {
            string name = dt.Rows[0]["Name"].ToString();
            string age = dt.Rows[0]["Age"].ToString();
            string address = dt.Rows[0]["Address"].ToString();
            string contact = dt.Rows[0]["Contact"].ToString();
            string doctor = dt.Rows[0]["Doctor"].ToString();
            string treatment = dt.Rows[0]["Treatment"].ToString();
            string hospital = dt.Rows[0]["Hospital"].ToString();

            txtName.Text = name;
            txtAge.Text = age;
            txtAddress.Text = address;
            txtContact.Text = contact;

            objfun.FillDropdownlist(ddlDoctor, "DoctorName", "DoctorId", "SELECT * FROM DoctorTable", "Select Doctor");
            ddlDoctor.SelectedValue = objfun.Get_details("SELECT DoctorId FROM DoctorTable WHERE DoctorName = '" + doctor + "'");

            string TId = objfun.Get_details("SELECT TreatmentId FROM DoctorTable WHERE DoctorName = '" + doctor + "'");
            objfun.FillDropdownlist(ddlTreatment, "TreatmentName", "TreatmentId", "SELECT * FROM TreatmentTable WHERE TreatmentId = '" + TId + "'", "Select Treatment");
            ddlTreatment.SelectedValue = TId;

            string CId = objfun.Get_details("SELECT HospitalId FROM TreatmentTable WHERE TreatmentId = '" + TId + "'");
            objfun.FillDropdownlist(ddlHospital, "HospitalName", "HospitalId", "SELECT * FROM HospitalTable WHERE HospitalId = '" + CId + "'", "Select Hospital");
            ddlHospital.SelectedValue = CId;

            HiddenF.Value = id.ToString();
            btnRegister.Text = "UPDATE";
            btnCancel.Visible = true;
           

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetForm();
        btnRegister.Text = "REGISTER";
        btnCancel.Visible = false;
    }


    protected void btnRegister_Click(object sender, EventArgs e)
    {
        // Check if already exists
        string checkQuery = ("SELECT COUNT (*) FROM PatientRegistration WHERE  Name = '" + txtName.Text + "' AND Age = '" + txtAge.Text + "' AND Address = '" + txtAddress.Text + "' AND Contact = '" + txtContact.Text + "' AND Doctor = '" + ddlDoctor.SelectedItem.Text + "' AND Treatment = '" + ddlTreatment.SelectedItem.Text + "' AND Hospital = '" + ddlHospital.SelectedItem.Text + "' ");
        int count = Convert.ToInt32(objfun.Get_details(checkQuery));
        if (count > 0)
        {
            lblMessage.Text = "Selected Record is already exists.";
        }
        else
        {
            if (btnRegister.Text == "UPDATE")
            {
                int id = Convert.ToInt32(HiddenF.Value);
                int update = objfun.ExecuteDML("UPDATE PatientRegistration SET Name = '" + txtName.Text + "', Age = '" + txtAge.Text + "', Address = '" + txtAddress.Text + "', Contact = '" + txtContact.Text + "', Doctor = '" + ddlDoctor.SelectedItem.Text + "', Treatment = '" + ddlTreatment.SelectedItem.Text + "', Hospital = '" + ddlHospital.SelectedItem.Text + "' WHERE Id ='" + id + "'");
                //int update = objfun.Fill_FormUpdateDML( txtName.Text, int.Parse(txtAge.Text), txtAddress.Text, txtContact.Text, ddlDoctor.SelectedItem.Text, ddlTreatment.SelectedItem.Text, ddlHospital.SelectedItem.Text, id);
                btnCancel.Visible = false;
                btnRegister.Text = "REGISTER";
                Grid();
                objfun.MsgBox("Record Updated SuccessFully", this);
            }
            else
            {
                 int save = objfun.ExecuteDML("INSERT INTO PatientRegistration (Name, Age, Address, Contact, Doctor, Treatment, Hospital) VALUES ('" + txtName.Text + "','" + txtAge.Text + "','" + txtAddress.Text + "','" + txtContact.Text + "','" + ddlDoctor.SelectedItem.Text + "','" + ddlTreatment.SelectedItem.Text + "','" + ddlHospital.SelectedItem.Text + "')");
               //int save = objfun.Fill_FormDML( txtName.Text, Convert.ToInt32(txtAge.Text), txtAddress.Text, txtContact.Text, ddlDoctor.SelectedItem.Text, ddlTreatment.SelectedItem.Text, ddlHospital.SelectedItem.Text);
                    
                    
                    //int save = objfun.Fill_FormDML();
                Grid();
                objfun.MsgBox("Record Stored SuccessFully", this);
            }
            ResetForm();
        }
    }

    private void ResetForm()
    {
        txtName.Text = "";
        txtAge.Text = "";
        txtAddress.Text = "";
        txtContact.Text = "";
        ddlDoctor.SelectedIndex = 0;
        
        HiddenF.Value = "";
       
        ddlTreatment.Items.Clear();
        ddlTreatment.Items.Add(new ListItem("Select Treatment", "0"));

        ddlHospital.Items.Clear();
        ddlHospital.Items.Add(new ListItem("Select Hospital", "0"));
    }




    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chkAll = (CheckBox)sender;
        bool allChecked = chkAll.Checked;

        foreach (GridViewRow row in griddetail.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null)
            {
                chk.Checked = allChecked;
            }
        }

        btnDltSelected.Visible = allChecked;
    }
        
    

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {

        bool anyChecked = false;
        foreach (GridViewRow row in griddetail.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null && chk.Checked)
            {
                anyChecked = true;
                break;
            }
        }
        btnDltSelected.Visible = anyChecked;



        bool allChecked = true;
        foreach (GridViewRow row in griddetail.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null && !chk.Checked)
            {
                allChecked = false;
                break;
            }
        }

        CheckBox chkSelectAll = (CheckBox)griddetail.HeaderRow.FindControl("chkSelectAll");
        if (chkSelectAll != null)
        {
            chkSelectAll.Checked = allChecked;
        }
        
    }
    
    
    protected void btnDltSelected_Click(object sender, EventArgs e)
    {
        bool anySelected = false;
        List<string> deletedData = new List<string>();


        foreach (GridViewRow row in griddetail.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            
           
            if (chk != null && chk.Checked)
            {
                anySelected = true;
                int id = Convert.ToInt32(griddetail.DataKeys[row.RowIndex].Values["Id"]);
               
                  DataTable dt = objfun.FillDataTable("SELECT * FROM PatientRegistration WHERE Id = '" + id + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string name = dt.Rows[0]["Name"].ToString();
                        string age = dt.Rows[0]["Age"].ToString();
                        string address = dt.Rows[0]["Address"].ToString();
                        string contact = dt.Rows[0]["Contact"].ToString();

                        objfun.ExecuteDML("DELETE FROM PatientRegistration WHERE Id = '" + id + "'");

                        deletedData.Add(name);
                        deletedData.Add(age);
                        deletedData.Add(address);
                        deletedData.Add(contact);

                    }
            }
        }


        if (anySelected)
        {
            if (deletedData.Count > 0)
            {
                string data = string.Join(" | ", deletedData);
                objfun.MsgBox("Records Deleted Successfully: " + data, this);

            }
        }
        else
        {
            objfun.MsgBox("No data is selected", this);
        }

        Grid();
        btnDltSelected.Visible = false;

    }
}



    