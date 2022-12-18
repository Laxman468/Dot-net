<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="EmployeeCRUD.EmployeeDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 191px;
        }
        .auto-style2 {
            width: 191px;
            height: 29px;
        }
        .auto-style3 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Employee Details</h1>
        <asp:Panel ID="pnlGivew" runat="server">
        <div>
            <asp:Button ID="btnAddUpdatePanel" runat="server" Text="Add Employee Details" OnClick="btnAddUpdatePanel_Click"/><br /><br />
            <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvEmployee_SelectedIndexChanged" OnRowDataBound="gvEmployee_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id"></asp:BoundField>
                    <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name"></asp:BoundField>
                    <asp:BoundField DataField="department" HeaderText="Department" SortExpression="department"></asp:BoundField>
                    <asp:BoundField DataField="role" HeaderText="Role" SortExpression="role"></asp:BoundField>
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status"></asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmployeeDetailsConnectionString %>" SelectCommand="SELECT * FROM [EmpDetails]"></asp:SqlDataSource>

        </div>
       </asp:Panel>
        <asp:Panel ID="pnlAddEdit" runat="server" Visible="false">
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                     <asp:Label ID="lblId" runat="server" Text="ID" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="210px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" Height="18px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Width="215px">
                        <asp:ListItem Selected="True" Value=" ">-- Select item --</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlRole" runat="server" Height="18px" Width="215px">
                        <asp:ListItem Selected="True" Value=" ">-- Select item --</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Height="26px" Width="215px">
                        <asp:ListItem Selected="True" Value=" ">-- Select Item --</asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="0">in Active</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="55px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
&nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
</asp:Panel>
    </form>
</body>
</html>
