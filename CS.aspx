<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView Paging Example</title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            border-collapse: collapse;
            background-color: #fff;
        }
        table th
        {
            background-color: #B8DBFD;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border: 1px solid #ccc;
        }
        table, table table td
        {
            border: 0px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
            <Columns>
                <asp:BoundField ItemStyle-Width="150px" DataField="Name" HeaderText="Name" />
                <asp:BoundField ItemStyle-Width="150px" DataField="City" HeaderText="City" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Address" HeaderText="Address" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Designation" HeaderText="Designation" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<asp:Button ID="Button1" runat="server" Height="22px" Text="Show in GridView" OnClick="Button1_Click" />
    &nbsp;<asp:Button ID="Button4" runat="server" Height="22px" OnClick="Button4_Click" Text="Upload to TB Employee" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Has Header ?"></asp:Label>
        <asp:RadioButtonList ID="rbHDR" runat="server" BorderStyle="None" BorderWidth="0px">
            <asp:ListItem Text = "Yes" Value = "Yes" Selected = "True" ></asp:ListItem>
            <asp:ListItem Text = "No" Value = "No"></asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="Button2" runat="server" Height="22px" OnClick="Button2_Click" Text="ShowTB Employee" />
        &nbsp;<asp:Button ID="Button3" runat="server" Height="22px" OnClick="Button3_Click" Text="TruncateTB Employee" />
        <br />
        <br />
    </form>
</body>
</html>
