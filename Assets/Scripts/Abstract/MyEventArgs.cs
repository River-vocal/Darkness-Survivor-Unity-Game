using System;

namespace MyEventArgs
{
    public class IntegerEventArg : EventArgs
    {
        public int Value { get; }

        public IntegerEventArg(int value){
            Value = value;
        }
    }

    public class BooleanEventArg : EventArgs
    {
        public bool Value { get; }

        public BooleanEventArg(bool value)
        {
            Value = value;
        }
    }
}