using Autofac;
using Data.PetSystem;
using Data.Pikachu;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public delegate string Deal(int num);

    public class InstanceFactory<T> where T : class
    {
        private static readonly ThreadLocal<T> threadLocal = new ThreadLocal<T>();

        public static T Get(Func<T> func)
        {
            return threadLocal.Value = threadLocal.Value ?? func();
        }
    }

    class Program
    {

        public Program()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId.ToString()} 构建了一个Program");
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            Console.ReadKey(true);
        }

        public static void TestThreadLocal()
        {
            InstanceFactory<Program>.Get((() => new Program()));

            ThreadPool.QueueUserWorkItem((state =>
            {
                InstanceFactory<Program>.Get((() => new Program()));
            }));
            ThreadPool.QueueUserWorkItem((state =>
            {
                InstanceFactory<Program>.Get((() => new Program()));
            }));
            ThreadPool.QueueUserWorkItem((state =>
            {
                InstanceFactory<Program>.Get((() => new Program()));
            }));
            
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
            InstanceFactory<Program>.Get((() => new Program()));
        }
        
        public static void TestThreadLocal2()
        {
            InstanceFactory<Program>.Get((() => new Program()));
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