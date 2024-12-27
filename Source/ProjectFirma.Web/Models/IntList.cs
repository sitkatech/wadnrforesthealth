using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public class IntList : IEnumerable<int>
    {
        private readonly List<int> _list = new List<int>();

        public IntList(IEnumerable<int> enumerableList)
        {
            Check.EnsureNotNull(enumerableList);
            _list.AddRange(enumerableList);
        }

        public IntList(int value)
        {
            _list.Add(value);
        }

        public bool Contains(int? intToCheckFor)
        {

            return intToCheckFor.HasValue && _list.Contains(intToCheckFor.Value);
        }

        public IntList Deduplicated()
        {
            return new IntList(_list.Distinct());
        }

        public static implicit operator IntList(int obj)
        {
            return new IntList(obj);
        }

        public static implicit operator IntList(HashSet<int> obj)
        {
            return new IntList(obj);
        }

        public static implicit operator IntList(List<int> obj)
        {
            Check.EnsureNotNull(obj);
            return new IntList(obj);
        }

        public static implicit operator List<int>(IntList obj)
        {
            return obj == null ? null : obj._list;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Parse IntList from string
        /// </summary>        
        public static IntList Parse(string intListAsString)
        {
            return new IntList(intListAsString.Split(',').Select(int.Parse));   
        }

        public override string ToString()
        {
            return string.Join(",", this.AsParallel().Distinct());
        }
    }
}