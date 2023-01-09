
// See https://aka.ms/new-console-template for more information
using ProjetOpenAPI;

Console.WriteLine("Hello, World!");




//Création d'un client relié à tous nos points d'entrée du Projet "TestWebAPI) via OpenAPI
var httpClient = new HttpClient();
var client = new WebAPI(		
     "https://localhost:7093",			
     httpClient);



