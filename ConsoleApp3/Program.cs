using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using static ConsoleApp3.Program;

namespace ConsoleApp3
{
    internal class Program
    {

        static void Main(string[] args)
        {

            //openClosedPrincipe
            var apple = new Product("Apple", Size.small, Color.green);
            var tree = new Product("Tree", Size.large, Color.green);
            var house = new Product("House", Size.large, Color.blue);
            var product = new Product[] { apple, tree, house };
            var bf = new BetterFilter();
            var largeSpec = new SizeSpecification(Size.large);
            var greenSpec = new ColorSpecification(Color.green);
            var largeGreenSpec = new AndSpecifications<Product>(largeSpec, greenSpec);


            foreach (var item in bf.Filter(product, largeSpec))
            {
                Console.WriteLine(item.Name + item.Size);
            }

            foreach (var item in bf.Filter(product, greenSpec))
            {
                Console.WriteLine(item.Name + item.Color);
            }

            //use kombinator for operators
            foreach (var item in bf.Filter(product, largeGreenSpec))
            {
                Console.WriteLine(item.Name + item.Color + item.Size);
            }

            //use operator
            foreach (var item in bf.Filter(product, largeSpec & greenSpec))
            {
                Console.WriteLine(item.Name + item.Size + item.Color);
            }
        }


    }
}