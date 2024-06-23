using MySql.Data.MySqlClient;
using LabresultsProject;
using System;
using static LabresultsProject.Results;

var builder = WebApplication.CreateBuilder(args);
string connectionString = "server=localhost;uid=root;pwd=mypassword;database=Labresults;port=3306";

try
{
    builder.Services.AddSingleton(new State(connectionString));
    var app = builder.Build();

app.MapGet("/", LabresultsProject.Results.All);

app.Run();
}
catch (MySqlException e)
{

    Console.WriteLine(e);
}
public record State(string DB);




        








