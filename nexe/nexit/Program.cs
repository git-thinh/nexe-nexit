using System.Diagnostics;

namespace nexit
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start("TASKKILL", @"/F /IM ""node.exe*""");
            Process.Start("TASKKILL", @"/F /IM ""nexe.exe*""");
            Process.Start("TASKKILL", @"/F /IM ""redis-db.exe*""");
        }
    }
}
