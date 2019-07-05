using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace task8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties

        private string[] girlsNames = { };//inicialization of the empty array for names categories
        private string[] boysNames = { };

        private string Project_path //path to files with names
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            }
        }

        #endregion Properties

        #region Constants

        private readonly string FILEGIRLS = "GirlNames.txt";
        private readonly string FILEBOYS = "BoyNames.txt";

        #endregion Constants

        public MainWindow()
        {
            InitializeComponent();
            LoadNamesData(); //load content in List Boxes
        }

        #region Private Methods

        private void LoadNamesData()
        {
            LoadGirlsData();
            LoadBoysData();
        }

        private void LoadGirlsData() { // getting data from file and populate 
            string girlNamesFilePath = Project_path + "/" + FILEGIRLS;
            string[] girlsNamesList = File.ReadAllLines(girlNamesFilePath);
            
            PopulateListBoxWithData(GirlNames_ListBox, girlsNamesList);

            this.girlsNames = girlsNamesList; //fill property array with names from file
        }

        private void LoadBoysData() // getting data from file and populate
        {
            string boyNamesFilePath = Project_path + "/" + FILEBOYS;
            string[] boysNamesList = File.ReadAllLines(boyNamesFilePath);

            PopulateListBoxWithData(BoyNames_ListBox, boysNamesList);

            this.boysNames = boysNamesList;
        }

        private void PopulateListBoxWithData(ListBox listbox, string[] data) //adding data to the ListBox
        {
            listbox.Items.Clear();

            foreach (var item in data)
            {
                listbox.Items.Add(item);
            }
        }

        private void FillDataForQuery(string query, string[] data, ListBox listBox) //sort names for searcheable querry
        {
            string[] names; //declaring array with results names

            if (string.IsNullOrEmpty(query))
            {
                names = data;
            }
            else
            {
                names = Array.FindAll(data, s => s.ToLower().Contains(query.ToLower()));

                if (names.Length == 0)
                {
                    names = new string[] { "Not Found" }; //initialize array with new value
                }
            }

            PopulateListBoxWithData(listBox, names);
        }

        #endregion Private Methods

        #region Action Handlers 

        //handles TextBoxes changes

        private void GirlName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = GirlName_TextBox.Text;

            FillDataForQuery(query, girlsNames, GirlNames_ListBox);
        }

        private void BoyName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = BoyName_TextBox.Text;

            FillDataForQuery(query, boysNames, BoyNames_ListBox);
        }

        #endregion Action Handlers
    }
}
