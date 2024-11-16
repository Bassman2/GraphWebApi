namespace GraphWebApiDemo
{
    internal class Program
    {

        
        static void Main(string[] args)
        {

            string? apiKey = Environment.GetEnvironmentVariable("GRAPH_APIKEY");

            try
            {
                using var graph = new Graph(apiKey!);

                var user = graph.CurrentUserAsync().Result;



            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }

        }
    }
}
