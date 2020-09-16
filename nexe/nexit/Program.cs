using System.Diagnostics;

namespace nexit
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start("TASKKILL", @"/F /IM ""node*""");
            Process.Start("TASKKILL", @"/F /IM ""nexe*""");
        }
    }
}
