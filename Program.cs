using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

app.MapGet("/getproduct/{code}", ([FromRoute]string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
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
}



public class Product{
    public string Code { get; set; }
    public string Name { get; set; }
}