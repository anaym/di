using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    public class QuickEnumerator<T> : IEnumerator<T>
    {
        private readonly Func<bool> moveNext;
        private readonly Func<T> current;
        private readonly Action reset;
        private readonly Action dispose;

        public QuickEnumerator(Func<bool> moveNext, Func<T> current) : this(moveNext, current, Pass.Action, Pass.Action)
        { }

        public QuickEnumerator(Func<bool> moveNext, Func<T> current, Action reset, Action dispose)
        {
            this.moveNext = moveNext;
            this.current = current;
            this.reset = reset;
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose();
        }

        public bool MoveNext()
        {
            return moveNext();
        }

        public void Reset()
        {
            reset();
        }

        public T Current => current();

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}