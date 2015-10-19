using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mystify
{
    class ThreadRepository
    {
        List<Thread> threads = new List<Thread>();
        public ThreadRepository()
        {
            //do nothing
        }
        public void StartAll()
        {
            foreach (Thread t in threads)
            {
                t.Start();
            }
        }
        public void JoinAll()
        {
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }
        public void Add(string name, ParameterizedThreadStart threadSt, Graphics mLine)
        {
            Thread trd = new Thread(threadSt);
            trd.Name = name;
            threads.Add(trd);
            trd.Start(mLine);

        }
        public void AddStuct(string name, ParameterizedThreadStart threadSt, ref Line.lineInfo mLine)
        {
            Thread trd = new Thread(threadSt);
            trd.Name = name;
            threads.Add(trd);
            trd.Start(mLine);

        }
    }
}
