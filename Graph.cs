using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Text;

namespace Graph_Rel
{
    class Graph
    {
        public List<int>[] g = new List<int>[1001];
        public int[] used,d,w;
        public List<pair<int, int>> ans;
        public List<pair<int, int>> path;
        public int st = 0, fn = 0,ind=0;
        
        public void new_graph()
        {
            for (int k = 0; k <= 1000; k++)
                g[k] = new List<int>();
        }
        public void dfs(int v)
        {
            used[v] = 1;
            ans.Add(new pair<int, int>(v, -1));
            for (int k = 0; k < g[v].Count; k++)
            {
                int to = g[v][k];
                if (used[to] !=1)
                {
                    ans.Add(new pair<int, int>(v, to));
                    dfs(to);
                }
            }
        }
        public List<pair<int, int>> DFS()
        {
            used = new int[1001];
            ans = new List<pair<int, int>>();
            dfs(st);
            return ans;
        }
        public void bfs()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(st);
            while (q.Count != 0)
            {
                int v = q.Peek();
                used[v]=1;
                ans.Add(new pair<int, int>(v, -1));
                q.Dequeue();
                for (int k = 0; k < g[v].Count; k++)
                {
                    int to = g[v][k];
                    if (used[to] == 0)
                    {
                        ans.Add(new pair<int, int>(v, to));
                        ans.Add(new pair<int, int>(to, -1));
                        q.Enqueue(to);
                        used[to] = 1;
                    }
                }
            }
        }
        public List<pair<int, int>> BFS()
        {
            used = new int[1001];
            ans = new List<pair<int, int>>();
            bfs();
            return ans;
        }
        public void sh_dfs(int v)
        {
            ind = 1;
            ans.Add(new pair<int, int>(v, -1));
            for (int k = 0; k < g[v].Count; k++)
            {
                int to = g[v][k];
                if (d[v]+1<d[to])
                {
                    ans.Add(new pair<int, int>(v, to));
                    d[to] = d[v] + 1;
                    w[to] = v;
                    sh_dfs(to);
                }
            }
        }
        public List<pair<int, int>> SH_DFS()
        {
            d = new int[1001];
            w = new int[1001];
            ans = new List<pair<int, int>>();
            path = new List<pair<int, int>>();
            d[st] = -10000;
            sh_dfs(st);
            if (d[fn] == 0)
            {
                return path;
            }
            int v = fn;
            while (v != st)
            {
                path.Add(new pair<int, int>(v, -1));
                int n_v = w[v];
                path.Add(new pair<int, int>(n_v,v));
                v = n_v;
            }
            path.Add(new pair<int, int>(st, -1));
            path.Reverse();
            ans.Add(new pair<int, int>(-1, -1));
            for (int k = 0; k < path.Count; k++)
            {
                ans.Add(path[k]);
            }
            return ans;
        }
        public void sh_bfs()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(st);
            while (q.Count != 0)
            {
                int v = q.Peek();
                ans.Add(new pair<int, int>(v, -1));
                q.Dequeue();
                for (int k = 0; k < g[v].Count; k++)
                {
                    int to = g[v][k];
                    if (d[to]>d[v]+1)
                    {
                        d[to] = d[v] + 1;
                        w[to] = v;
                        ans.Add(new pair<int, int>(v, to));
                        ans.Add(new pair<int, int>(to, -1));
                        q.Enqueue(to);

                    }
                }
            }
        }
        public List<pair<int, int>> SH_BFS()
        {
            d = new int[1001];
            w = new int[1001];
            ans = new List<pair<int, int>>();
            path = new List<pair<int, int>>();
            d[st] = -10000;
            sh_bfs();
            if (d[fn] == 0)
            {
                return path;
            }
            int v = fn;
            while (v != st)
            {
                path.Add(new pair<int, int>(v, -1));
                int n_v = w[v];
                path.Add(new pair<int, int>(n_v, v));
                v = n_v;
            }
            path.Add(new pair<int, int>(st, -1));
            path.Reverse();
            ans.Add(new pair<int, int>(-1, -1));
            for (int k = 0; k < path.Count; k++)
            {
                ans.Add(path[k]);
            }
            return ans;
        }

        public void sh_dik(int n)
        {
            List<pair<int, int>> q = new List<pair<int, int>>();
            for(int k=0;k<n;k++)
                q.Add(new pair<int,int>(0,k));
            q[st] = new pair<int, int>(-1000, st);
            while (q.Count != 0)
            {
                pair<int, int> v=new pair<int,int>(1000,1000);
                for (int k = 0; k < n; k++)
                    if (v.X > q[k].X && used[k]==0)
                        v = q[k];
                used[v.Y] = 1;
                if (v.Y == fn || v.X==1000)
                    break;
                ans.Add(new pair<int, int>(v.Y, -1));
                for (int k = 0; k < g[v.Y].Count; k++)
                {
                    int to = g[v.Y][k];
                    if (d[to] > d[v.Y] + 1)
                    {
                        d[to] = d[v.Y] + 1;
                        w[to] = v.Y;
                        ans.Add(new pair<int, int>(v.Y, to));
                        ans.Add(new pair<int, int>(to, -1));
                        q[to] = new pair<int, int>(d[to], to);

                    }
                }
            }
        }
        public List<pair<int, int>> SH_DIK(int n)
        {
            d = new int[1001];
            w = new int[1001];
            used = new int[1001];
            ans = new List<pair<int, int>>();
            path = new List<pair<int, int>>();
            d[st] = -10000;
            sh_dik(n);
            if (d[fn] == 0)
            {
                return path;
            }
            int v = fn;
            while (v != st)
            {
                path.Add(new pair<int, int>(v, -1));
                int n_v = w[v];
                path.Add(new pair<int, int>(n_v, v));
                v = n_v;
            }
            path.Add(new pair<int, int>(st, -1));
            path.Reverse();
            ans.Add(new pair<int, int>(-1, -1));
            for (int k = 0; k < path.Count; k++)
            {
                ans.Add(path[k]);
            }
            return ans;
        }
    }
}
