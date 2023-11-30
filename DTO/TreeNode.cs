using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TreeNode
    {
        public string Path { get; set; }
        public string Collection { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public int ChildCount { get; set; }
        public IEnumerable<TreeNode> Children { get; set; }
    }
}
