using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/products", (Product product) => {
    ProductRepository.Add(product);
});

app.MapGet("/products/{code}", ([FromRoute]string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

app.MapPut("/products", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

app.MapDelete("/products/{code}",([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
});
app.Run();


public static class ProductRepository {
    public static List<Product> Products { get; set; }

    public static void Add(Product product){
        if(Products is null){
            Products = new List<Product>();
        }
        Products.Add(product);
    }

    public static Product GetBy(string code){
        return Products.FirstOrDefault(x => x.Code == code);
    }

    public static void  Remove(Product product){
        Products.Remove(product);
    }
}



public class Product{
    public string Code { get; set; }
    public string Name { get; set; }
}