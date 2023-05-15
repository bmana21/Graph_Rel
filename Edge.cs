using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace Graph_Rel
{
    class Edge
    {
       public int x, y;
       public Point A, B;
       public int length = 3, is_done = 0;
       public Edge(int First,int Second)
       {
           x = First;
           y = Second;
       }
       public void update(Point S, Point F)
       {
           A = S;
           B = F;
           if (is_done == 1)
               length = Convert.ToInt32(dist(A, B));
       }
       public double dist(Point A, Point B)
       {
           double ans =(Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y)));
            return ans;
       }
       public pair<Point, Point> get_line()
       {
           Point S = A;
           double sin = (B.Y - A.Y) / dist(A, B);
           double cos = (B.X - A.X) / dist(A, B);
           Point F = new Point(S.X+Convert.ToInt32(length * cos), S.Y+Convert.ToInt32(length * sin));
           return new pair<Point, Point>(S, F);
       }
    }
}
