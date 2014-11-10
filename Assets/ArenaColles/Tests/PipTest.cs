using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class PipTest : MonoBehaviour
		{
				public Pips pips;
				int counter = -6;
				// Use this for initialization
				void Start ()
				{
						InvokeRepeating ("TestCounter", 2, 1);
				}
	
				// Update is called once per frame
				void Update ()
				{
			
				}
				
				void TestCounter ()
				{
						pips.pipValue = counter; 
						foreach (int v in Utils.Range (-Pips.EXTENT, Pips.EXTENT)) {
								int c = Mathf.Clamp (counter, -Pips.EXTENT, Pips.EXTENT);
								if (pips.map.ContainsKey (v) && (pips.map [v].enabled != (v == c))) {
										IntegrationTest.Fail (gameObject, string.Format ("Mismatch of expectation at {0} during counter {1}({2})", v, c, counter));
								}
						}
			
						if (counter > 6)
								CancelInvoke ();
						++counter;
				}
		}
}