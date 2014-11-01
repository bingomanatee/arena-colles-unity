using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace ArenaColles
{
		public class UtilsTest
		{

#region range

				[Test]

				public void TestRange ()
				{
						int[] range = Utils.Range (0, 5);
						Assert.AreEqual (new int[]{0, 1, 2, 3, 4, 5}, range, "range from 0 to 5");
				}

#endregion
		}
}