internal class Program
{
    private static void Main(string[] args)
    {
        var counter = new AreaCounter();
        using (var inputStream = Console.OpenStandardInput())
        {
            var res = counter.Count(inputStream);
            Console.WriteLine(res);
        }
    }
}