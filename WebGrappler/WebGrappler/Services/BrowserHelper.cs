using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebGrappler.Handler;
using WebGrappler.Interface;
using WebGrappler.Models;

namespace WebGrappler.Services
{
    
    class BrowserHelper<T> where T : class,IDisposable,IReuseable,new()
    {
        Dictionary<T, double> LastReturnMap = new Dictionary<T, double>();
        object LastLocker = new object();
        double TimeLimit = TimeSpan.FromSeconds(20).Ticks;
        bool NeedQuite = false;
        

        List<T> pool = new List<T>();

        private uint PoolSize = 10;

        Thread thread;

        public BrowserHelper()
        {
           //实现对象老化回收
           thread = new Thread(() =>
            {
                LinkedList<T> removelist = new LinkedList<T>();
                while (NeedQuite == false)
                {
                    var _current = DateTime.Now.Ticks;
                    lock (LastLocker)
                    {
                        foreach (var item in LastReturnMap)
                        {
                            if((_current - item.Value)>= TimeLimit)
                            {
                                removelist.AddFirst(item.Key);  
                            }
                        }
                    }
                    lock (pool)
                    {
                        foreach (var sub in removelist)
                        {
                            if (sub.CanReuse())
                            {
                                pool.Remove(sub);
                                LastReturnMap.Remove(sub);
                                sub.Release();
                                //Console.WriteLine("释放。。。。。。。");
                            }
                        }
                        removelist.Clear();
                    }

                    Thread.Sleep(2333);
                }
            });
           thread.Start();
        }

        /// <summary>
        /// 获取一个可用对象，如果没有且池未满且无可用对象将返回一个新建的对象
        /// 如果池已满，但有可用对象将返回第一个找到的可用对象
        /// 如果池已满且无可用对象将返回NULL
        /// 当池中对象个数大于池个数时，将进行一次回收
        /// </summary>
        /// <param name="RequestUIThread">为true 则将使用UI线程创建该对象</param>
        /// <returns></returns>
        public T Get(bool RequestUIThread = true)
        {

            if (PoolSize < pool.Count)
            {
                Collection();
                LogHelper.Debug(string.Format(" 对象池回收 (Have/Limit){0}/{1} ", pool.Count, PoolSize));
            }

            lock (pool)
            {
                foreach (var item in pool)
                {
                    if (item.CanReuse())
                    {
                        item.Reset();
                        item.ChangeReuseable(false);
                        LogHelper.Debug(string.Format(" 对象池 找到可重用资源对象 {0}", item.GetHashCode()));
                        return item;
                    }
                }
            }

            if (pool.Count < PoolSize)
            {
                lock (pool)
                {
                    if (RequestUIThread)
                    {
                       T t = App.Current.Dispatcher.Invoke(()=> {
                            return new T();
                        });
                        t.ChangeReuseable(false);
                        pool.Add(t);
                        LogHelper.Debug(string.Format(" 对象池 新建可重用资源对象 {0}", t.GetHashCode()));
                        return t;
                    }
                    else
                    {
                        T t = new T();
                        t.ChangeReuseable(false);
                        pool.Add(t);
                        return t;
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// 重置可用性
        /// </summary>
        /// <param name="t"></param>
        public void Reuse(T t)
        {
            t.Reset();
            t.ChangeReuseable(true);
            //记录对象最后使用时间，超时后将销毁此对象
            lock (LastLocker)
            {
                if (LastReturnMap.ContainsKey(t)) {
                    LastReturnMap[t] = DateTime.Now.Ticks;
                }
                else
                {
                    LastReturnMap.Add(t,DateTime.Now.Ticks);
                }
            }

        }
        
        /// <summary>
        /// 当Pool中对象个数多于PoolSize，清理Pool，释放空闲的对象使其符合设定的大小
        /// </summary>
        public  void Collection()
        {
            lock (pool)
            {
                for (int i = pool.Count - 1; i > 0; i++)
                {
                    if (pool.Count > PoolSize && pool[i].CanReuse())
                    {
                        var _t = pool[i];
                        pool.RemoveAt(i);
                        _t.Release();
                    }
                }
            }
        }
        /// <summary>
        /// 返回当前池中对象个数
        /// </summary>
        /// <returns></returns>
        public int GetObjectSize()
        {
            return pool.Count();
        }
        /// <summary>
        /// 获取当前池大小
        /// </summary>
        /// <returns></returns>
        public uint GetPoolSize()
        {
            return PoolSize;
        }
        /// <summary>
        /// 设置变更池大小
        /// </summary>
        /// <param name="size"></param>
        public void SetPoolSize(uint size)
        {
            var os = PoolSize;
            PoolSize = size;
            if (os < size) Collection();
        }
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {

            NeedQuite = true;
            if (pool.Count>0)
            {
                lock (pool)
                {
                    foreach (var item in pool)
                    {
                        item.Release();
                    }
                    pool.Clear();
                }
            }
           
        }


    }
}
