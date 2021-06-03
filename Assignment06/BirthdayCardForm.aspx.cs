using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment6
{
    public partial class BirthdayCardForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmptyValidationMessageText();

            trFromRow.Visible = true;
            trToRow.Visible = true;
            trMessageRow.Visible = true;
            trSubmitRow.Visible = true;
        }
        protected void btnSendCard_Click(object sender, EventArgs e)
        {
            EmptyValidationMessageText();

            if (string.IsNullOrWhiteSpace(txtFrom.Text))
            {
                lblFromValidationMessage.Text = "Please enter From";
            }
            if (string.IsNullOrWhiteSpace(txtTo.Text))
            {
                lblToValidationMessage.Text = "Please enter To";
            }
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                lblMessageValidationMessage.Text = "Please enter Message";
            }
            if ((!string.IsNullOrWhiteSpace(txtFrom.Text))
                && (!string.IsNullOrWhiteSpace(txtTo.Text))
                    && (!string.IsNullOrWhiteSpace(txtMessage.Text)))
            {
                lblFromValidationMessage.Text = "From: " + txtFrom.Text;
                lblToValidationMessage.Text = "To: " + txtTo.Text;
                lblMessageValidationMessage.Text = "Message: " + txtMessage.Text;

                trFromRow.Visible = false;
                trToRow.Visible = false;
                trMessageRow.Visible = false;
                trSubmitRow.Visible = false;
            }
        }

        private void EmptyValidationMessageText()
        {
            lblFromValidationMessage.Text = string.Empty;
            lblToValidationMessage.Text = string.Empty;
            lblMessageValidationMessage.Text = string.Empty;
        }
    }
}