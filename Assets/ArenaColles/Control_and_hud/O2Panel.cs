using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class O2Panel : MonoBehaviour
		{
				public TextMesh valueText;
				public TextMesh maxText;
				public Pips pips;
				public GameObject panel;
				int value_ = 0;
				int max_ = 0;

				public Dome colony {
						get { return colony_; }
						set {
								colony_ = value;
								UpdateReadout ();
						}
				}
				
				public int readout { 
						set {
								pips.pipValue = value;
						}
						get { 
								return pips.pipValue; 
						} 
				}

				Dome colony_;

				public int o2value {
						set {
								value_ = value;
								valueText.text = value.ToString ();
						}
						get { return value_; }
				}
				
				public int o2max {
						get{ return max_;}
						set {
								max_ = value; 
								maxText.text = string.Format ("max {0}", value.ToString ());
						} 
				}
	
				// Use this for initialization
				void Start ()
				{
						o2value = 0;
				}
	
				// Update is called once per frame
				void Update ()
				{
						UpdateReadout ();
				}

				void UpdateReadout ()
				{
						if (colony) {
								o2value = colony.storedO2;
								o2max = colony.maxO2;
								if (colony.NumberOfColonists > 0) {
										readout = colony.o2production - colony.NumberOfColonists;
										// Debug.Log (string.Format ("readout = O2 production {0} - number of colonists ({1}) = {2}", colony.o2production, colony.NumberOfColonists, readout));
								} else if (colony.o2production > 0) { 
										readout = 4;
								} else {
										readout = 0;
								}
						} else {
								o2value = 0;
								o2max = 0;
						}
				}

		}
}