<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeregistrering.aspx.cs" Inherits="iFlow.TimeregistreringASP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="styles/MyStyle.css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">

        function DeleteKeyPress() {
            document.getElementById('input').value = '';
            document.getElementById('input').placeholder = 'Telefonnummer';
            document.getElementById('input').focus();
        }
        function BackKeyPress(){
            var text = document.getElementById('input').value;
            document.getElementById('input').value = text.substring(0, text.length - 1);
            document.getElementById('input').focus();
        }

        function KeyPress() {
            
            try {

                if (document.activeElement.id == 'input')
                    document.getElementById('input').value = document.activeElement.value;
                else
                    document.getElementById('input').value += document.activeElement.value;
                
                var number = parseInt(document.getElementById('input').value);
                
                if (number > 0) {
                    if (document.getElementById('input').placeholder == 'Telefonnummer') {
                        
                        document.getElementById('<%= phoneNumber.ClientID %>').value = document.getElementById('input').value
                    }
                    else {
                        document.getElementById('<%= hoursRegistered.ClientID %>').value = document.getElementById('input').value
                    }
                }

                else {
                    alert('Du kan kun taste inn hele tall!');
                    document.getElementById('input').value = '';
                }
            }
            catch {
                
            }

            document.getElementById('input').focus();
        }
        function RegisterPhone() {

            if (document.getElementById('input').placeholder == 'Telefonnummer' && (document.getElementById('input').value.length >= 8)) {
                    
                document.getElementById('<%= phoneNumber.ClientID %>').value = document.getElementById('input').value;
                document.getElementById('input').placeholder = 'Antall Timer';
                document.getElementById('input').value = '';
                document.getElementById('input').focus();
                document.getElementById('btnRegister').style.display = 'none';
                document.getElementById('<%= btnInsertHour.ClientID %>').disabled = false;
            }
            
            else {
                alert('SKriv inn gyldig telefonnummer!');
            }
        }

        function RegisterHours() {
            
            var number = parseInt(document.getElementById('input').value);

            if ((number == 0) || (number.toString() == 'NaN')) {
                document.getElementById('<%= hoursRegistered.ClientID %>').value = '';
                alert('Du må legge inn antall timer!');
            }
        }

        function LoadPage() {
            document.getElementById('txtDatePicker').value = new Date().toISOString().substr(0, 10);
            document.getElementById('input').focus();
        }
        function CommentKeyPress() {
            document.getElementById('<%= commentField.ClientID %>').value = document.getElementById('comment').value;   
        }
    </script>
    <h1 style="color:#ff7500;">Timeregistrering</h1>
    <asp:HiddenField ID="phoneNumber" runat="server"/>
    <asp:HiddenField ID="hoursRegistered" runat="server"/> 
    <asp:HiddenField ID="commentField" runat="server" />
    <input class="t1" placeholder="Telefonnummer" id="input" onkeyup="KeyPress();" type="text" /><br />
    <input id="btnRegister" type="button" class="b1" style="width:245px" onclick="RegisterPhone();" value="Legg til telefon"/><br />
    <div>
        <div>
            <input type="button" id="btn1" class="b1" onclick="KeyPress();" value="1"/>
            <input type="button" id="btn2" class="b1" onclick="KeyPress();" value="2"/>
            <input type="button" id="btn3" class="b1" onclick="KeyPress();" value="3"/>
        </div>
        <div>
            <input type="button" id="btn4" class="b1" onclick="KeyPress();" value="4"/>
            <input type="button" id="btn5" class="b1" onclick="KeyPress();" value="5"/>
            <input type="button" id="btn6" class="b1" onclick="KeyPress();" value="6"/>
        </div>
        <div>
            <input type="button" id="btn7" class="b1" onclick="KeyPress();" value="7"/>
            <input type="button" id="btn8" class="b1" onclick="KeyPress();" value="8"/>
            <input type="button" id="btn9" class="b1" onclick="KeyPress();" value="9"/>
        </div>
        <div>
            <input type="button" id="btnBack" class="b1" onclick="BackKeyPress();" value="BACK"/>
            <input type="button" id="btn0" class="b1" onclick="KeyPress();" value="0"/>
            <input type="button" id="btnOK" class="b1" onclick="DeleteKeyPress();" value="DEL"/>
        </div>
    </div>
    <br />
    <div style="align-content:inherit">
        <asp:TextBox ID="txtDatePicker" CssClass="t1" Width="245" TextMode="Date" runat="server"></asp:TextBox><br />
        <textarea class="t1" id="comment" placeholder="Skriv notat" onkeyup="CommentKeyPress();" style="height:100px;width:245px" rows="2"></textarea>
    </div>
    <div>
        <asp:Button ID="btnInsertHour" Text="Registrer Timeantall" UseSubmitBehavior="false" Enabled="true" Visible="true" BackColor="Green" Width="245px" CssClass="b1" OnClientClick="RegisterHours();" OnClick="btnRegisterTime_Click" runat="server"/>
    </div>    
</asp:Content>
