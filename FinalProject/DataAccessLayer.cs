using ComputerAccessoryInventory.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ComputerAccessoryInventory
{
    public class DataAccessLayer
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["PCPDatabase"].ConnectionString;

        public List<InventoryItem> GetInventoryList()
        {
            List<InventoryItem> inventoryList = new List<InventoryItem>();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("SELECT * FROM ComputerAccessoryInventory", sqlConn))
                {
                    if (sqlComm.Connection.State == System.Data.ConnectionState.Closed)
                        sqlComm.Connection.Open();
                    SqlDataReader sqlRd = sqlComm.ExecuteReader();

                    // Get Inventory List
                    while (sqlRd.Read())
                    {
                        InventoryItem inventoryItem = new InventoryItem();

                        inventoryItem.ItemNo = Int32.Parse(sqlRd["ItemNo"].ToString());
                        inventoryItem.Description = sqlRd["Description"].ToString();

                        double itemPrice = Double.Parse(sqlRd["ItemPrice"].ToString());
                        inventoryItem.ItemPrice = itemPrice;

                        int itemQuantity = Int32.Parse(sqlRd["Quantity"].ToString());
                        inventoryItem.Quantity = itemQuantity;

                        double itemCost = Double.Parse(sqlRd["ItemCost"].ToString());
                        inventoryItem.ItemCost = itemCost;

                        inventoryItem.ItemValue = itemPrice * itemQuantity;

                        inventoryList.Add(inventoryItem);
                    }
                    sqlConn.Close();
                }
            }
            return inventoryList;
        }

        public List<InventoryItem> AddNewItem(InventoryItem inventoryItem)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand(@"INSERT INTO [dbo].[ComputerAccessoryInventory] 
                                                    ([Description],[ItemPrice],[Quantity],[ItemCost])
                                                     VALUES(@Description,@ItemPrice,@Quantity,@ItemCost)", sqlConn))
                {
                    if (sqlComm.Connection.State == System.Data.ConnectionState.Closed)
                        sqlComm.Connection.Open();

                    sqlComm.Parameters.AddWithValue("@Description", inventoryItem.Description);
                    sqlComm.Parameters.AddWithValue("@ItemPrice", inventoryItem.ItemPrice);
                    sqlComm.Parameters.AddWithValue("@Quantity", inventoryItem.Quantity);
                    sqlComm.Parameters.AddWithValue("@ItemCost", inventoryItem.ItemCost);
                    sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            return GetInventoryList();
        }

        public List<InventoryItem> UpdateItem(int itemIdToUpdate, int quanityToUpdate)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand(@"UPDATE [dbo].[ComputerAccessoryInventory]
                                                   SET Quantity= @Quantity" +
                                                " WHERE ItemNo=@ItemNo", sqlConn))
                {
                    if (sqlComm.Connection.State == System.Data.ConnectionState.Closed)
                        sqlComm.Connection.Open();

                    sqlComm.Parameters.AddWithValue("@Quantity", quanityToUpdate);
                    sqlComm.Parameters.AddWithValue("@ItemNo", itemIdToUpdate);
                    sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            return GetInventoryList();
        }

        public List<InventoryItem> DeleteItem(int deleteItemNo)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand(@"DELETE FROM [dbo].[ComputerAccessoryInventory]" +
                                                " WHERE ItemNo=" + deleteItemNo.ToString(), sqlConn))
                {
                    if (sqlComm.Connection.State == System.Data.ConnectionState.Closed)
                        sqlComm.Connection.Open();

                    sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            return GetInventoryList();
        }

    }
}
