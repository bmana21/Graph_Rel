using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace Graph_Rel
{
    class Node
    {
        public int x=-10, y=-10, r = 1,max_r=36;
        public int num;
        public int fx = 0, fy = 0;
        public Color col = Color.Black;
        public Node()
        {
        }
        public Node(int location_x, int location_y)
        {
            x = location_x;
            y = location_y;

        }
        public void update()
        {
            if (r < max_r)
                r+=2;
        }
        public int dist(Point A, Point B)
        {
            int ans = Convert.ToInt32(Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y)));
            return ans;
        }

        public bool check_click(Point A,int B)
        {
            if (B == 0)
            {
                if (dist(A, new Point(x, y)) <= max_r)
                    return true;
                return false;
            }
            if (B == 1)
            {
                if (dist(A, new Point(x, y)) <= max_r/2)
                    return true;
                return false;
            }
            return false;
        }
    }
}
