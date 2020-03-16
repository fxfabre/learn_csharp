using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GestionPersonnel;
using NLog;

namespace GestionPersonnelUI
{
    public partial class DisplayPersonnel : UserControl
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private Personnel listPersonnel;
        private string fileName = string.Empty;

        private UserActions currentAction = UserActions.Unknown;
        private Personne personToUpdate = null;

        enum UserActions
        {
            Unknown,
            AddPerson,
            ModifyPerson
        }

        public DisplayPersonnel()
        {
            logger.Info("Initializing UI");
            InitializeComponent();

            this.listPersonnel = new Personnel();
            this.currentAction = UserActions.Unknown;
            this.SetVisibleModifyArea(false);
        }


        /**********************
         * Update view
         **********************/
        private void UpdateListView()
        {
            logger.Debug("UpdateListView");
            if (InvokeRequired)
            {
                logger.Info("Calling same function in the graphic thread");
                Action action = this.UpdateListView;
                BeginInvoke(action, new object[] { });
            }
            else
            {
                logger.Info("Clearing list");
                this.listBoxNames.Items.Clear();

                foreach (var personne in this.listPersonnel.ListPersonnes)
                {
                    logger.Debug("adding person to list " + personne.ToString());
                    this.listBoxNames.Items.Add(personne.AsShortString());
                }
            }
        }

        private void SetVisibleModifyArea(bool toDisplay)
        {
            logger.Debug("SetVisibleModifyArea : " + toDisplay);

            this.lblName.Visible = toDisplay;
            this.txtBoxName.Visible = toDisplay;

            this.lblAdress.Visible = toDisplay;
            this.txtBoxAdress.Visible = toDisplay;

            this.lblEmail.Visible = toDisplay;
            this.txtBoxEmail.Visible = toDisplay;

            this.LblFirstName.Visible = toDisplay;
            this.txtBoxFirstName.Visible = toDisplay;

            this.btnSave.Visible = toDisplay;
        }

        private void ClearModifyArea()
        {
            logger.Debug("ClearModifyArea");

            this.txtBoxName.Text = string.Empty;
            this.txtBoxFirstName.Text = string.Empty;
            this.txtBoxEmail.Text = string.Empty;
            this.txtBoxAdress.Text = string.Empty;
        }

        private void DisplayPersonne(Personne personne)
        {
            logger.Debug("DisplayPersonne " + personne.ToString());

            this.SetVisibleModifyArea(true);

            this.txtBoxName.Text = personne.Nom;
            this.txtBoxFirstName.Text = personne.Prenom;
            this.txtBoxEmail.Text = personne.Email;
            this.txtBoxAdress.Text = personne.Adresse;
        }

        /*********************
         * Load and save XMl
         *********************/
        private void BtnSaveXmlClick(object sender, EventArgs e)
        {
            logger.Debug("BtnSaveXmlClick");

            this.currentAction = UserActions.Unknown;
            this.SetVisibleModifyArea(false);


            if (this.listPersonnel != null)
            {
                logger.Info("Exporting list in file " + "outputFile.xml");
                this.listPersonnel.ExportXml("outputFile.xml");
            }
            else
            {
                logger.Info("List null, cannot save in file");
            }
        }

        private void BtnLoadXmlClick(object sender, EventArgs e)
        {
            logger.Debug("BtnLoadXmlClick");

            this.currentAction = UserActions.Unknown;
            this.SetVisibleModifyArea(false);

            logger.Info("Opening windows to select a file");
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\Carpo\Documents\Visual Studio 2013\Projects\GestionPersonnel",
                Filter = "XML|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                logger.Debug("Window : DialogResult.OK");
                logger.Info("Creating thread to retrieve the data");

                var thread = new Thread(this.LoadXml);
                this.fileName = openFileDialog.FileName;
                thread.Start();
            }
            else
            {
                logger.Warn("can't get filename from OpenFileDialog");
            }
        }

        private void LoadXml()
        {
            logger.Debug("LoadXml");
            logger.Info("Import XML file : " + this.fileName);

            this.listPersonnel.ImportXml(this.fileName);
            
            this.UpdateListView();
        }


        /************************
         * Add and delete users
         ************************/
        private void BtnDeleteClick(object sender, EventArgs e)
        {
            logger.Debug("BtnDeleteClick");

            this.currentAction = UserActions.Unknown;
            this.SetVisibleModifyArea(false);

            int selectIndex = this.listBoxNames.SelectedIndex;
            logger.Debug("Selected index in listBoxNames : " + selectIndex);

            if (selectIndex >= 0)
            {
                logger.Info("Removing item " + selectIndex);
                this.listPersonnel.ListPersonnes.RemoveAt(selectIndex);
            }
            this.UpdateListView();
        }

        private void BtnClearClick(object sender, EventArgs e)
        {
            logger.Debug("BtnClearClick");

            this.currentAction = UserActions.Unknown;
            this.SetVisibleModifyArea(false);

            this.listPersonnel = new Personnel();
            this.UpdateListView();
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            logger.Debug("BtnAddClick");

            this.currentAction = UserActions.AddPerson;

            this.SetVisibleModifyArea(true);
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            logger.Debug("BtnSaveClick");

            if (this.currentAction == UserActions.AddPerson)
            {
                logger.Info("Adding a new Personne");
                var name = txtBoxName.Text;
                var firstName = txtBoxFirstName.Text;
                var email = txtBoxEmail.Text;
                var adress = txtBoxAdress.Text;

                var personne = new Personne(name, firstName, adress, email);
                this.listPersonnel.AddPerson(personne);
            }
            else if (this.currentAction == UserActions.ModifyPerson)
            {
                logger.Info("Modifying : "+ this.personToUpdate.ToString());
                this.personToUpdate.Nom = txtBoxName.Text;
                this.personToUpdate.Prenom = txtBoxFirstName.Text;
                this.personToUpdate.Adresse = txtBoxAdress.Text;
                this.personToUpdate.Email = txtBoxEmail.Text;
                logger.Info("Modified : " + this.personToUpdate.ToString());
            }

            this.currentAction = UserActions.Unknown;
            this.UpdateListView();
            this.SetVisibleModifyArea(false);
            this.ClearModifyArea();
        }

        private void ListBoxNamesClick(object sender, EventArgs e)
        {
            logger.Debug("ListBoxNamesClick");

            this.currentAction = UserActions.Unknown;
            this.SetVisibleModifyArea(false);

            var index = this.listBoxNames.SelectedIndex;
            if (index < 0)
            {
                logger.Warn("Click on an item that does not exist ?");
            }
            else
            {
                logger.Info("Displaying a personne");

                currentAction = UserActions.ModifyPerson;
                Personne personne = this.listPersonnel.ListPersonnes[index];
                this.DisplayPersonne(personne);
                this.personToUpdate = personne;
            }
        }


    }
}