# Swagger Example

This is an example project for the code and tips explained in [this blog post](http://michaco.net/blog/TipsForUsingSwaggerAndAutorestInAspNetCoreMvcServices).
In this solution I'm using [NSwag](https://github.com/NSwag/NSwag) to generate swagger.json and the C# client.

## Running the Example
The solution is build with Visual Studio 2017 tooling and the new dotnet core csproj project system.

Start both, the website and the data service either via Visual Studio or commandline `dotnet run`...

The website is running on port 5000 while the service is running on port 5200.

* go to http://localhost:5000/ to see the index page which should show a list of blog posts. 
  The Home controller gets the service client injected via DI.
* go to http://localhost:5200/swagger to see the swagger documentation of the service
