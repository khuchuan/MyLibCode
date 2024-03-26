using ClientConsumerGrpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();


string msg = "Hello";
string url = "http://localhost:7601";

CallGrpc callGrpc = new CallGrpc();
callGrpc.FuncSayHello(msg, url);

string urlGateway = "https://localhost:5001/server-grpc";// "http://localhost:7135/server-grpc";
callGrpc.FuncSayHello(msg,urlGateway);


app.Run();
