/*
File name: ThreadRepository.cs
Project: Mystify
By: William Pring 
Date: October 23, 2015
Description: Keep track of my threads and have supporting thread functions
*/


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
        public void JoinAll()
        {
            foreach (Thread th in threads)
            {
                th.Join();
            }
        }
        public void EndAll()
        {
            threads.Clear();
        }
        public void Add(string name, ParameterizedThreadStart threadSt, Graphics mLine)
        {
            Thread trd = new Thread(threadSt);
            trd.Name = name;
            threads.Add(trd);
            trd.Start(mLine);

        }
 
    }
}
