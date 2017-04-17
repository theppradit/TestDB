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
    string constrEx, Query, sqlconn;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.IsPostBack)
        //{
        //    this.BindGrid();
        //}
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Name, City, Address, Designation FROM dbo.Employee"))
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

        constrEx = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
        Econ = new OleDbConnection(constrEx);

    }
    private void connection()
    {
        con = new SqlConnection(constr);
    }

    private void InsertExcelRecords(string FilePath)
    {
        ExcelConn(FilePath);

        Query = string.Format("SELECT [Name],[City],[Address],[Designation] FROM [Sheet1$]");
        OleDbCommand Ecom = new OleDbCommand(Query, Econ);
        Econ.Open();

        DataSet ds = new DataSet();
        OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
        Econ.Close();
        oda.Fill(ds);
        DataTable Exceldt = ds.Tables[0];

        GridView1.DataSource = Exceldt;
        GridView1.DataBind();
        //connection();
        ////creating object of SqlBulkCopy    
        //SqlBulkCopy objbulk = new SqlBulkCopy(con);
        ////assigning Destination table name    
        //objbulk.DestinationTableName = "dbo.Employee";
        ////Mapping Table column    
        //objbulk.ColumnMappings.Add("Name", "Name");
        //objbulk.ColumnMappings.Add("City", "City");
        //objbulk.ColumnMappings.Add("Address", "Address");
        //objbulk.ColumnMappings.Add("Designation", "Designation");
        ////inserting Datatable Records to DataBase    
        //con.Open();
        //objbulk.WriteToServer(Exceldt);
        //con.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string CurrentFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
        //InsertExcelRecords(CurrentFilePath);
        btnImport(sender, e);
    }

    protected void btnImport(object sender, EventArgs e)
    {
        string connString = "";
        string strFileType = Path.GetExtension(FileUpload1.FileName).ToLower();
        string path = FileUpload1.PostedFile.FileName;
        //Connection String to Excel Workbook
        if (strFileType.Trim() == ".xls")
        {
            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        }
        else if (strFileType.Trim() == ".xlsx")
        {
            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        }
        string query = "SELECT [Name],[City],[Address],[Designation] FROM [Sheet1$]";
        OleDbConnection conn = new OleDbConnection(connString);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        OleDbCommand cmd = new OleDbCommand(query, conn);
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        da.Dispose();
        conn.Close();
        conn.Dispose();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}
