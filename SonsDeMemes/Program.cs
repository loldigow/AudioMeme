namespace SonsDeMemes
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var contextpage = new AplicacaoSons();
            Application.Run(contextpage);
        }
    }
}