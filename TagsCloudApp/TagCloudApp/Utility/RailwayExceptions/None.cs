﻿using System;

namespace Utility.RailwayExceptions
{
    // CR: Should be removed
    public sealed class None { }

    // CR: Same
    public static class FuncExtension
    {
        public static Func<None> ToFunc(this Action foo)
        {
            return () =>
            {
                foo();
                return new None();
            };
        }

        public static Func<T, None> ToFunc<T>(this Action<T> foo)
        {
            return arg =>
            {
                foo(arg);
                return new None();
            };
        }
    }
}