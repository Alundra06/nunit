﻿// ***********************************************************************
// Copyright (c) 2015 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Reflection;

namespace NUnit.Framework.Internal.Tests
{
    public abstract class TestNamingTests
    {
        protected const string OUTER_CLASS = "NUnit.Framework.Internal.Tests.TestNamingTests";

        protected abstract string FixtureName { get; }

        protected TestContext.TestAdapter CurrentTest
        {
            get { return TestContext.CurrentContext.Test; }
        }

        [Test]
        public void SimpleTest()
        {
            CheckNames("SimpleTest");
        }

        [TestCase(5, 7, "ABC")]
        public void ParameterizedTest(int x, int y, string s)
        {
            CheckNames("ParameterizedTest(5,7,\"ABC\")");
        }

        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        public void TestCaseWithLongStringArgument(string s)
        {
            CheckNames("TestCaseWithLongStringArgument(\"abcdefghijklmnop...\")");
        }

        [TestCase(42)]
        public void GenericTest<T>(T arg)
        {
            CheckNames("GenericTest<Int32>(42)");
        }

        private void CheckNames(string expectedName)
        {
            Assert.That(CurrentTest.Name, Is.EqualTo(expectedName));
            Assert.That(CurrentTest.FullName, Is.EqualTo(OUTER_CLASS + "+" + FixtureName + "." + expectedName));
        }

        public class SimpleFixture : TestNamingTests
        {
            protected override string FixtureName
            {
                get { return "SimpleFixture"; }
            }
        }

        [TestFixture(typeof(int))]
        public class GenericFixture<T> : TestNamingTests
        {
            protected override string FixtureName
            {
                get { return "GenericFixture<Int32>"; }
            }
        }

        [TestFixture(42, "Forty-two")]
        public class ParameterizedFixture : TestNamingTests
        {
            public ParameterizedFixture(int x, string s) { }

            protected override string FixtureName
            {
                get { return "ParameterizedFixture(42,\"Forty-two\")"; }
            }
        }

        [TestFixture("This is really much too long to be used in the test name!")]
        public class ParameterizedFixtureWithLongStringArgument : TestNamingTests
        {
            public ParameterizedFixtureWithLongStringArgument(string s) { }

            protected override string FixtureName
            {
                get { return "ParameterizedFixtureWithLongStringArgument(\"This is really mu...\")"; }
            }
        }

        [TestFixture(typeof(int), typeof(string), 42, "Forty-two")]
        public class GenericParameterizedFixture<T1,T2> : TestNamingTests
        {
            public GenericParameterizedFixture(T1 x, T2 y) { }

            protected override string FixtureName
            {
                get { return "GenericParameterizedFixture<Int32,String>(42,\"Forty-two\")"; }
            }
        }
    }
}
