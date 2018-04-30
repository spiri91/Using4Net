using System;
using Using4Net;

namespace Using4Net_POC
{
    class MySpecialClass : IDisposable
    {
        public void WriteSomething()
        {
            Console.WriteLine("Something");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new MySpecialClass();
            var b = new MySpecialClass();
            var c = new MySpecialClass();

            While.Using(new MySpecialClass[] { a, b, c }, () => { a.WriteSomething(); b.WriteSomething(); c.WriteSomething(); });

            Console.ReadKey();
        }
    }
}
