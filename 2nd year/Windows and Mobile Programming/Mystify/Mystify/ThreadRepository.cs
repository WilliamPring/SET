using System;
using System.Collections.Generic;
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




    }
}
