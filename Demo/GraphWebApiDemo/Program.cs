namespace GraphWebApiDemo;

internal class Program
{
    static void Main(string[] _)
    {

        //string? apiKey = Environment.GetEnvironmentVariable("GRAPH_APIKEY");

        try
        {
            using var graph = new Graph("DemoApp", Private.Login, Private.Password);

            var user = graph.CurrentUserAsync().Result;



        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message);
        }

    }
}
