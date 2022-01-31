using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ConsoleApp
    {
        public ConsoleApp()
        {

        }

        public async Task Run()
        {
            try
            {
                Console.WriteLine("Hey There!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            await Task.CompletedTask;
        }
    }
}