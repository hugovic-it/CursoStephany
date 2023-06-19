using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/user" , () => new {Name = "Hugo Victor", Age = 25} );
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste","Hugo Victor");
    return "Olá";
});

app.MapPost("/saveproduct", (Product product) => {
    return product.Code + " - " + product.Name;
});


//api.app.com/users?datestart={date}&dateend={date}  //query
app.MapGet("/getproduct", ([FromQuery] string dateStart,[FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});
//api.app.com/users/{code}  //rota
app.MapGet("/getproduct/{code}", ([FromRoute]string code) => {
    return code;
});

//header
app.MapGet("/getprodutctbyheader" , (HttpRequest request) => {
    return request.Headers["product-code"].ToString();
});

app.Run();

public class Product{
    public string Code { get; set; }
    public string Name { get; set; }
}