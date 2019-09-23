using Autofac;
using Data.Pikachu;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTest
{

    public delegate string Deal(int num);

    class Program
    {
        static void Main(string[] args)
        {
            TestCache();

            Console.WriteLine("Hello World");

            Console.ReadKey(true);

        }

        public static void TestAutoFac()
        {

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<SolutionDeal>();

            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");

            var db = connectionMultiplexer.GetDatabase();

            builder.Register(u => db);

            PikachuDataContext context = new PikachuDataContext();

            builder.Register(u => context);

            var info = builder.Build();

            var test = info.Resolve<SolutionDeal>();

        }

        public static void TestDeletageLink()
        {

            Deal deal = a => a.ToString();

            deal += a => (a * 10).ToString();

            foreach (var item in deal.GetInvocationList())
            {
                Console.WriteLine((item as Deal).Invoke(10));
            }
        }

        public static void TestRegex()
        {
            var match = Regex.Match("禁用配置 astest", @"[\s|\n|\r]*禁用配置[\s|\n|\r]*(.*)[\s|\n|\r]*");

            foreach (Group name in match.Groups)
            {
                Console.WriteLine(name.Value);
            }
        }

        public static void TestCache()
        {
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");

            var db = connectionMultiplexer.GetDatabase();

            db.KeyDelete("console.test");

            if (db.StringSet("console.test", "safjidaosjf", TimeSpan.FromSeconds(10)))
            {
                Console.WriteLine("redis 添加成功");

                var info = db.StringGet("console.test");

                Console.WriteLine($"redis 读取数据:{info}");
            }
        }

        public static void TestDb()
        {

            PikachuDataContext context = new PikachuDataContext();

            context.Database.CreateIfNotExists();

            context.SaveChanges();

        }

    }
}
