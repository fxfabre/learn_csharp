using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using NLog;
using ReflexionLibs;

namespace ReflexionLibsUI
{
    public partial class DisplayType : UserControl
    {
        private static readonly Logger logger = LogManager.GetLogger("_Reflexion  UI__");
        private Type classType;

        private string dllName;
        private List<Type> listClass;
        private List<MemberInfo> listMethods;


        /******************
         * Constructor
         *******************/

        public DisplayType()
        {
            InitializeComponent();
        }


        /***********************
         * Update list Classes *
         ***********************/

        private void BtnLoadClick(object sender, EventArgs e)
        {
            loadingImg.Visible = true;

            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\Carpo\Documents\Visual Studio 2013\Projects\ReflexionLibsUI\bin\Debug",
                Filter = "DLL|*.dll",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ResetListClass();

                var thread = new Thread(GetUpdateListClass);
                dllName = openFileDialog1.FileName;
                thread.Start();

                lblDllName.Text = dllName;
            }
            else
            {
                loadingImg.Visible = false;
            }
        }

        private void ResetListClass()
        {
            logger.Info("Reset list class");
            lblDllName.Text = string.Empty;
            dllName = string.Empty;
            listClass = null;
            listBoxClassName.Items.Clear();

            ResetListMethods();
        }

        private void GetUpdateListClass()
        {
            if (!string.IsNullOrWhiteSpace(dllName))
            {
                logger.Info("UI : Loading assembly " + dllName);
                List<Type> list = AssemblyInfo.GetAssemblyFromFilePath(dllName);

                UpdateListClass(list);
            }
        }

        private void UpdateListClass(List<Type> list)
        {
            if (InvokeRequired)
            {
                object[] parameters = {list};
                Action<List<Type>> action = UpdateListClass;
                BeginInvoke(action, parameters);
            }
            else
            {
                DisplayClass(list);
                loadingImg.Visible = false;
            }
        }

        private void DisplayClass(List<Type> list)
        {
            ResetListClass();

            logger.Info("Display class names in the UI");
            foreach (Type className in list)
            {
                string displayString = string.Empty;

                // Public // not public
                if (className.IsPublic)
                {
                    displayString += "+ ";
                }
                else
                {
                    displayString += "-  ";
                }

                // Full name
                displayString += className.FullName;

                listBoxClassName.Items.Add(displayString);

                logger.Info("Adding " + displayString + " to the textBox");
            }
            listClass = list;
        }


        /***********************
         * Update list Methods *
         ***********************/

        private void ListBoxClassNameClick(object sender, EventArgs e)
        {
            if (listClass == null) return;

            loadingImg.Visible = true;
            ResetListMethods();

            var box = (ListBox) sender;

            int n = box.SelectedIndex;
            if ((n >= 0) && (n < listClass.Count))
            {
                var thread = new Thread(GetUpdateListMethods);
                classType = listClass[n];
                thread.Start();
            }
            else
            {
                loadingImg.Visible = false;
            }
        }

        private void ResetListMethods()
        {
            logger.Info("Reset list methods");
            classType = null;
            listMethods = null;
            listBoxMethods.Items.Clear();
        }

        private void GetUpdateListMethods()
        {
            if (classType != null)
            {
                List<MemberInfo> list = AssemblyInfo.GetAllItems(classType);

                UpdateListMethods(list);
            }
        }

        private void UpdateListMethods(List<MemberInfo> list)
        {
            if (InvokeRequired)
            {
                object[] parameters = {list};
                Action<List<MemberInfo>> action = UpdateListMethods;
                BeginInvoke(action, parameters);
            }
            else
            {
                ResetListMethods();

                DisplayMethods(list);

                listMethods = list;
                loadingImg.Visible = false;
            }
        }

        private void DisplayMethods(IEnumerable<MemberInfo> list)
        {
            foreach (MemberInfo method in list)
            {
                listBoxMethods.Items.Add(method.ToString());
            }
        }
    }
}