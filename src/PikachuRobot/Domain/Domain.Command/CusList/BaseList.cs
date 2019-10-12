using System.Collections.Generic;

namespace Domain.Command.CusList
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 13:47:25
    /// @source : 
    /// @des : 
    /// </summary>
    public class BaseList<T>
    {

        protected IList<string> flagList = new List<string>();

        protected IList<T> list = new List<T>();

        public BaseList<T> AddDeal(T deal,string flag)
        {
            list.Add(deal);
            flagList.Add(flag);
            return this;
        }

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
