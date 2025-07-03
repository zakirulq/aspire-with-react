using Aspire.Hosting.MongoDB;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

//var mongodb = builder.AddMongoDB("mongodb").WithDataVolume();
//var taskDb = mongodb.AddDatabase("taskDb");

var mongodb = builder.AddMongoDB("mongodb")
    .WithEndpoint(port: 27017, targetPort: 27017, name: "mongodb-external")
    //.WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "user")
    //.WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "user");
    .WithBindMount("./mongodb-data", "/data/db");
    //.WithArgs("--bind_ip_all") // Allow external connections
    //.PublishAsContainer();

// Add MongoDB container with explicit port binding for external access
// var mongodb = builder.AddMongoDB("mongodb")
//     .WithBindMount("./mongodb-data", "/data/db") // Optional: persist data
//     .WithArgs("--bind_ip_all") // Allow external connections
//     .PublishAsContainer() // Make it accessible externally
//     .WithMongoExpress(); // Optional: Adds Mongo Express web UI

// Add database
var taskDb = mongodb.AddDatabase("taskdb");

var apiService = builder.AddProject<Projects.TaskManagementApp_ApiService>("apiservice")
    .WithReference(taskDb);

// builder.AddProject<Projects.TaskManagementApp_Web>("webfrontend")
//     .WithExternalHttpEndpoints()
//     .WithReference(apiService);

// Add the React frontend as a project
var reactFrontend = builder.AddExecutable(
        "react-frontend",
        workingDirectory: "../task-management-client",
        command: "npm",
        args: "start")
    .WithEnvironment("PORT", "3000")
    .WithEnvironment("BROWSER", "none");

builder.Build().Run();
