# About
This project is an example of how to use [Consul](http://consul.io) in .NET applications further explained in this [blog post](http://michaco.net/blog/ServiceDiscoveryAndHealthChecksInAspNetCoreWithConsul).

## Structure

* Website 
 
   A plain ASP.NET Core MVC application with one page showing some information from the services using DNS service discovery and a simple `HttpClient` calls.

* DataService 

   An ASP.NET Core web service which registers itself in consul and returns some data.
   It also exposes swagger API docs which can be used to generate clients from for example.

## How Does It Work?
The Website's Home Controller>Index action does a DNS lookup, asking Consul for instances of the `DataService`. 
To do DNS lookups, it uses the [DnsClient.NET](http://dnsclient.michaco.net) library.

If a service instance was found, it takes the first result and calls the service via a simple `HttpClient` call.
If mulitple instances were found, the Consul DNS endpoint does load balancing for us...

If you run the website only, there should be no results displayed, but also no errors on the page.

If one instance of the service is running, the website should show results of the service.

If two ore more instances of the service are running, the website should display the list of endpoints and the result of the HttpClient call.
Refreshing the page a few times should show the automatic load balancing by the Consul DNS endpoint (as the results change every other call...)

## Run the Project
To open the solution in Visual Studio, you'll need Visual Studio 2017 as this is the new MSBuild based project system.

You can also install the latest dotnet sdk and just run it via command line 
or call the RunAll.cmd which should (if everything works) download and start consul, start the website and two instances of the DataService.


- go to http://localhost:8500 to see the Consul UI which should show two instances of DataService reporting all green
- go to http://localhost:5000 to inspect the Website, which should show the result of a DNS Lookup and the result of the service call
- go to http://localhost:5200/swagger or http://localhost:5300/swagger to see the swagger UI for the service instance
   
   Via the UI or via curl or postman, you can change the data in each service. The Website should reflect the changes to the data...

Have fun playing with this :>