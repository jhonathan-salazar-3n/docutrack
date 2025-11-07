using DocuTrack.Controllers;
using DocuTrack.Views;

namespace DocuTrack
{
    static class Program
    {
        static void Main(string[] args)
        {
            var view = new ConsoleView();
            var controller = new TreeController(view);
            controller.RunAllUseCases();
        }
    }
}