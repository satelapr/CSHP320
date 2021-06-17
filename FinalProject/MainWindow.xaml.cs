using ComputerAccessoryInventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComputerAccessoryInventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InventoryListView.Items.Clear();
            LoadListView();
            //btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void ListViewBinding(List<InventoryItem> inventoryItems)
        {
            InventoryListView.ItemsSource = inventoryItems;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(InventoryListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("ItemNo", ListSortDirection.Ascending));
            listStatusBar.Text = "Inventory Count: " + InventoryListView.Items.Count.ToString();
        }

        private void LoadListView()
        {
            DataAccessLayer dal = new DataAccessLayer();
            var itemInventoryList = dal.GetInventoryList();
            ListViewBinding(itemInventoryList);

            //InventoryListView.ItemsSource = itemInventoryList;
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(InventoryListView.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("ItemNo", ListSortDirection.Ascending));
            //listStatusBar.Text = "Inventory Count: " + InventoryListView.Items.Count.ToString();
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = InventoryListView.SelectedItem;
            txtItemNo.IsReadOnly = true;
            txtItemNo.Visibility = Visibility.Visible;
            lblItemId.Visibility = Visibility.Visible;

            if (item != null)
            {
                InventoryItem inventoryItem = item as InventoryItem;
                txtItemNo.Text = inventoryItem.ItemNo.ToString();
                txtItemDescrpition.Text = inventoryItem.Description.ToString();
                txtItemPrice.Text = inventoryItem.ItemPrice.ToString();
                txtItemQuanity.Text = inventoryItem.Quantity.ToString();
                txtItemCost.Text = inventoryItem.ItemCost.ToString();
            }
            DisableInputTextBoxes();
        }

        private void LoadList(object sender, RoutedEventArgs e)
        {
            txtMenuSelection.Text = "List";
            LoadListView();
            MessageBox.Show("Inventory List Refreshed");
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            txtMenuSelection.Text = "Add";

            txtItemNo.IsReadOnly = true;
            txtItemNo.Text = string.Empty;
            txtItemNo.Visibility = Visibility.Hidden;
            lblItemId.Visibility = Visibility.Hidden;
            EnableInputTextBoxes();

            ClearInputTextBoxes();

            btnSave.IsEnabled = true;
            //btnDelete.IsEnabled = false;

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<InventoryItem> latestInventory = new List<InventoryItem>();
            if (txtMenuSelection.Text.Equals("Add"))
            {
                List<string> validationMessages = new List<string>();
                if (string.IsNullOrWhiteSpace(txtItemDescrpition.Text.ToString()))
                {
                    validationMessages.Add("Item Description is required, Please enter.");
                }
                if (string.IsNullOrWhiteSpace(txtItemPrice.Text.ToString()))
                {
                    validationMessages.Add("Item Price is required, Please enter.");
                }
                else
                {
                    if (Double.Parse(txtItemPrice.Text) <= 0)
                    {
                        validationMessages.Add("Item Price should be greater than 0, Please enter.");
                    }
                }
                if (string.IsNullOrWhiteSpace(txtItemQuanity.Text.ToString()))
                {
                    validationMessages.Add("Item Quantity is required, Please enter.");
                }
                else
                {
                    if (Double.Parse(txtItemQuanity.Text) <= 0)
                    {
                        validationMessages.Add("Item Quantity should be greater than 0, Please enter.");
                    }
                }
                if (string.IsNullOrWhiteSpace(txtItemCost.Text.ToString()))
                {
                    validationMessages.Add("Item Cost is required, Please enter.");
                }
                else
                {
                    if (Double.Parse(txtItemCost.Text) <= 0)
                    {
                        validationMessages.Add("Item Cost should be greater than 0, Please enter.");
                    }
                }
                if (validationMessages.Count > 0)
                {
                    string validationMessage = String.Join(Environment.NewLine, validationMessages);
                    MessageBox.Show(validationMessage);
                    latestInventory = dal.GetInventoryList();
                }
                else
                {
                    InventoryItem item = new InventoryItem();
                    item.Description = txtItemDescrpition.Text;
                    item.ItemPrice = Double.Parse(txtItemPrice.Text);
                    item.Quantity = Int32.Parse(txtItemQuanity.Text);
                    item.ItemCost = Double.Parse(txtItemCost.Text);

                    latestInventory = dal.AddNewItem(item);
                    MessageBox.Show("Item add completed");
                }

                lblItemId.Visibility = Visibility.Hidden;
                txtItemNo.Visibility = Visibility.Hidden;
                txtItemNo.Text = string.Empty;
                //InventoryListView.ItemsSource = latestInventory;
                ListViewBinding(latestInventory);
                ClearInputTextBoxes();
            }
            else if (txtMenuSelection.Text.Equals("Edit"))
            {
                var itemToEdit = InventoryListView.SelectedItem;
                int itemNo = Int32.Parse(txtItemNo.Text);
                int quantityToUpdate = Int32.Parse(txtItemQuanity.Text);

                if (quantityToUpdate <= 0)
                {
                    MessageBox.Show("Item Quantity should be greater than 0, Please enter.");

                    InventoryListView.SelectedItem = itemToEdit;
                    var listBoxItem = (ListBoxItem)InventoryListView
                                       .ItemContainerGenerator
                                       .ContainerFromItem(InventoryListView.SelectedItem);

                    listBoxItem.Focus();
                }
                else
                {
                    latestInventory = dal.UpdateItem(itemNo, quantityToUpdate);
                    MessageBox.Show("Item update completed");

                    lblItemId.Visibility = Visibility.Visible;
                    txtItemNo.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = false;
                    ListViewBinding(latestInventory);

                    txtItemNo.Text = string.Empty;
                    ClearInputTextBoxes();
                }
            }
            else if (txtMenuSelection.Text.Equals("Delete"))
            {
                int itemIdToDelete = Int32.Parse(txtItemNo.Text);
                latestInventory = dal.DeleteItem(itemIdToDelete);
                MessageBox.Show("Item delete completed");

                lblItemId.Visibility = Visibility.Visible;
                txtItemNo.Visibility = Visibility.Visible;
                txtItemNo.Text = string.Empty;
                btnSave.IsEnabled = false;

                //InventoryListView.ItemsSource = latestInventory;
                ListViewBinding(latestInventory);
                ClearInputTextBoxes();
            }



        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            txtMenuSelection.Text = "Edit";
            var item = InventoryListView.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Please select item for edit");
                btnSave.IsEnabled = false;
            }
            else
            {
                btnSave.IsEnabled = true;
                txtItemQuanity.IsReadOnly = false;
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            txtMenuSelection.Text = "Delete";

            var item = InventoryListView.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Please select item for delete");
                btnSave.IsEnabled = false;
            }
            else
            {
                btnSave.IsEnabled = true;
            }
            //btnDelete.IsEnabled = true;
        }

        private void ValidateNumbers(object sender, TextCompositionEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]{0,1})?$");
            string str = txt.Text + e.Text.ToString();
            int cntPrc = 0;
            if (str.Contains('.'))
            {
                string[] tokens = str.Split('.');
                if (tokens.Count() > 0)
                {
                    string result = tokens[1];
                    char[] prc = result.ToCharArray();
                    cntPrc = prc.Count();
                }
            }
            if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)) && (cntPrc < 3))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ValidateInteger(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = !regex.IsMatch(e.Text);
        }


        private void EnableInputTextBoxes()
        {
            txtItemDescrpition.IsReadOnly = false;
            txtItemPrice.IsReadOnly = false;
            txtItemQuanity.IsReadOnly = false;
            txtItemCost.IsReadOnly = false;
        }

        private void DisableInputTextBoxes()
        {
            txtItemDescrpition.IsReadOnly = true;
            txtItemPrice.IsReadOnly = true;
            txtItemQuanity.IsReadOnly = true;
            txtItemCost.IsReadOnly = true;
        }

        private void ClearInputTextBoxes()
        {
            txtItemDescrpition.Text = string.Empty;
            txtItemPrice.Text = string.Empty;
            txtItemQuanity.Text = string.Empty;
            txtItemCost.Text = string.Empty;
        }

        private void QuitApplication(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
