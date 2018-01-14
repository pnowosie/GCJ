using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CodeCommons
{
    using GCJ;

    [TestClass]
    public class EnumerableEachTest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Each_on_null()
        {
            IEnumerable<int> nullEnumerable = null;
            nullEnumerable.Each((elt, ind) => { });
        }

        [TestMethod]
        public void Each_on_empty()
        {
            Enumerable.Empty<int>().Each((elt, ind) => { throw new InvalidOperationException(); });
        }

        [TestMethod]
        public void Each_on_integer_sequence()
        {
            int last_ind = -1;
            Enumerable.Range(0, 10).Each((elt, ind) =>
                {
                    if (elt < 0) throw new InvalidOperationException();
                    if (elt >= 10) throw new InvalidOperationException();
                    if (elt != ind) throw new InvalidOperationException();
                    last_ind = ind;
                });

            Assert.AreEqual(10-1, last_ind);
        }

        [TestMethod]
        public void Every_element_is_used()
        {
            bool[] ba = new bool[10];
            ba.Each((elt, ind) =>
                {
                    if (elt != false) throw new InvalidOperationException();
                    ba[ind] = true; // cannot change value by arg
                });
            Assert.IsTrue(ba.All(elt => elt));
        }
    }
}
