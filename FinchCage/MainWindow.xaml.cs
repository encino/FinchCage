using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinchCage { 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Replace this with path of the directory you want to watch
            //this.TextBoxFolderPath.Text = @"C:\Users\encino\Documents\test";
            this.TextBoxFolderPath.Text = @"C:\Users\Administrator\AppData\Roaming\MetaQuotes\Terminal\Common\Files"; 

            // Start file system watcher on selected folder
            StartFileSystemWatcher();
            GenerateControls();
        }




        public void GenerateControls()
        {
            int count = 0;
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            ColumnDefinition gridCol4 = new ColumnDefinition();
            ColumnDefinition gridCol5 = new ColumnDefinition();
            gridCol1.Width = new GridLength(180);
            GridCurrencies.ColumnDefinitions.Add(gridCol1);
            gridCol2.Width = new GridLength(40);
            GridCurrencies.ColumnDefinitions.Add(gridCol2);
            gridCol3.Width = new GridLength(50);
            GridCurrencies.ColumnDefinitions.Add(gridCol3);
            gridCol4.Width = new GridLength(40);
            GridCurrencies.ColumnDefinitions.Add(gridCol4);
            GridCurrencies.ColumnDefinitions.Add(gridCol5);

            //RowDefinition gridRow1 = new RowDefinition();
            //gridRow1.Height = new GridLength(25);
            //RowDefinition gridRow2 = new RowDefinition();
            //gridRow2.Height = new GridLength(45);
            //RowDefinition gridRow3 = new RowDefinition();
            //gridRow3.Height = new GridLength(45);
            //GridCurrencies.RowDefinitions.Add(gridRow1);
            //GridCurrencies.RowDefinitions.Add(gridRow2);
            //GridCurrencies.RowDefinitions.Add(gridRow3);

            //foreach (SettingsPropertyValue property in FinchCage.Properties.Settings.Default.PropertyValues)
            //{
            //        String name = Convert.ToString(property.Name);
            //        String value = Convert.ToString(property.PropertyValue);
            //}

            string[] currencies = Properties.Settings.Default.Currencies.ToString().Split(',');
            string[] currencyVals = Properties.Settings.Default.CurrencyValues.ToString().Split(',');

            for (int i = 0; i <= currencies.Length-1; i++)
            {
                GenerateCurrencyControls(currencies[i], currencyVals[i], i);
            }
            GenerateAddCurrencyControls(currencies.Count()+1);

        }

        public void GenerateCurrencyControls(string currency, string value, int count)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(25);
            GridCurrencies.RowDefinitions.Add(gridRow1);

            System.Windows.Controls.Label lblMax = new System.Windows.Controls.Label();
            lblMax.Content = "Max Trades for " + currency;
            Grid.SetRow(lblMax, count);
            Grid.SetColumn(lblMax, 0);
            GridCurrencies.Children.Add(lblMax);

            System.Windows.Controls.TextBox txtNumber = new System.Windows.Controls.TextBox();
            txtNumber.Name = "txtCurrencyVal" + currency;
            txtNumber.VerticalAlignment = VerticalAlignment.Top;
            txtNumber.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            txtNumber.Text = value;
            txtNumber.Width = 20;

            Grid.SetRow(txtNumber, count);
            Grid.SetColumn(txtNumber, 1);

            GridCurrencies.Children.Add(txtNumber);
            if (GridCurrencies.FindName(txtNumber.Name) == null)
                GridCurrencies.RegisterName(txtNumber.Name, txtNumber);

            System.Windows.Controls.Button btnClickMe = new System.Windows.Controls.Button();
            btnClickMe.Content = "Reset";
            btnClickMe.VerticalAlignment = VerticalAlignment.Top;
            btnClickMe.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btnClickMe.Name = "btnSet" + currency;
            btnClickMe.Click += new RoutedEventHandler(this.CallMeClick);

            Grid.SetRow(btnClickMe, count);
            Grid.SetColumn(btnClickMe, 3);
            GridCurrencies.Children.Add(btnClickMe);
        }

        public void GenerateAddCurrencyControls(int count)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(25);
            GridCurrencies.RowDefinitions.Add(gridRow1);

            System.Windows.Controls.Label lblMax = new System.Windows.Controls.Label();
            lblMax.Content = "Add Currency:";
            Grid.SetRow(lblMax, count);
            Grid.SetColumn(lblMax, 0);
            GridCurrencies.Children.Add(lblMax);

            System.Windows.Controls.TextBox txtCurrency = new System.Windows.Controls.TextBox();
            txtCurrency.Name = "txtCurrencyNew";
            txtCurrency.VerticalAlignment = VerticalAlignment.Top;
            txtCurrency.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            txtCurrency.Width = 30;

            Grid.SetRow(txtCurrency, count);
            Grid.SetColumn(txtCurrency, 1);

            GridCurrencies.Children.Add(txtCurrency);
            if (GridCurrencies.FindName(txtCurrency.Name) == null)
                GridCurrencies.RegisterName(txtCurrency.Name, txtCurrency);

            System.Windows.Controls.Label lblMaxVal = new System.Windows.Controls.Label();
            lblMaxVal.Content = "Max:";
            lblMaxVal.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Grid.SetRow(lblMaxVal, count);
            Grid.SetColumn(lblMaxVal, 2);
            GridCurrencies.Children.Add(lblMaxVal);

            System.Windows.Controls.TextBox txtValue = new System.Windows.Controls.TextBox();
            txtValue.Name = "txtCurrencyNewValue";
            txtValue.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            txtValue.VerticalAlignment = VerticalAlignment.Top;
            txtValue.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            txtValue.Width = 20;

            Grid.SetRow(txtValue, count);
            Grid.SetColumn(txtValue, 3);

            GridCurrencies.Children.Add(txtValue);
            if (GridCurrencies.FindName(txtValue.Name) == null)
                GridCurrencies.RegisterName(txtValue.Name, txtValue);

            System.Windows.Controls.Button btnClickMe = new System.Windows.Controls.Button();
            btnClickMe.Content = "Set";
            btnClickMe.VerticalAlignment = VerticalAlignment.Top;
            btnClickMe.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btnClickMe.Name = "btnSetNew";
            btnClickMe.Click += new RoutedEventHandler(this.NewCurrencySettingClick);

            Grid.SetRow(btnClickMe, count);
            Grid.SetColumn(btnClickMe, 4);
            GridCurrencies.Children.Add(btnClickMe);
        }

        protected void CallMeClick(object sender, RoutedEventArgs e)
        {
            string currency = ((System.Windows.Controls.Button)sender).Name.Substring(6);
            string maxVal = ((System.Windows.Controls.TextBox)this.GridCurrencies.FindName("txtCurrencyVal" + currency)).Text;

            string[] currencies = Properties.Settings.Default.Currencies.ToString().Split(',');
            string[] currencyVals = Properties.Settings.Default.CurrencyValues.ToString().Split(',');

            if (currencies.Contains(currency))
            {
                Dictionary<string, string> currVals = new Dictionary<string, string>(); //put in dict to re-order
                for (int i = 0; i <= currencies.Length - 1; i++)
                {
                    currVals.Add(currencies[i], currencyVals[i]);
                }
                currVals[currency] = maxVal;

                Properties.Settings.Default.Currencies = String.Join(",", currVals.Select(k => k.Key).ToList());
                Properties.Settings.Default.CurrencyValues = String.Join(",", currVals.Select(k => k.Value).ToList());
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }

            CheckCurrencyLimits(this.TextBoxFolderPath.Text);
            
            //string message = string.Format("The number is {0}", txtNumber.Text);
            //System.Windows.MessageBox.Show(message);
        }

        protected void NewCurrencySettingClick(object sender, RoutedEventArgs e)
        {
            string currency = ((System.Windows.Controls.TextBox)this.GridCurrencies.FindName("txtCurrencyNew")).Text.ToUpper();
            string maxVal = ((System.Windows.Controls.TextBox)this.GridCurrencies.FindName("txtCurrencyNewValue")).Text;

            string[] currencies = Properties.Settings.Default.Currencies.ToString().Split(',');
            string[] currencyVals = Properties.Settings.Default.CurrencyValues.ToString().Split(',');

            if (!currencies.Contains(currency))
            {
                Dictionary<string, string> currVals = new Dictionary<string, string>(); //put in dict to re-order
                for (int i = 0; i <= currencies.Length - 1; i++)
                {
                    currVals.Add(currencies[i], currencyVals[i]);
                }
                currVals.Add(currency, maxVal);

                Properties.Settings.Default.Currencies = String.Join(",", currVals.OrderBy(key => key.Key).Select(k => k.Key).ToList());
                Properties.Settings.Default.CurrencyValues = String.Join(",", currVals.OrderBy(key => key.Key).Select(k => k.Value).ToList());
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }

            CheckCurrencyLimits(this.TextBoxFolderPath.Text);
        }


        private void ButtonOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            // Browse folder to monitor
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TextBoxFolderPath.Text = folderBrowserDialog.SelectedPath;
            }

            // Start file system watcher on selected folder
            StartFileSystemWatcher();
        }

        private void StartFileSystemWatcher()
        {
            string folderPath = this.TextBoxFolderPath.Text;

            // If there is no folder selected, to nothing
            if (string.IsNullOrWhiteSpace(folderPath) || !System.IO.Directory.Exists(folderPath))
                return;

            System.IO.FileSystemWatcher fileSystemWatcher = new System.IO.FileSystemWatcher();

            // Set folder path to watch
            fileSystemWatcher.Path = folderPath;

            // Gets or sets the type of changes to watch for.
            // In this case we will watch change of filename, last modified time, size and directory name
            fileSystemWatcher.NotifyFilter = System.IO.NotifyFilters.FileName |
                System.IO.NotifyFilters.LastWrite |
                System.IO.NotifyFilters.Size |
                System.IO.NotifyFilters.DirectoryName;


            // Event handlers that are watching for specific event
            fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(fileSystemWatcher_Created);
            //fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(fileSystemWatcher_Changed);
            fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(fileSystemWatcher_Deleted);
            //fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(fileSystemWatcher_Renamed);

            // NOTE: If you want to monitor specified files in folder, you can use this filter
            // fileSystemWatcher.Filter

            // START watching
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        // ----------------------------------------------------------------------------------
        // Events that do all the monitoring
        // ----------------------------------------------------------------------------------

        void fileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            if (e.Name.StartsWith("finch-") && e.Name.EndsWith(".txt"))
            {
                CheckCurrencyLimits(e.FullPath);
                DisplayFileSystemWatcherInfo(e.ChangeType, e.Name);
            }
        }

        void fileSystemWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            DisplayFileSystemWatcherInfo(e.ChangeType, e.Name);
        }

        void fileSystemWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            if (e.Name.StartsWith("finch-") && e.Name.EndsWith(".txt"))
            {
                CheckCurrencyLimits(e.FullPath);
                DisplayFileSystemWatcherInfo(e.ChangeType, e.Name);
            }
        }

        void fileSystemWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            DisplayFileSystemWatcherInfo(e.ChangeType, e.Name, e.OldName);
        }

        // ----------------------------------------------------------------------------------

        void DisplayFileSystemWatcherInfo(System.IO.WatcherChangeTypes watcherChangeTypes, string name, string oldName = null)
        {
            if (watcherChangeTypes == System.IO.WatcherChangeTypes.Renamed)
            {
                // When using FileSystemWatcher event's be aware that these events will be called on a separate thread automatically!!!
                // If you call method AddListLine() in a normal way, it will throw following exception: 
                // "The calling thread cannot access this object because a different thread owns it"
                // To fix this, you must call this method using Dispatcher.BeginInvoke(...)!
                Dispatcher.BeginInvoke(new Action(() => { AddListLine(string.Format("{0} -> {1} to {2} - {3}", watcherChangeTypes.ToString(), oldName, name, DateTime.Now)); }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => { AddListLine(string.Format("{0} -> {1} - {2}", watcherChangeTypes.ToString(), name, DateTime.Now)); }));
            }
        }

        public void AddListLine(string text)
        {
            this.ListBoxFileSystemWatcher.Items.Add(text);
        }

        void CheckCurrencyLimits(string filePath)
        {

            try
            {

                string[] currencies = Properties.Settings.Default.Currencies.ToString().Split(',');
                string[] currencyVals = Properties.Settings.Default.CurrencyValues.ToString().Split(',');

                Dictionary<string, string> currVals = new Dictionary<string, string>(); //put in dict to re-order
                for (int i = 0; i <= currencies.Length - 1; i++)
                {
                    currVals.Add(currencies[i], currencyVals[i]);
                }

                Dictionary<string, int> buy =
                new Dictionary<string, int>();

                Dictionary<string, int> sell =
                new Dictionary<string, int>();

                string folder = String.Empty;
                if (System.IO.Directory.Exists(filePath))
                {
                    folder = filePath; 
                } else
                {
                    folder = System.IO.Path.GetDirectoryName(filePath); 
                }
                    
            string[] fileEntries = Directory.GetFiles(folder, "finch-*");

            ///TODO loop FOR each additional directory


            foreach (string fileName in Directory.GetFiles(folder, "finchcaged-*"))
            {
                File.Delete(fileName);
            }

            foreach (string folderAndFileName in fileEntries)
            {
                string fileName = System.IO.Path.GetFileName(folderAndFileName);

                bool buyOrder = fileName.Substring(13, 3).ToLower() == "buy";

                string buycurrency = String.Empty;
                string sellcurrency = String.Empty;

                if (buyOrder)
                {
                    buycurrency = fileName.Substring(6, 3);
                    sellcurrency = fileName.Substring(9, 3);
                }
                else
                {
                    buycurrency = fileName.Substring(9, 3);
                    sellcurrency = fileName.Substring(6, 3);
                }


                if (buy.ContainsKey(buycurrency))
                {
                    int count = buy[buycurrency];
                    buy[buycurrency] = count + 1;
                }
                else
                {
                    buy.Add(buycurrency, 1);
                }

                if (sell.ContainsKey(sellcurrency))
                {
                    int count = sell[sellcurrency];
                    sell[sellcurrency] = count + 1;
                }
                else
                {
                    sell.Add(sellcurrency, 1);
                }
            }

            foreach (KeyValuePair<string, int> kvp in sell)
            {
                int max = 0;
                string maxAction = String.Empty;

                    if (currVals.ContainsKey(kvp.Key))
                    {
                        max = Convert.ToInt32(currVals[kvp.Key]);
                    }
                    else
                    {
                        max = Convert.ToInt32(currVals["Default"]);
                    }

                    string blockfile = System.IO.Path.Combine(folder, "finchcaged-" + kvp.Key + "-sell.txt");
                if (max <= kvp.Value)
                {
                    maxAction = "*BLOCK FURTHER SELLS*";
                    CreateBlockFiles(blockfile);
                }

                Dispatcher.BeginInvoke(new Action(() => { AddListLine(string.Format("sell {0}: {1} Max: {2}  {3}", kvp.Key, kvp.Value.ToString(), max.ToString(), maxAction)); }));

            }
            foreach (KeyValuePair<string, int> kvp in buy)
            {

                int max = 0;
                string maxAction = String.Empty;

                if (currVals.ContainsKey(kvp.Key))
                {
                    max = Convert.ToInt32(currVals[kvp.Key]);
                }
                else
                {
                    max = Convert.ToInt32(currVals["Default"]);
                }

                string blockfile = System.IO.Path.Combine(folder, "finchcaged-" + kvp.Key + "-buy.txt");
                if (max <= kvp.Value)
                {
                    maxAction = "*BLOCK FURTHER BUYS*";
                    CreateBlockFiles(blockfile);
                }

                Dispatcher.BeginInvoke(new Action(() => { AddListLine(string.Format("buy {0}: {1} Max: {2} {3}", kvp.Key, kvp.Value.ToString(), max.ToString(), maxAction)); }));
            }

            }
            catch (Exception Ex)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show(Ex.InnerException.ToString(), "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool DoesSettingExist(string settingName)
        {
            return FinchCage.Properties.Settings.Default.Properties.Cast<SettingsProperty>().Any(prop => prop.Name == settingName);
        }

        private void CreateBlockFiles(string fileName)
        {

            ///TODO
            ///FOR each folder

            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    return; // File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file
                    //Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                    //fs.Write(title, 0, title.Length);

                }

                // Open the stream and read it back.
                //using (StreamReader sr = File.OpenText(fileName))
                //{
                //    string s = "";
                //    while ((s = sr.ReadLine()) != null)
                //    {
                //        Console.WriteLine(s);
                //    }
                //}
            }
            catch (Exception Ex)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show(Ex.InnerException.ToString(), "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.ListBoxFileSystemWatcher.Items.Clear();
            CheckCurrencyLimits(this.TextBoxFolderPath.Text);
        }
    }
}