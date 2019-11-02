using System;

namespace NissanCoupon
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.ServerManager.ServerInit();
            Console.WriteLine("Nissan coupon server has inited success");
            Console.ReadKey(true);
        }
    }
}
