using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PetSystem;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:21:08
    /// @source : 
    /// @des : 宠物系统
    /// </summary>
    public class PetDeal : IGroupMsgDeal
    {

        public PetDeal(PetService petService)
        {
            PetService = petService;
        }

        public PetService PetService { get; }

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            if(Regex.IsMatch(context.Message, @"^[\s|\n|\r]*宠物系统[\s|\n|\r]*$"))
            {
                return @"当前宠物系统支持:
    [宠物商店] [宠物道具商店] [查看宠物] [放养宠物]
";
            }

            if(Regex.IsMatch(context.Message, @"^[\s|\n|\r]*宠物商店[\s|\n|\r]*$"))
            {
                var list = PetService.GetAll().OrderByDescending(u => u.Id).ToList();

                if(list.Count == 0)
                {
                    return "宠物商店还没有入驻的宠物呢,请联系管理员添加吧!";
                }

                StringBuilder builder = new StringBuilder();

                builder.AppendLine(" [宠物介绍图] [宠物名称] [宠物描述]");

                for (int i = 0; i < list.Count; i++)
                {
                    builder.AppendLine($"{(i+1).ToString()}. {list[i].Face}  {list[i].Name}  [{list[i].Description}]");
                }

                builder.AppendLine();
                builder.AppendLine(" 试试对我说 [领养宠物] [宠物名称] 吧~");

                return builder.ToString();

            }

            return string.Empty;

        }

    }
}
