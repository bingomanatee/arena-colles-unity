using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class Utils
		{
		
				public static int[] Range (int min_, int max_)
				{
						if (min_ == max_)
								return new int[]{min_};
						int min = Mathf.Min (min_, max_);
						int max = Mathf.Max (min_, max_);
						int range = max - min + 1;
						int[] result = new int[range];

						for (int i = 0; i < range; ++i) {
								result [i] = i + min;
						}
						return result;
				}

				public static int[] Range (float min, float max)
				{
						return Range (Mathf.FloorToInt (min), Mathf.FloorToInt (max));
				}

		}

}