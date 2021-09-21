using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsWebApi.Internal
{
    internal static class JenkinsRun
    {
        internal static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        internal static void Run<T1>(Action<T1> action, T1 arg)
        {
            try
            {
                action(arg);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        internal static void Run<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            try
            {
                action(arg1, arg2);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        internal static TResult Run<TResult>(Func<TResult> function)
        {
            try
            {
                return function();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        internal static TResult Run<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 arg1, T2 arg2)
        {
            try
            {
                return function(arg1, arg2);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
