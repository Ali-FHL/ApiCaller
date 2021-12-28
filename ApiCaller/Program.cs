using System;
using System.Collections.Generic;

namespace ApiCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            // var model = Authorization.GetToken().Result;

            var person = new Person("1", "ali", "fallah", "2162718685", "1001");
            var people = new List<Person>();
            people.Add(person);

            var model = Authorization.Post<dynamic>("url", people);

            Console.WriteLine(model);
            Console.Read();
        }
    }
}
