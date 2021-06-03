<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BirthdayCardForm.aspx.cs" Inherits="Assignment6.BirthdayCardForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header>
                <h2 style="border:0.5px solid Beige;">Birthday Card Generator</h2>
            </header>
            <h1>Send a Birthday Card</h1>
            <asp:Table ID="Table1" runat="server">
                   <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblFromValidationMessage" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                     </asp:TableRow>
                   <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblToValidationMessage" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                      <asp:TableCell>
                        <asp:Label ID="lblMessageValidationMessage" runat="server" Text=""></asp:Label>
                   </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="trSpaceRow" runat="server">
                    <asp:TableCell>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="trFromRow" runat="server">
                    <asp:TableCell>
                        <asp:Label ID="lblFrom" runat="server" Text="From:"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                 <asp:TableRow ID="trToRow" runat="server">
                    <asp:TableCell>
                       <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="trMessageRow" runat="server">
                    <asp:TableCell>
                       <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtMessage" runat="server" style="background-color:orange;"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="trSubmitRow" runat="server">
                    <asp:TableCell>
                      <asp:Button ID="btnSubmit" Text="Send Card" runat="server" OnClick="btnSendCard_Click" style="background-color:MediumSeaGreen;"></asp:Button>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
