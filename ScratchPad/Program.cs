// See https://aka.ms/new-console-template for more information

using Blazorly.ClientApplication.Core;

string connString = "Server=.;Database=DemoApp01;Trusted_Connection=True;";

DBFactory compiler = new DBFactory("MSSQL", connString);

Console.WriteLine("Hello, World!");

