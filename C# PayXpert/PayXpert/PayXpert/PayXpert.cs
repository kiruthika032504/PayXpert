using PayXpert.app;
using PayXpert.util;
using System.Data.SqlClient;

namespace PayXpert
{
    public class PayXpert
    {
        static void Main(string[] args)
        {
            PayXpertApp app = new PayXpertApp();
            app.Run();
        }
    }
}
