using System;
using System.Windows.Forms;
using Calculatrice_AST;

namespace Calculatrice.CalculatriceUI
{
    public partial class Numbers : UserControl
    {
        private bool textBoxHasdefaultString = true;

        public Numbers()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (textBoxHasdefaultString)
            {
                textBoxOperation.Text = string.Empty;
                textBoxHasdefaultString = false;
            }

            Button button = (Button) sender;
            textBoxOperation.Text += button.Text;
        }

        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            if (textBoxOperation.Text.Length > 0)
            {
                textBoxOperation.Text = textBoxOperation.Text.Substring(0, textBoxOperation.Text.Length - 1);
            }
        }

        private void ButtonClearClick(object sender, EventArgs e)
        {
            textBoxOperation.Text = string.Empty;
        }

        private void BtnComputeClick(object sender, EventArgs e)
        {
            StringOperations myOper = new StringOperations( textBoxOperation.Text );
            double? result = myOper.EvalTree();

            if (result == null)
            {
                textBoxResult.Text = "Error !";
            }
            else
            {
                textBoxResult.Text = result.ToString();
            }
        }

        private void TextBoxOperationKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                BtnComputeClick(sender, e);
            }
        }
        
    }
}
