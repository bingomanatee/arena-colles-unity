using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class PlasticsPanel : MonoBehaviour
		{
				public TextMesh valueText;
				public TextMesh maxText;
				public Pips pips;
				public GameObject panel;
				int value_ = 0;
				int max_ = 0;

				public Dome colony {
						get { return (Game.game && (Game.game.ActiveDome != null)) ? Game.game.ActiveDome : null; }
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

				public int plasticsValue {
						set {
								value_ = value;
								valueText.text = value.ToString ();
						}
						get { return value_; }
				}
				
				public int plasticsMax {
						get{ return max_;}
						set {
								max_ = value; 
								maxText.text = string.Format ("max {0}", value.ToString ());
						} 
				}
	
				// Use this for initialization
				void Start ()
				{
						plasticsValue = 0;
				}
	
				// Update is called once per frame
				void Update ()
				{
						UpdateReadout ();
				}

				void UpdateReadout ()
				{
						if (colony) {
								plasticsValue = colony.QuantityOfPlastics;
								plasticsMax = colony.PlasticsStorageCap;
								
								readout = colony.PlasticsProduction - colony.PlasticsConsumption;
							
						} else {
								plasticsValue = 0;
								plasticsMax = 0;
						}
				}

		}
}