using Python.Runtime;
using System;

namespace DockerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var state = Py.GIL();
            var sys = Py.Import("sys");
            var np = Py.Import("numpy");
        }
    }
}
