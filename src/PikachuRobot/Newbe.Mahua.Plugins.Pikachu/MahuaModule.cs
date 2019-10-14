using Autofac;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusModule;

namespace Newbe.Mahua.Plugins.Pikachu
{
    /// <summary>
    /// Ioc容器注册
    /// </summary>
    public class MahuaModule : IMahuaModule
    {

        public Module[] GetModules()
        {
            // 可以按照功能模块进行划分，此处可以改造为基于文件配置进行构造。实现模块化编程。
            return new Module[]
            {
                new PluginModule(),
                new CommandApiModule(), 
                new CommandEventModule(), 
                new JobModule(),
                new InitModule(),
                new MpqModule(), 

            };
        }
    }
}