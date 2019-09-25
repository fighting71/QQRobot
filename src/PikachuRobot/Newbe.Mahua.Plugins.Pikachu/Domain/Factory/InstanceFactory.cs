using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Commond.Tools
{
    /// <summary>
    /// @desc : InstanceFactory  
    /// @author : monster_yj
    /// @create : 2019/9/22 18:10:27 
    /// @source : 
    /// </summary>
    public class InstanceFactory
    {

        private static Dictionary<Type, object> _cache = new Dictionary<Type, object>();

        public static T Get<T>() where T : new()
        {

            var type = typeof(T);

            if (_cache.ContainsKey(type))
            {
                var value = _cache[type];
                if (value != null && value is T t)
                    return t;
            }

            var ret = new T();
            _cache.Add(type, ret);

            return ret;

        }

        public static T Get<T>(Func<T> ctor) 
        {
            var type = typeof(T);

            if (_cache.ContainsKey(type))
            {
                var value = _cache[type];
                if (value != null && value is T t)
                    return t;
            }

            var ret = ctor();
            _cache.Add(type, ret);

            return ret;

        }


    }
}
