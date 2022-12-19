using FolkaShop.WebApi.Data.Repository;
using FolkaShop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace FolkaShop.Console
{
    class Program
    {
        private static int categoryId;
        private readonly static string spaces = "        ";

        public static async Task Main(string[] args)
        {
            await DefaultShowApp();          
            System.Console.ReadLine();
        }

        private static async Task DefaultShowApp()
        {
            try
            {
                await GetCategory();
                System.Console.Write("Entry Category Id : ");
                categoryId = int.Parse(System.Console.ReadLine());
            }
            finally
            {
                System.Console.WriteLine();
                await GetProduct(categoryId);
            }
        }

        private static async Task GetCategory()
        {
            try
            {
                BaseHttpRepository<Category> _categorys = new BaseHttpRepository<Category>();
                var categorys = await _categorys.GetHttpRequest("category", false, 0);

                foreach (var item in categorys)
                {
                    System.Console.WriteLine(item.Id + ". " + item.Name);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write("Error Message : " + ex.Message);
            }
        }

        private static async Task GetProduct(int categoryId)
        {
            try
            {
                BaseHttpRepository<Product> _products = new BaseHttpRepository<Product>();
                var products = await _products.GetHttpRequest("product", true, categoryId);

                foreach (var item in products)
                {
                    var idrprice = "Rp. " + item.Price.ToString("c2", CultureInfo.GetCultureInfo("es-es")).Replace("€", "");
                    System.Console.WriteLine(item.SKU + spaces + item.Name + spaces + item.Description + spaces + idrprice);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write("Error Message : " + ex.Message);
            }
            finally
            {
                categoryId = 0;
                System.Console.WriteLine();
                System.Console.WriteLine("************************************************************");
                System.Console.WriteLine();
                await DefaultShowApp();
            }
        }
    }
}
