using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCommons
{
    using GCJ;

    [TestClass]
    public class AnswerTest
    {
        private static readonly Regex GCJ_std_answer = new Regex(@"^Case #(?<CaseNo>\d+): (?<Answer>.*)$", RegexOptions.Compiled | RegexOptions.Singleline);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deal_with_null_value()
        {
            var ans = 1.Answer(null);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "First argument cannot be ommited or null")]
        public void Answer_with_no_value()
        {
            var ans = 1.Answer();
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deal_with_first_null_argument()
        {
            var ans = 1.Answer(null, 1);
            Assert.Fail();
        }

        [TestMethod]
        public void Answer_with_empty_value()
        {
            const string expected = "Case #1: ";

            var ans = 1.Answer(string.Empty);
            Assert.AreEqual(expected, ans);
        }

        //[Ignore]
        [TestMethod]
        public void Simple_types_default_ToString()
        {
            Func<string, string> ansExtractor = (ans) =>
                {
                    var m = GCJ_std_answer.Match(ans);
                    return m.Success ? m.Groups["Answer"].Value : null;
                };
            string answer;

            // bool
            {
                bool True = true;
                answer = ansExtractor(1.Answer(True));
                Assert.AreEqual(bool.TrueString, answer);
            }

            // byte
            {
                byte[] bytes = {byte.MinValue, byte.MaxValue, 0x1f, 0x33, 0xab, 0xcd, 0xfe }; 
                foreach (byte b in bytes)
                {
                    answer = ansExtractor(1.Answer(b));
                    Assert.AreEqual(b.ToString(), answer);
                }
            }

            // short
            {
                short[] sVals = { short.MinValue, -1, 0, short.MaxValue };
                foreach (var s in sVals)
                {
                    answer = ansExtractor(1.Answer(s));
                    Assert.AreEqual(s.ToString(), answer);    
                }
            }

            // strings
            {
                string[] singleLines = {string.Empty, " ", "-", new string('a', 500), };
                foreach (var s in singleLines)
                {
                    answer = ansExtractor(1.Answer(s));
                    Assert.AreEqual(s, answer);
                }
            }

            // integers
            {
                int[] ints = {int.MinValue, 0, int.MaxValue};
                foreach (var i in ints)
                {
                    answer = ansExtractor(1.Answer(i));
                    Assert.AreEqual(i.ToString(), answer);
                }
            }

            // integers
            {
                long[] longs = { long.MinValue, 0, long.MaxValue };
                foreach (var i in longs)
                {
                    answer = ansExtractor(1.Answer(i));
                    Assert.AreEqual(i.ToString(), answer);
                }
            }
        }

        [TestMethod]
        public void Correct_case_numbers()
        {
            for (int i = 0; i <= 100; i+=5)
            {
                var ans = i.Answer(i.ToString("000"));
                var m = GCJ_std_answer.Match(ans);
                Assert.IsTrue(m.Success);
                Assert.AreEqual(2+1, m.Groups.Count);
                Assert.AreEqual(3, m.Groups["Answer"].Value.Length);
                Assert.AreEqual(i.ToString(), m.Groups["CaseNo"].Value);
            }
        }

        [TestMethod]
        public void Formatted_answer_test()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            
            {
                string expected = "Case #1: 1.2.3.4.5";
                Assert.AreEqual(expected, 1.Answer("{0}.{1}.{2}.{3}.{4}", 1, 2, 3, 4, 5));
            }

            {
                string expected = "Case #2: 5-10-5";
                Assert.AreEqual(expected, 2.Answer("{0}-{1}-{0}", 5, 10));
            }

            {
                string expected = "Case #3: 005 2.00";
                Assert.AreEqual(expected, 3.Answer("{0:D3} {1:F2}", 5, 2));
            }

            {
                string expected = "Case #4: 0xff";
                Assert.AreEqual(expected, 4.Answer("0x{0:x2}", 0xff));
            }
        }
    }
}
