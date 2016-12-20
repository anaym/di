using System;
using Utility.RailwayExceptions.Extensions;

namespace Utility.RailwayExceptions
{
    public static class LogicResultExtension
    {
        public static Result<TO> And<TA, TB, TO>(this Result<TA> first, Result<TB> second, Func<TA, TB, TO> selector)
        {
            return first.Select(f => second.Select(s => selector(f, s))).Unpack();
        }

        public static Result<TO> And<TA, TB, TC, TO>(this Result<TA> first, Result<TB> second, Result<TC> third, Func<TA, TB, TC, TO> selector)
        {
            return first.And(second, (f, s) => third.Select(t => selector(f, s, t))).Unpack();
        }

        public static Result<TO> Or<T, TO>(this Result<T> first, Result<T> second, Func<T, TO> selector)
        {
            return (first.IsSuccess ? first : second).Select(selector);
        }
    }
}