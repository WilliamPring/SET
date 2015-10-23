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
    /*
      Name: ThreadRepository
      Purpose: Keep track of the threads and have supporting functions for the threads
      Data Members : threads - list of thread 
      Type:  Nothing

       */
  
    class ThreadRepository
    {


        /// <summary>
        /// The threads
        /// </summary>
        List<Thread> threads = new List<Thread>();
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadRepository"/> class.
        /// </summary>
        public ThreadRepository()
        {
            //do nothing
        }
        /// <summary>
        /// Joins all.
        /// </summary>
        public void JoinAll()
        {
            foreach (Thread th in threads)
            {
                th.Join();
            }
        }
        /// <summary>
        /// Ends all.
        /// </summary>
        public void EndAll()
        {
            threads.Clear();
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="threadSt">The thread st.</param>
        /// <param name="mLine">The m line.</param>
        public void Add(string name, ParameterizedThreadStart threadSt, Graphics mLine)
        {
            Thread trd = new Thread(threadSt);
            trd.Name = name;
            threads.Add(trd);
            trd.Start(mLine);

        }
 
    }
}
