using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaApis.Mpq
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 11:30:51
    /// @source : 
    /// @des : 
    /// </summary>
    public class MpqHelper
    {

        /// <summary>
        /// 解析json字符串
        /// @demo : "{\"ec\":0,\"errcode\":0,\"em\":\"\",\"adm_num\":0,\"adm_max\":10,\"vecsize\":1,\"0\":0,\"mems\":[{\"uin\":1844867503,\"role\":0,\"flag\":0,\"g\":-1,\"join_time\":1569138527,\"last_speak_time\":1569465871,\"lv\":{\"point\":0,\"level\":1},\"nick\":\".\",\"card\":\"\",\"qage\":7,\"tags\":\"-1\",\"rm\":0},{\"uin\":2758938447,\"role\":2,\"flag\":0,\"g\":-1,\"join_time\":1569139041,\"last_speak_time\":1569465854,\"lv\":{\"point\":0,\"level\":1},\"nick\":\"\\u5c0f\\u9ed1\",\"card\":\"\",\"qage\":0,\"tags\":\"-1\",\"rm\":0},{\"uin\":1036504373,\"role\":2,\"flag\":0,\"g\":0,\"join_time\":1569229350,\"last_speak_time\":1569233318,\"lv\":{\"point\":0,\"level\":1},\"nick\":\"\\uff02\\u7eed\\u5fc3\\u8a00\\u3001\",\"card\":\"\",\"qage\":9,\"tags\":\"-1\",\"rm\":0}]{\"ec\":0,\"errcode\":0,\"em\":\"\",\"adm_num\":0,\"adm_max\":10,\"vecsize\":1,\"0\":0,\"count\":3,\"svr_time\":1569465874,\"max_count\":200,\"search_count\":3}";
        /// </summary>
        /// <param name="json"></param>
        public static GroupMemberInfoListJson DeserGroupMemberJsonA(string json)
        {

            var match = Regex.Match(json, @"^[\s|\S]*(\[[\s|\S]*\])[\s|\r|\n]*([\s|\S]*)$");

            if (match.Success)
            {
                var groupInfo = JsonConvert.DeserializeObject<GroupMemberInfoListJson>(match.Groups[2].Value);
                var members = JsonConvert.DeserializeObject<Mem[]>(match.Groups[1].Value);

                groupInfo.Mems = members;
                return groupInfo;
            }

            return null;

        }

    }

    #region groupMebereA 相关实体


    public class GroupMemberInfoListJson
    {
        public int Adm_max { get; set; }

        public int Adm_num { get; set; }

        public int Count { get; set; }

        public int Ec { get; set; }

        public Levelname Levelname { get; set; }

        public int Max_count { get; set; }

        public Mem[] Mems { get; set; }

        public int Search_count { get; set; }

        public int Svr_time { get; set; }

        public int Vecsize { get; set; }
    }

    public class Levelname : Dictionary<string, string>
    {
    }

    public enum GroupMemberSex
    {
        Male = 0,
        Female = 1,
        NotSetted = 255, // 0x000000FF
    }

    public class Mem
    {
        public string Card { get; set; }

        public int Flag { get; set; }

        public GroupMemberSex? G { get; set; }

        public int Join_time { get; set; }

        public int Last_speak_time { get; set; }

        public Lv Lv { get; set; }

        public string Nick { get; set; }

        public int Qage { get; set; }

        public int Role { get; set; }

        public string Tags { get; set; }

        public long Uin { get; set; }
    }

    public class Lv
    {
        public int Level { get; set; }

        public int Point { get; set; }
    }

    #endregion

}
