// See https://aka.ms/new-console-template for more information

using Blazorly.ClientApplication.Core;
using Blazorly.ClientApplication.Core.Deployers;

string connString = "Server=.;Database=DemoApp02;Trusted_Connection=True;";

DBFactory compiler = new DBFactory("MSSQL", connString);
var deployer = new MSSQLDBDeployer(connString);
deployer.Deploy();

Console.WriteLine("Hello, World!");

