using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace XhtmlDumper.Tests
{
    public enum State
    {
        Open,
        Close
    }

    public class TestObject
    {
        public int Counter;
        public int TotalCounter { get; set; }
        public decimal ProgressPercent = (decimal) 0.1156;

        public string Name { get; set; }

        public State State;
        public State? State2;

        public SubType Child;

        private int privateField = 111;

        [Browsable(false)]
        public int NotBrowsable = 222;
    }

    public class SubType
    {
        public string Desciption = "hi";
        public string[] Names = new[] {"hello", "world"};
    }


    [TestFixture]
    public class ObjectXhtmlRendererTests
    {
        [Test]
        public void Render_Enumerable()
        {
            var o1 = new TestObject() { Counter = 1, TotalCounter = 200, Name = "First", State = State.Close, State2 = null, Child = new SubType()};
            var o2 = new TestObject() { Counter = 2, TotalCounter = 250, Name = "Second", State = State.Open, State2 = State.Close };
            var list = new[] {o1, o2};

            using (var s = new StreamWriter("test.html", false))
            using (var dumper = new XhtmlDumper(s))
            {
                dumper.WriteObject(list, null, 1);
                dumper.WriteObject(o1, "First object", 2);

                dumper.WriteObject("a string", "", 1);
                dumper.WriteObject(new[] { 1, 2, 3, 4 }, "", 1);
                dumper.WriteObject(5.555, "", 1);
                dumper.WriteObject(DateTime.Now, "", 1);
                dumper.WriteObject(DateTimeOffset.Now, "", 1);
            }
        }

    }//end class
}
