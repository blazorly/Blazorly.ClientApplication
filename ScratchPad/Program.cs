// See https://aka.ms/new-console-template for more information

using Blazorly.ClientApplication.Core;

string connString = "Server=.;Database=DemoApp02;Trusted_Connection=True;";

DBFactory compiler = new DBFactory("MSSQL", connString);
DBDeployer deployer = new DBDeployer(compiler);
deployer.Deploy();

Console.WriteLine("Hello, World!");

