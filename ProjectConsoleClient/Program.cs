// See https://aka.ms/new-console-template for more information
using ProjectConsoleClient;


Console.WriteLine("Appuyer sur une touche lorsque l'API sera lancée. Svp");
Console.ReadKey(true);

var httpClient = new HttpClient();
var client = new WebApi(		    //LE NOM MIS DANS LE CS.PROJ
     "https://localhost:7093",			//ATTENTION A METTRE VOTRE PORT
     httpClient);

var customers = await client.ReadAllAsync();
foreach (var item in customers)
{
    Console.WriteLine($"{item.Id} - {item.LastName}") ;
}

