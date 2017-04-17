using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{

    OleDbConnection Econ;
    SqlConnection con;
    string constr, Query, sqlconn;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.BindGrid();
        }
    }

    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT CustomerId, ContactName, City, Country FROM Customers"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    private void ExcelConn(string FilePath)
    {

        constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
        Econ = new OleDbConnection(constr);

    }
    private void connection()
    {
        sqlconn = ConfigurationManager.ConnectionStrings["SqlCom"].ConnectionString;
        con = new SqlConnection(sqlconn);

    }

    private void InsertExcelRecords(string FilePath)
    {
        ExcelConn(FilePath);

        Query = string.Format("Select [Name],[City],[Address],[Designation] FROM [{0}]", "Sheet1$");
        OleDbCommand Ecom = new OleDbCommand(Query, Econ);
        Econ.Open();

        DataSet ds = new DataSet();
        OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
        Econ.Close();
        oda.Fill(ds);
        DataTable Exceldt = ds.Tables[0];
        connection();
        //creating object of SqlBulkCopy    
        SqlBulkCopy objbulk = new SqlBulkCopy(con);
        //assigning Destination table name    
        objbulk.DestinationTableName = "Employee";
        //Mapping Table column    
        objbulk.ColumnMappings.Add("Name", "Name");
        objbulk.ColumnMappings.Add("City", "City");
        objbulk.ColumnMappings.Add("Address", "Address");
        objbulk.ColumnMappings.Add("Designation", "Designation");
        //inserting Datatable Records to DataBase    
        con.Open();
        objbulk.WriteToServer(Exceldt);
        con.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string CurrentFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
        InsertExcelRecords(CurrentFilePath);
    }
}
