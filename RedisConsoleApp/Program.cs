using System;
using ServiceStack.Redis;
using ServiceStack.Model;
using System.Diagnostics;

namespace RedisConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IRedisClientsManager redisClientsManager = new PooledRedisClientManager())
            {
                using (var client = redisClientsManager.GetClient())
                {
                    var p = new Person()
                    {
                        name = "tim lv",
                        age = 24
                    };
                    client.Set("author", p);
                    var name = client.Get<Person>("author");
                    Console.WriteLine(name);
                }
            }
            Console.WriteLine("Hello World!");
        }

        //string 类型的set和get
        static void Case1(IRedisClientsManager redisClientsManager)
        {
            using (var client = redisClientsManager.GetClient())
            {
                client.Set("name", "tim lv");
                client.Set("address", "https://github.com/lv-conner/");
                var name = client.Get<string>("name");
                var address = client.Get<string>("address");
                Console.WriteLine(name);
            }
        }
        //单个实体get/set 将会被json序列化后作为字符串存储。
        static void Case2(IRedisClientsManager redisClientsManager)
        {
            using (var client = redisClientsManager.GetClient())
            {
                var p = new Person()
                {
                    name = "tim lv",
                    age = 24
                };
                client.Set("author", p);
                var name = client.Get<Person>("author");
                Console.WriteLine(name);
            }
        }

    }

    class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public override string ToString()
        {
            return name + "\t" +  age.ToString();
        }
    }
}
