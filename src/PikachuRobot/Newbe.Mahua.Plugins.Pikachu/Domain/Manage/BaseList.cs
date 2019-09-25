using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 13:47:25
    /// @source : 
    /// @des : 
    /// </summary>
    public class BaseList<T>
    {
        
        protected IList<T> list = new List<T>();

        public BaseList<T> AddDeal(T deal)
        {
            list.Add(deal);
            return this;
        }

        public void AddDeal(int index, T deal)
        {
            list.Insert(index, deal);
        }

    }
}
