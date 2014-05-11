using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huff
{
    public class Node
    {
        public int Symbol { get; set; }
        public int Count { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
