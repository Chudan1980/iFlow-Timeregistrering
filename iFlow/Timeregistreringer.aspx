<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeregistreringer.aspx.cs" Inherits="iFlow.Timeregistreringer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function KeyPress() {
            
            try {
                var number = document.getElementById('input').value;
                if (number > 0)
                    document.getElementById('<%= phoneNumber.ClientID %>').value = document.getElementById('input').value
                else {
                    alert('Du kan kun skrive hele tall!');
                    document.getElementById('input').value = '';
                }
            }
            catch { }
        }
        function SearchEmployee() {
            
            if (document.getElementById('input').value.length < 8) {
                alert('Telefonnummeret du skrev må være minst 8 tegn!');
                document.getElementById('input').focus();
            }
        }
    </script>
    <h1>Mine Timeregistreringer</h1>
    <input id="input" class="t1" placeholder="Telefonnummer" onkeyup="KeyPress();" type="text" />
    <asp:HiddenField  ID="phoneNumber" runat="server"/>
    <asp:Button ID="btnSearchEmployee" CssClass="b2" OnClientClick="SearchEmployee();" OnClick="btnSearchEmployee_Click" Text="Søk" runat="server"/><br />
    <asp:GridView ID="gvTimeRegistrations" CssClass="GridView" AlternatingRowStyle-CssClass="GridViewAlternate" HeaderStyle-CssClass ="GridViewHeader" RowStyle-CssClass="GridViewRows"  Enabled="true" Visible="true" runat="server" Width="419px" AutoGenerateColumns="false" DataKeyNames="TimeRegistrationID">
        <Columns>
            <asp:BoundField DataField="TimeRegistrationID" Visible="true" HeaderText="ID" ReadOnly="True" SortExpression="TimeRegistrationID" InsertVisible="True" />
            <asp:BoundField DataField="DateStamp" HeaderText="Dato" SortExpression="DateStamp" />
            <asp:BoundField DataField="Hours" HeaderText="Antall Timer" SortExpression="Hours" />
            <asp:BoundField DataField="Notes" HeaderText="Kommentar" SortExpression="Notes" />
            <asp:BoundField DataField="TimeRegistered" HeaderText="Registrert Tid" SortExpression="TimeRegistered"/>
            <asp:TemplateField HeaderText="Slett Oppgr." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100">  
                <HeaderTemplate>
                    <asp:CheckBox ID="chcDeleteTimeAll" TextAlign="Left" Text="Slett Time" AutoPostBack="true" OnCheckedChanged="ChcDeleteTimeAll_CheckedChanged" runat="server" />
                </HeaderTemplate>
                    <ItemTemplate>  
                        <asp:CheckBox ID="chcDeleteTime" runat="server"/>  
                    </ItemTemplate>  
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sqlEmployees" runat="server" ConnectionString="<%$ ConnectionStrings:iFlowConnectionString %>" 
       SelectCommand="SELECT TimeregistrationID, format(DateStamp, 'dd-MM-yyyy') as DateStamp, Hours, Notes, TimeRegistered FROM TimeRegistrations WHERE EmployeeID=@EmployeeID"
        DeleteCommand="DELETE FROM TimeRegistrations WHERE TimeRegistrationID=@TimeRegistrationID"
        >
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Button ID="btnDeleteTime" CssClass="b3" Visible="false" Text="Slett Timer" OnClick="btnDeleteTime_Click"  runat="server"/>
</asp:Content>
