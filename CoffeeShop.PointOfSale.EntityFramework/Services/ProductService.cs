using CoffeeShop.PointOfSale.EntityFramework.Controllers;
using CoffeeShop.PointOfSale.EntityFramework.Models;
using Spectre.Console;

namespace CoffeeShop.PointOfSale.EntityFramework.Services;

internal class ProductService
{
    internal static void InsertProduct()
    {
        var product = new Product();
        product.Name = AnsiConsole.Ask<string>("Product's name:");
        product.Price = AnsiConsole.Ask<decimal>("Product's price:");

        ProductsController.AddProduct(product);
    }
    internal static void DeleteProduct()
    {
        var product = GetProductOptionInput();
        ProductsController.DeleteProduct(product);
    }

    internal static void GetProducts()
    {
        var products = ProductsController.GetProducts();
        UserInterface.ShowProductTable(products);
    }

    internal static void GetProduct()
    {
        var product = GetProductOptionInput();
        UserInterface.ShowProduct(product);
    }

    internal static void UpdateProduct()
    {
        var product = GetProductOptionInput();

        product.Name = AnsiConsole.Confirm("Update name?") ? product.Name : AnsiConsole.Ask<string>("Product's new name:");
        product.Name = AnsiConsole.Confirm("Update price?") ? product.Name : AnsiConsole.Ask<string>("Product's new name:");

        ProductsController.UpdateProduct(product);
    }

    static private Product GetProductOptionInput()
    {
        var products = ProductsController.GetProducts();
        var productsArray = products.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Product")
            .AddChoices(productsArray));
        var id = products.Single(x => x.Name == option).ProductId;
        var product = ProductsController.GetProductById(id);

        return product;
    }
}