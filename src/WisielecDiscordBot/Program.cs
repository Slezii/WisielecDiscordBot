namespace WisielecDiscordBot
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var boostrapper = new Boostrapper();
            await boostrapper.RunAsync();
        }
    }
}
