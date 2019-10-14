using Autofac;
using Data.Pikachu;
using Data.Utils;
using Data.Utils.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;


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

    interface IWork
    {
        void Run();
    }

    class A:IWork
    {
        public void Run()
        {
            Console.WriteLine("a work");
        }
    }
    
    class B:IWork
    {
        public void Run()
        {
            Console.WriteLine("b work");
        }
    }
    
    class WorkContainer
    {
        public WorkContainer(IList<IWork> works)
        {
            
        }
    }
    
    /// <summary>
    /// 临时测试类...
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<A>().AsSelf().As<IWork>();
            containerBuilder.RegisterType<B>().AsSelf().As<IWork>();
            containerBuilder.RegisterType<WorkContainer>();

            var container = containerBuilder.Build();

            var workContainer = container.Resolve<WorkContainer>();

            Console.WriteLine("Hello World");

            Console.ReadKey(true);
        }

        public static void TestTimer()
        {
            _ = new Timer(state =>
            {
                Console.WriteLine("test timer~");
            }, null, 60000,-1);

            _ = new Timer(state =>
            {
                Console.WriteLine("test timer 2~");
            }, null, TimeSpan.FromSeconds(60), TimeSpan.Zero);
        }

        /// <summary>
        /// 添加成语字典....
        /// </summary>
        public static void AddIdiomInfo()
        {
            var json = File.ReadAllText(@"G:\dick\download\uc\chinese-xinhua-master\data\idiom.json");

            var list = JsonConvert.DeserializeObject<DicModel[]>(json);

            list = list.Where(u => !string.IsNullOrWhiteSpace(u.word)).ToArray();

            UtilsContext utilsContext = new UtilsContext();

            int start = utilsContext.IdiomInfos.Count(), count = list.Length;
            while (start <= count)
            {

                utilsContext.IdiomInfos.AddRange(list.Skip(start).Take(1000).Select(u =>
                    {
                        var spellArr = u.pinyin.Split(' ');
                        return new IdiomInfo()
                        {
                            Derivation = u.derivation,
                            Example = u.example,
                            Explanation = u.explanation,
                            Spell = u.pinyin,
                            Word = u.word,
                            Abbreviation = u.abbreviation,
                            FirstSpell = spellArr[0],
                            LastSpell = spellArr[spellArr.Length - 1]
                        };
                    }
                ));
                utilsContext.SaveChanges();
                start += 1000;
                Console.WriteLine("添加成功！");
            }

        }

        /// <summary>
        /// 成语解析模型
        /// </summary>
        class DicModel
        {
            /// <summary>
            /// 来源
            /// </summary>
            public string derivation { get; set; }
            /// <summary>
            /// 示例
            /// </summary>
            public string example { get; set; }
            /// <summary>
            /// 解释
            /// </summary>
            public string explanation { get; set; }
            /// <summary>
            /// 拼音
            /// </summary>
            public string pinyin { get; set; }
            /// <summary>
            /// 词语
            /// </summary>
            public string word { get; set; }
            /// <summary>
            /// 首字母组合
            /// </summary>
            public string abbreviation { get; set; }
        }

        public static void DeserGroupMemberA()
        {
            var json =
                "{\"ec\":0,\"errcode\":0,\"em\":\"\",\"adm_num\":0,\"adm_max\":10,\"vecsize\":1,\"0\":0,\"mems\":[{\"uin\":1844867503,\"role\":0,\"flag\":0,\"g\":-1,\"join_time\":1569138527,\"last_speak_time\":1569465871,\"lv\":{\"point\":0,\"level\":1},\"nick\":\".\",\"card\":\"\",\"qage\":7,\"tags\":\"-1\",\"rm\":0},{\"uin\":2758938447,\"role\":2,\"flag\":0,\"g\":-1,\"join_time\":1569139041,\"last_speak_time\":1569465854,\"lv\":{\"point\":0,\"level\":1},\"nick\":\"\\u5c0f\\u9ed1\",\"card\":\"\",\"qage\":0,\"tags\":\"-1\",\"rm\":0},{\"uin\":1036504373,\"role\":2,\"flag\":0,\"g\":0,\"join_time\":1569229350,\"last_speak_time\":1569233318,\"lv\":{\"point\":0,\"level\":1},\"nick\":\"\\uff02\\u7eed\\u5fc3\\u8a00\\u3001\",\"card\":\"\",\"qage\":9,\"tags\":\"-1\",\"rm\":0}]{\"ec\":0,\"errcode\":0,\"em\":\"\",\"adm_num\":0,\"adm_max\":10,\"vecsize\":1,\"0\":0,\"count\":3,\"svr_time\":1569465874,\"max_count\":200,\"search_count\":3}";

            var match = Regex.Match(json, @"^[\s|\S]*(\[[\s|\S]*\])[\s|\r|\n]*([\s|\S]*)$");

            if (match.Success)
            {
                var members = match.Groups[1].Value;
                var groupInfo = match.Groups[2].Value;
            }
        }

        public static void DeserGroupMemberB()
        {

            var json = "_GroupMember_Callback({\"code\":0,\"data\":{\"alpha\":0,\"bbscount\":0,\"class\":10012,\"create_time\":1569138527,\"filecount\":0,\"finger_memo\":\"\",\"group_memo\":\"\",\"group_name\":\"PikachuRobot\",\"item\":[{\"iscreator\":1,\"ismanager\":0,\"nick\":\".\",\"uin\":1844867503},{\"iscreator\":0,\"ismanager\":0,\"nick\":\"小黑\",\"uin\":2758938447}],\"level\":0,\"nick\":\"小黑\",\"option\":2,\"total\":3},\"default\":0,\"message\":\"\",\"subcode\":0});";

            var match = Regex.Match(json, @"^_GroupMember_Callback\(([\s|\S]*)\);$");

            if (match.Success)
            {
                var info = match.Groups[1].Value;

            }
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

            var listKey = "empty.test.list";

            db.ListLeftPush(listKey, "test");
            db.ListLeftPush(listKey, "test2");

            string test = db.ListLeftPop(listKey);
            while (!string.IsNullOrWhiteSpace(test))
            {
                Console.WriteLine(test);
                test = db.ListLeftPop(listKey);
            }

            Console.WriteLine(test);

        }

        public static void TestDb()
        {

            PikachuDataContext context = new PikachuDataContext();

            context.Database.CreateIfNotExists();

            var dbCommand = context.Database.Connection.CreateCommand();
            
            context.SaveChanges();

        }
    }
}