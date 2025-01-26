namespace GraphWebApiDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using var graph = new Graph("graph", "GraphWebApiDemo");

                var user = graph.CurrentUserAsync().Result;



            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }

        }
    }
}
