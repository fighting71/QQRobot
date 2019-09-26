using Newbe.Mahua.Apis;
using Newbe.Mahua.NativeApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaApis.Mpq
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 9:59:43
    /// @source : 
    /// @des : 
    /// </summary>
    public class GetGroupMemebersWithModelApiMahuaCommandHandler : MpqApiMahuaCommandHandlerBase<GetGroupMemebersWithModelApiMahuaCommand, GetGroupMemebersWithModelApiMahuaCommandResult>
    {
        public GetGroupMemebersWithModelApiMahuaCommandHandler(
          IMpqApi mpqApi,
          IRobotSessionContext robotSessionContext,
          IEventFunOutput eventFunOutput)
          : base(mpqApi, robotSessionContext, eventFunOutput)
        {
        }

        public override GetGroupMemebersWithModelApiMahuaCommandResult Handle(
          GetGroupMemebersWithModelApiMahuaCommand message)
        {

            // "_GroupMember_Callback({\"code\":0,\"data\":{\"alpha\":0,\"bbscount\":0,\"class\":10012,\"create_time\":1569138527,\"filecount\":0,\"finger_memo\":\"\",\"group_memo\":\"\",\"group_name\":\"PikachuRobot\",\"item\":[{\"iscreator\":1,\"ismanager\":0,\"nick\":\".\",\"uin\":1844867503},{\"iscreator\":0,\"ismanager\":0,\"nick\":\"小黑\",\"uin\":2758938447}],\"level\":0,\"nick\":\"小黑\",\"option\":2,\"total\":3},\"default\":0,\"message\":\"\",\"subcode\":0});"
            //string info = MpqApi.Api_GetGroupMemberB(CurrentQq, message.ToGroup);

            // "{\"ec\":0,\"errcode\":0,\"em\":\"\",\"adm_num\":0,\"adm_max\":10,\"vecsize\":1,\"0\":0,\"mems\":[{\"uin\":1844867503,\"role\":0,\"flag\":0,\"g\":-1,\"join_time\":1569138527,\"last_speak_time\":1569465871,\"lv\":{\"point\":0,\"level\":1},\"nick\":\".\",\"card\":\"\",\"qage\":7,\"tags\":\"-1\",\"rm\":0},{\"uin\":2758938447,\"role\":2,\"flag\":0,\"g\":-1,\"join_time\":1569139041,\"last_speak_time\":1569465854,\"lv\":{\"point\":0,\"level\":1},\"nick\":\"\\u5c0f\\u9ed1\",\"card\":\"\",\"qage\":0,\"tags\":\"-1\",\"rm\":0},{\"uin\":1036504373,\"role\":2,\"flag\":0,\"g\":0,\"join_time\":1569229350,\"last_speak_time\":1569233318,\"lv\":{\"point\":0,\"level\":1},\"nick\":\"\\uff02\\u7eed\\u5fc3\\u8a00\\u3001\",\"card\":\"\",\"qage\":9,\"tags\":\"-1\",\"rm\":0}]{\"ec\":0,\"errcode\":0,\"em\":\"\",\"adm_num\":0,\"adm_max\":10,\"vecsize\":1,\"0\":0,\"count\":3,\"svr_time\":1569465874,\"max_count\":200,\"search_count\":3}"; 
            // bug 返回数据格式异常 且群主role值为0(普通)
            string groupMemberA = this.MpqApi.Api_GetGroupMemberA(this.CurrentQq, message.ToGroup);
            if (string.IsNullOrEmpty(groupMemberA))
                return new GetGroupMemebersWithModelApiMahuaCommandResult()
                {
                    ModelWithSourceString = new ModelWithSourceString<IEnumerable<GroupMemberInfo>>()
                    {
                        SourceString = groupMemberA,
                        Model = Enumerable.Empty<GroupMemberInfo>()
                    }
                };

            GroupMemberInfoListJson memberInfoListJson = MpqHelper.DeserGroupMemberJsonA(groupMemberA);

            ModelWithSourceString<IEnumerable<GroupMemberInfo>> withSourceString = new ModelWithSourceString<IEnumerable<GroupMemberInfo>>()
            {
                SourceString = groupMemberA,
                Model = ((IEnumerable<Mem>)memberInfoListJson.Mems).Select((x => new GroupMemberInfo()
                {
                    Group = message.ToGroup,
                    Age = 0,
                    Area = string.Empty,
                    Authority = this.GetGroupMemberAuthority(x.Role),
                    CanModifyInGroupName = false,
                    Gender = this.GetGender(x.G),
                    HasBadRecord = false,
                    InGroupName = x.Card,
                    JoinTime = Clock.ConvertSecondsToDateTime((long)x.Join_time),
                    LastSpeakingTime = Clock.ConvertSecondsToDateTime((long)x.Last_speak_time),
                    Level = x.Lv.Level.ToString(),
                    NickName = x.Nick,
                    Qq = x.Uin.ToString(),
                    SpecialTitle = string.Empty,
                    TitleExpirationTime = TimeSpan.MinValue
                })).ToArray()
            };
            return new GetGroupMemebersWithModelApiMahuaCommandResult()
            {
                ModelWithSourceString = withSourceString
            };
        }

        private GroupMemberAuthority GetGroupMemberAuthority(int role)
        {
            switch (role)
            {
                case 2:
                    return GroupMemberAuthority.Manager;
                case 3:
                    return GroupMemberAuthority.Leader;
                default:
                    return GroupMemberAuthority.Normal;
            }
        }

        private Gender GetGender(GroupMemberSex? gender)
        {
            GroupMemberSex? nullable = gender;
            if (nullable.HasValue)
            {
                switch (nullable.GetValueOrDefault())
                {
                    case GroupMemberSex.Male:
                        return Gender.Male;
                    case GroupMemberSex.Female:
                        return Gender.Female;
                }
            }
            return Gender.Unknow;
        }

    }
}
