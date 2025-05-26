using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MVC_No_1.Models; 
namespace MVC_No_1.Controllers 
{
    public class InvoiceController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _invoiceConnectionString;

        public InvoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
            _invoiceConnectionString = _configuration.GetConnectionString("Invoice_DatabaseConnection"); 
        }

        public IActionResult InvoiceHome()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_invoiceConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CommercialInvoiceNo, ShipDate, Forwarder, BLNo, PO, Line, PartNo, VendorCode, Qty, UnitPrice, Amount, Currency, FinanceDate, CustomerName FROM Customer";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        customers.Add(new Customer
                        {
                            CommercialInvoiceNo = row["CommercialInvoiceNo"]?.ToString(),
                            ShipDate = Convert.ToDateTime(row["ShipDate"]),
                            Forwarder = row["Forwarder"]?.ToString(),
                            BLNo = row["BLNo"]?.ToString(),
                            PO = row["PO"]?.ToString(),
                            Line = row["Line"]?.ToString(),
                            PartNo = row["PartNo"]?.ToString(),
                            VendorCode = row["VendorCode"]?.ToString(),
                            Qty = Convert.ToInt32(row["Qty"]),
                            UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                            Amount = Convert.ToDecimal(row["Amount"]),
                            Currency = row["Currency"]?.ToString(),
                            FinanceDate = Convert.ToDateTime(row["FinanceDate"]),
                            CustomerName = row["CustomerName"]?.ToString()
                        });
                    }
                }
                catch (SqlException ex)
                {
                    TempData["ErrorMessage"] = "Lỗi khi tải dữ liệu từ Database (ITInventory): " + ex.Message;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi không mong muốn khi tải dữ liệu: " + ex.Message;
                }
            }

            return View(customers);
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_invoiceConnectionString))
                {
                    string query = @"INSERT INTO Customer (CommercialInvoiceNo, ShipDate, Forwarder, BLNo, PO, Line, PartNo, VendorCode, Qty, UnitPrice, Amount, Currency, FinanceDate, CustomerName)
                                     VALUES (@CommercialInvoiceNo, @ShipDate, @Forwarder, @BLNo, @PO, @Line, @PartNo, @VendorCode, @Qty, @UnitPrice, @Amount, @Currency, @FinanceDate, @CustomerName)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CommercialInvoiceNo", customer.CommercialInvoiceNo);
                        command.Parameters.AddWithValue("@ShipDate", customer.ShipDate);
                        command.Parameters.AddWithValue("@Forwarder", string.IsNullOrEmpty(customer.Forwarder) ? (object)DBNull.Value : customer.Forwarder);
                        command.Parameters.AddWithValue("@BLNo", string.IsNullOrEmpty(customer.BLNo) ? (object)DBNull.Value : customer.BLNo);
                        command.Parameters.AddWithValue("@PO", customer.PO);
                        command.Parameters.AddWithValue("@Line", customer.Line);
                        command.Parameters.AddWithValue("@PartNo", customer.PartNo);
                        command.Parameters.AddWithValue("@VendorCode", customer.VendorCode);
                        command.Parameters.AddWithValue("@Qty", customer.Qty);
                        command.Parameters.AddWithValue("@UnitPrice", customer.UnitPrice);
                        command.Parameters.AddWithValue("@Amount", customer.Qty * customer.UnitPrice);
                        command.Parameters.AddWithValue("@Currency", customer.Currency);
                        command.Parameters.AddWithValue("@FinanceDate", customer.FinanceDate);
                        command.Parameters.AddWithValue("@CustomerName", string.IsNullOrEmpty(customer.CustomerName) ? (object)DBNull.Value : customer.CustomerName);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                TempData["SuccessMessage"] = "Dữ liệu đã được lưu thành công!";
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Lưu dữ liệu không thành công. Không có dòng nào bị ảnh hưởng.";
                            }
                        }
                        catch (SqlException ex)
                        {
                            TempData["ErrorMessage"] = "Lỗi Database: " + ex.Message;
                        }
                        catch (Exception ex)
                        {
                            TempData["ErrorMessage"] = "Đã xảy ra lỗi: " + ex.Message;
                        }
                    }
                }
                return RedirectToAction("InvoiceHome");
            }
            return View("InvoiceHome", GetCustomers());
        }

        private List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(_invoiceConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CommercialInvoiceNo, ShipDate, Forwarder, BLNo, PO, Line, PartNo, VendorCode, Qty, UnitPrice, Amount, Currency, FinanceDate, CustomerName FROM Customer";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        customers.Add(new Customer
                        {
                            CommercialInvoiceNo = row["CommercialInvoiceNo"]?.ToString(),
                            ShipDate = Convert.ToDateTime(row["ShipDate"]),
                            Forwarder = row["Forwarder"]?.ToString(),
                            BLNo = row["BLNo"]?.ToString(),
                            PO = row["PO"]?.ToString(),
                            Line = row["Line"]?.ToString(),
                            PartNo = row["PartNo"]?.ToString(),
                            VendorCode = row["VendorCode"]?.ToString(),
                            Qty = Convert.ToInt32(row["Qty"]),
                            UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                            Amount = Convert.ToDecimal(row["Amount"]),
                            Currency = row["Currency"]?.ToString(),
                            FinanceDate = Convert.ToDateTime(row["FinanceDate"]),
                            CustomerName = row["CustomerName"]?.ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
            return customers;
        }
    }
}