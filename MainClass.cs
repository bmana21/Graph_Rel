using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
using System.IO;

namespace Graph_Rel
{
    class MainClass
    {
        //--Variables------------
        static Form fm;
        static Panel pn;
        static List<Node> nodes;
        static Timer timer;
        static Button add_but,remove_but,edge_remove_but,edge_add_but,move,DFS,BFS,SH_DFS,SH_BFS,SH_DIK;
        static Font in_f;
        static int which_but = 0,mx_num=1,d1=-1,d2=-1, is_down, px,py, is_d=0,d_p=0,lng=0,p_dr=0;
        static List<int> nums;
        static List<Edge> edges;
        static int[] has;
        static Pen pen,dpen,ppen;
        static Graph graph;
        static int[,] daf;
        static List<pair<int, int>> path,p;
        //-----------------------

        static void Create_Win()
        {

            //--fm-----------------
            fm = new Form();
            fm.BackColor = Color.Purple;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();

            //--pn-----------------
            pn = new Panel();
            pn.BackColor = Color.White;
            pn.Left = 3;
            pn.Top = 60;
            pn.Width = fm.Width - pn.Left - 20;
            pn.Height = fm.Height - pn.Top - 42;
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pn, new object[] { true }); 
            fm.Controls.Add(pn);
            pn.MouseClick += new MouseEventHandler(pn_MouseClick);
            pn.Paint += new PaintEventHandler(pn_Paint);
            pn.MouseDown += new MouseEventHandler(pn_MouseDown);
            pn.MouseUp += new MouseEventHandler(pn_MouseUp);
            pn.MouseMove += new MouseEventHandler(pn_MouseMove);

            //--nodes--------------
            nodes = new List<Node>();

            //--timer--------------
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);

            //--in_f--out_f---------
            in_f = new Font("Lato", 13, FontStyle.Regular);

            //--add_but------------
            add_but = new Button();
            add_but.Left = 3;
            add_but.Top = 3;
            add_but.Width = 100;
            add_but.Height = pn.Top - add_but.Top - 3;
            add_but.FlatStyle = FlatStyle.Flat;
            add_but.Text = "Add Node";
            add_but.MouseEnter += new EventHandler(add_but_MouseEnter);
            add_but.MouseLeave += new EventHandler(add_but_MouseLeave);
            add_but.Click += new EventHandler(add_but_Click);
            add_but.Font = in_f;
            add_but.ForeColor = Color.White;
            add_but.FlatAppearance.BorderSize = 1;
            fm.Controls.Add(add_but);

            //--edge_add_but--------
            edge_add_but = new Button();
            edge_add_but.Left = 9+(add_but.Width*2);
            edge_add_but.Top = 3;
            edge_add_but.Width = 100;
            edge_add_but.Height = pn.Top - add_but.Top - 3;
            edge_add_but.FlatStyle = FlatStyle.Flat;
            edge_add_but.Text = "Add Edge";
            edge_add_but.MouseEnter += new EventHandler(add_but_MouseEnter);
            edge_add_but.MouseLeave += new EventHandler(add_but_MouseLeave);
            edge_add_but.Click += new EventHandler(add_but_Click);
            edge_add_but.Font = in_f;
            edge_add_but.ForeColor = Color.White;
            edge_add_but.FlatAppearance.BorderSize = 0;
            fm.Controls.Add(edge_add_but);

            //--remove_but---------
            remove_but = new Button();
            remove_but.Left = 6 + add_but.Width;
            remove_but.Top = 3;
            remove_but.Width = 100;
            remove_but.Height = pn.Top - remove_but.Top - 3;
            remove_but.FlatStyle = FlatStyle.Flat;
            remove_but.Text = "Remove Node";
            remove_but.Font = in_f;
            remove_but.ForeColor = Color.White;
            remove_but.FlatAppearance.BorderSize = 0;
            remove_but.MouseEnter += new EventHandler(add_but_MouseEnter);
            remove_but.MouseLeave += new EventHandler(add_but_MouseLeave);
            remove_but.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(remove_but);

            //--edge_remove_but---------
            edge_remove_but = new Button();
            edge_remove_but.Left = 12 + (3*add_but.Width);
            edge_remove_but.Top = 3;
            edge_remove_but.Width = 100;
            edge_remove_but.Height = pn.Top - remove_but.Top - 3;
            edge_remove_but.FlatStyle = FlatStyle.Flat;
            edge_remove_but.Text = "Remove Edge";
            edge_remove_but.Font = in_f;
            edge_remove_but.ForeColor = Color.White;
            edge_remove_but.FlatAppearance.BorderSize = 0;
            edge_remove_but.MouseEnter += new EventHandler(add_but_MouseEnter);
            edge_remove_but.MouseLeave += new EventHandler(add_but_MouseLeave);
            edge_remove_but.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(edge_remove_but);

            //--move---------
            move = new Button();
            move.Left = 15 + (4 * add_but.Width);
            move.Top = 3;
            move.Width = 100;
            move.Height = pn.Top - remove_but.Top - 3;
            move.FlatStyle = FlatStyle.Flat;
            move.Text = "Move Nodes";
            move.Font = in_f;
            move.ForeColor = Color.White;
            move.FlatAppearance.BorderSize = 0;
            move.MouseEnter += new EventHandler(add_but_MouseEnter);
            move.MouseLeave += new EventHandler(add_but_MouseLeave);
            move.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(move);

            //-DFS----------
            DFS = new Button();
            DFS.Left = 90 + (5 * add_but.Width);
            DFS.Top = 3;
            DFS.Width = 100;
            DFS.Height = pn.Top - remove_but.Top - 3;
            DFS.FlatStyle = FlatStyle.Flat;
            DFS.Text = "Run DFS";
            DFS.Font = in_f;
            DFS.ForeColor = Color.White;
            DFS.FlatAppearance.BorderSize = 0;
            DFS.MouseEnter += new EventHandler(add_but_MouseEnter);
            DFS.MouseLeave += new EventHandler(add_but_MouseLeave);
            DFS.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(DFS);


            //--BFS----------
            BFS = new Button();
            BFS.Left = 93 + (6 * add_but.Width);
            BFS.Top = 3;
            BFS.Width = 100;
            BFS.Height = pn.Top - remove_but.Top - 3;
            BFS.FlatStyle = FlatStyle.Flat;
            BFS.Text = "Run BFS";
            BFS.Font = in_f;
            BFS.ForeColor = Color.White;
            BFS.FlatAppearance.BorderSize = 0;
            BFS.MouseEnter += new EventHandler(add_but_MouseEnter);
            BFS.MouseLeave += new EventHandler(add_but_MouseLeave);
            BFS.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(BFS);

            //--SH_DFS-------
            SH_DFS = new Button();
            SH_DFS.Left = 96 + (8 * add_but.Width);
            SH_DFS.Top = 3;
            SH_DFS.Width = 100;
            SH_DFS.Height = pn.Top - remove_but.Top - 3;
            SH_DFS.FlatStyle = FlatStyle.Flat;
            SH_DFS.Text = "Shortest Path DFS";
            SH_DFS.Font = in_f;
            SH_DFS.ForeColor = Color.White;
            SH_DFS.FlatAppearance.BorderSize = 0;
            SH_DFS.MouseEnter += new EventHandler(add_but_MouseEnter);
            SH_DFS.MouseLeave += new EventHandler(add_but_MouseLeave);
            SH_DFS.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(SH_DFS);

            //--SH_BFS-------
            SH_BFS = new Button();
            SH_BFS.Left = 99 + (9 * add_but.Width);
            SH_BFS.Top = 3;
            SH_BFS.Width = 100;
            SH_BFS.Height = pn.Top - remove_but.Top - 3;
            SH_BFS.FlatStyle = FlatStyle.Flat;
            SH_BFS.Text = "Shortest Path BFS";
            SH_BFS.Font = in_f;
            SH_BFS.ForeColor = Color.White;
            SH_BFS.FlatAppearance.BorderSize = 0;
            SH_BFS.MouseEnter += new EventHandler(add_but_MouseEnter);
            SH_BFS.MouseLeave += new EventHandler(add_but_MouseLeave);
            SH_BFS.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(SH_BFS);

            //--SH_DIK-------
            SH_DIK = new Button();
            SH_DIK.Left = 102 + (10 * add_but.Width);
            SH_DIK.Top = 3;
            SH_DIK.Width = 100;
            SH_DIK.Height = pn.Top - remove_but.Top - 3;
            SH_DIK.FlatStyle = FlatStyle.Flat;
            SH_DIK.Text = "Dijkstra";
            SH_DIK.Font = in_f;
            SH_DIK.ForeColor = Color.White;
            SH_DIK.FlatAppearance.BorderSize = 0;
            SH_DIK.MouseEnter += new EventHandler(add_but_MouseEnter);
            SH_DIK.MouseLeave += new EventHandler(add_but_MouseLeave);
            SH_DIK.Click += new EventHandler(add_but_Click);
            fm.Controls.Add(SH_DIK);

            //--nums----------------
            nums = new List<int>();

            //--edges---------------
            edges = new List<Edge>();

            //--has-----------------
            has = new int[100];

            //-pen------------------
            pen = new Pen(Color.Black, 6);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //-pen------------------
            dpen = new Pen(Color.DodgerBlue, 7);
            dpen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;

            //-pen------------------
            ppen = new Pen(Color.LimeGreen, 7);
            ppen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;

            //-g--------------------
            graph = new Graph();
            graph.new_graph();
            
            //-daf------------------
            daf = new int[1001, 1001];

            //-path-----------------
            path = new List<pair<int, int>>();
            
        }
        static void ref_cols()
        {
            for (int k = 0; k < nodes.Count; k++)
                nodes[k].col = Color.Black;
        }
        static void pn_MouseMove(object sender, MouseEventArgs e)
        {
            if (is_down == 1 && which_but == 4)
            {
                if (d1 != -1)
                {
                    nodes[d1].x += (e.X - px);
                    nodes[d1].y += (e.Y - py);
                    nodes[d1].fx += (e.X - px);
                    nodes[d1].fy += (e.Y - py);
                    for (int k = 0; k < edges.Count; k++)
                    {
                        if (edges[k].x == d1)
                        {
                            edges[k].update(new Point(nodes[d1].x, nodes[d1].y), edges[k].B);
                        }
                        if (edges[k].y == d1)
                        {
                            edges[k].update( edges[k].A,new Point(nodes[d1].x, nodes[d1].y));
                        }
                    }
                    px = e.X;
                    py = e.Y;
                }
            }
        }

        static void pn_MouseUp(object sender, MouseEventArgs e)
        {
            is_down = 0;
        }

        static void pn_MouseDown(object sender, MouseEventArgs e)
        {
            if (which_but == 4)
            {
                is_down = 1;
                px = e.X;
                py = e.Y;
                int pos = -1;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 1) == true)
                    {
                        pos = k;
                    }
                }
                d1 = pos;
            }
        }

        static void add_but_Click(object sender, EventArgs e)
        {
            if (is_d == 1)
               return;
                ref_cols();
                path = new List<pair<int, int>>();
                refresh_d1_d2();

                Button but = sender as Button;
                add_but.FlatAppearance.BorderSize = 0;
                remove_but.FlatAppearance.BorderSize = 0;
                edge_add_but.FlatAppearance.BorderSize = 0;
                edge_remove_but.FlatAppearance.BorderSize = 0;
                move.FlatAppearance.BorderSize = 0;
                DFS.FlatAppearance.BorderSize = 0;
                BFS.FlatAppearance.BorderSize = 0;
                SH_DFS.FlatAppearance.BorderSize = 0;
                SH_BFS.FlatAppearance.BorderSize = 0;
                SH_DIK.FlatAppearance.BorderSize = 0;
                but.FlatAppearance.BorderSize = 1;

                if (but == add_but)
                    which_but = 0;
                if (but == remove_but)
                    which_but = 1;
                if (but == edge_add_but)
                    which_but = 2;
                if (but == edge_remove_but)
                    which_but = 3;
                if (but == move)
                    which_but = 4;
                if (but == DFS)
                    which_but = 5;
                if (but == BFS)
                    which_but = 6;
                if (but == SH_DFS)
                    which_but = 7;
                if (but == SH_BFS)
                    which_but = 8;
                if (but == SH_DIK)
                    which_but = 9;
        }

        static void add_but_MouseLeave(object sender, EventArgs e)
        {
            Button but = sender as Button;
            but.BackColor = Color.Purple;
            but.ForeColor = Color.White;
            but.Font = in_f;
        }

        static void add_but_MouseEnter(object sender, EventArgs e)
        {
            Button but = sender as Button;
            but.BackColor = Color.White;
            but.ForeColor = Color.Purple;
            but.Font = in_f;
        }

        static void pn_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int k = 0; k < edges.Count; k++)
            {
                pair<Point,Point> line=edges[k].get_line();
                gr.DrawLine(pen, line.X, line.Y);
                if (edges[k].is_done==0 && edges[k].dist(edges[k].A, edges[k].B) >= edges[k].length)
                    edges[k].length += 5;
                else edges[k].is_done = 1;
            }
            if (is_d == 1)
            {
                pair<int, int> gt = p[d_p];
                if (gt.X == -1)
                {
                    p_dr = 1;
                    d_p++;
                    gt = p[d_p];
                    for (int k = 0; k < nodes.Count; k++)
                        if (nodes[k].col == Color.Blue)
                            nodes[k].col = Color.Black;
                }
                
                    if (gt.Y == -1)
                    {
                        if (p_dr == 0)
                            nodes[gt.X].col = Color.Blue;
                        else nodes[gt.X].col = Color.Green;
                        d_p++;

                    }
                    else
                    {
                        int f = gt.X, s = gt.Y;
                        double cos = (nodes[s].x - nodes[f].x) / dist(new Point(nodes[f].x, nodes[f].y), new Point(nodes[s].x, nodes[s].y));
                        double sin = (nodes[s].y - nodes[f].y) / dist(new Point(nodes[f].x, nodes[f].y), new Point(nodes[s].x, nodes[s].y));
                        Point F = new Point(nodes[f].x + Convert.ToInt32(lng * cos), nodes[f].y + Convert.ToInt32(lng * sin));
                        if(p_dr==0)
                        gr.DrawLine(dpen, nodes[f].x, nodes[f].y, F.X, F.Y);
                        else gr.DrawLine(ppen, nodes[f].x, nodes[f].y, F.X, F.Y);
                        lng += 5;
                        if (lng >= dist(new Point(nodes[f].x, nodes[f].y), new Point(nodes[s].x, nodes[s].y)))
                        {
                            if(p_dr==1)
                            path.Add(new pair<int,int>(f,s));
                            d_p++;
                            lng = 0;

                        }
                    }
                    if (d_p >= p.Count)
                    {
                       
                        is_d = 0;
                        p_dr = 0;
                    }
                   
            }
            for (int k = 0; k < path.Count; k++)
            {
                gr.DrawLine(ppen, nodes[path[k].X].x, nodes[path[k].X].y, nodes[path[k].Y].x, nodes[path[k].Y].y);
            }
            for (int k = 0; k < nodes.Count; k++)
            {
                if (nodes[k].x == -10)
                    continue;
                gr.FillEllipse(new SolidBrush(nodes[k].col), nodes[k].x - nodes[k].r / 2, nodes[k].y - nodes[k].r / 2, nodes[k].r, nodes[k].r);
                gr.DrawString(nodes[k].num.ToString(), new Font("Helvetica", (nodes[k].r + 3) / 3, FontStyle.Bold), Brushes.White, nodes[k].fx, nodes[k].fy);
                nodes[k].update();
            }
        }
        static double dist(Point A, Point B)
        {
            double ans = (Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y)));
            return ans;
        }
        static void timer_Tick(object sender, EventArgs e)
        {
            pn.Invalidate();  
        }
        static void refresh_d1_d2()
        {
            if (d1 != -1)
                nodes[d1].col = Color.Black;
            if (d2 != -1)
                nodes[d2].col = Color.Black;
            d1 = -1;
            d2 = -1;
        }
        static void pn_MouseClick(object sender, MouseEventArgs e)
        {
            if (is_d == 1)
                return;
            path = new List<pair<int, int>>();
            ref_cols();
            if (which_but == 0)
            {
                int ind = 0;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 0))
                        ind = 1;
                }
                if (ind == 0)
                {
                    Node new_node = new Node(e.X, e.Y);
                    int K = nodes.Count - 1;
                    if (nums.Count == 0)
                    {
                        new_node.num = mx_num;
                    }
                    else
                    {
                        new_node.num = nums[0];
                    }
                    if (new_node.num.ToString().Length == 1)
                    {
                        new_node.fx = new_node.x - new_node.max_r / 6;
                        new_node.fy = new_node.y - new_node.max_r / 4;
                    }

                    if (new_node.num.ToString().Length == 2)
                    {
                        new_node.fx = new_node.x - new_node.max_r / 3;
                        new_node.fy = new_node.y - new_node.max_r / 4;
                    }

                    if (nums.Count == 0)
                    {
                        new_node.num = mx_num;
                        nodes.Add(new_node);
                        mx_num++;
                    }
                    else
                    {
                        new_node.num = nums[0];
                        nums.RemoveAt(0);
                        nodes[new_node.num - 1] = new_node;
                    }

                }
            }
            if (which_but == 1)
            {
                int pos = -1;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 1))
                        pos = k;
                }
                
                if (pos != -1)
                {
                    for (int k = 0; k < edges.Count; k++)
                    {
                        if (edges[k].x == pos || edges[k].y == pos)
                        {
                            graph.g[edges[k].x].Remove(edges[k].y);
                            graph.g[edges[k].y].Remove(edges[k].x);
                            daf[edges[k].x, edges[k].y] = 0;
                            daf[edges[k].y, edges[k].x] = 0;
                            edges.RemoveAt(k);
                            k--;
                        }
                    }
                    nums.Add(nodes[pos].num);
                    nums.Sort();
                    nodes[pos] = new Node();
                }
            }
            if (which_but == 2)
            {
                int pos = -1;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 1))
                        pos = k;
                }
                if (pos != -1)
                {
                    if (d1 == -1)
                    {
                        d1 = pos;
                        nodes[d1].col = Color.Gray;
                    }
                    else
                    {
                        if (pos != d1)
                        {
                            d2 = pos;
                            if (daf[d1, d2] == 0)
                            {
                                Edge edge = new Edge(d1, d2);
                                graph.g[d1].Add(d2);
                                graph.g[d2].Add(d1);
                                daf[d1, d2] = 1;
                                daf[d2, d1] = 1;
                                edge.update(new Point(nodes[d1].x, nodes[d1].y), new Point(nodes[d2].x, nodes[d2].y));
                                edges.Add(edge);
                            }
                            refresh_d1_d2();
                        }
                        else refresh_d1_d2();
                    }
                }else 
                refresh_d1_d2();
            }
            
            if (which_but == 3)
            {
                int pos = -1;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 1))
                        pos = k;
                }
                if (pos != -1)
                {
                    if (d1 == -1)
                    {
                        d1 = pos;
                        nodes[d1].col = Color.Gray;
                    }
                    else
                    {
                        if (pos != d1)
                        {
                            d2 = pos;
                            for (int k = 0; k < edges.Count; k++)
                            {
                                if ((edges[k].x == d1 && edges[k].y == d2) || (edges[k].x == d2 && edges[k].y == d1))
                                {
                                    graph.g[d1].Remove(d2);
                                    graph.g[d2].Remove(d1);
                                    daf[d1, d2] = 0;
                                    daf[d2, d1] = 0;
                                    edges.RemoveAt(k);
                                    break;
                                }
                            }
                        }
                        refresh_d1_d2();
                    }
                }
                else
                {
                    refresh_d1_d2();
                }
            }
            if (which_but == 5 || which_but==6)
            {
                int pos = -1;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 1))
                        pos = k;
                 }
                if (pos != -1)
                {
                    graph.st = pos;
                    p = new List<pair<int, int>>();
                    if (which_but == 5)
                        p = graph.DFS();
                    else p = graph.BFS();
                    d_p = 0;
                    lng = 0;
                    is_d = 1;
                }
            }
            if (which_but == 7 || which_but==8 || which_but==9)
            {
                int pos = -1;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (nodes[k].x == -10)
                        continue;
                    if (nodes[k].check_click(new Point(e.X, e.Y), 1))
                        pos = k;
                }
                if (pos != -1)
                {
                    if (d1 == -1)
                    {
                        d1 = pos;
                        nodes[d1].col = Color.Gray;
                    }
                    else
                    {
                        if (pos != d1)
                        {
                            d2 = pos;
                            graph.st = d1;
                            graph.fn = d2;
                            p = new List<pair<int, int>>();
                            if (which_but == 7)
                                p = graph.SH_DFS();
                            if (which_but == 8)
                                p = graph.SH_BFS();
                            if (which_but == 9)
                                p = graph.SH_DIK(mx_num-1);
                            d_p = 0;
                            lng = 0;
                            if(p.Count!=0)
                            is_d = 1;
                            refresh_d1_d2();
                        }
                        else refresh_d1_d2();
                    }
                }
                else
                    refresh_d1_d2();
            }
        }
        static void Main()
        {
            Create_Win();
            Application.Run(fm);
        }
    }
}
