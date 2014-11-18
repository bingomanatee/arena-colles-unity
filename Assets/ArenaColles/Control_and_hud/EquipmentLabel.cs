using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class EquipmentLabel : MonoBehaviour
		{
				public TextMesh label;
				public TextMesh quantityLabel;
				public TextMesh itemHint;
				string item_ = "";

				public string item {
						set { 
								item_ = value;
								if (itemHint)
										itemHint.text = item_;
						}
						get {
								return item_;
						}
				}
				
				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
						ReflectItem ();
				}

				void ReflectItem ()
				{
						if (!Game.GameActiveDome) {
								Debug.Log (string.Format ("Label {0} does not have Dome", name));
								ClearLabels ();
						} else
								switch (item) {
								case "plants": 
										ReflectPlants ();
										break;
				
								case "plastics": 
										ReflectPlastics ();
										break;
				
								case "rovers": 
										ReflectRovers ();
										break;
				
								case "":
										ClearLabels ();
										break;
				
								default:
										ClearLabels ();
										break;
								}
				}

				void Describe (string text, int value)
				{
						label.text = text;
						quantityLabel.text = value.ToString ();
				}
		
				void ReflectPlants ()
				{
						if (Game.GameActiveDome) {
								Describe ("SuperPlants", Game.GameActiveDome.NumberOfPlants);
						} else {
								ClearLabels ();
						}
				}

				void ReflectPlastics ()
				{
						if (Game.GameActiveDome) {
								Describe ("Construction Plastics", Game.GameActiveDome.QuantityOfPlastics);
						} else {
								ClearLabels ();
						}
				}

				void ReflectRovers ()
				{
						if (Game.GameActiveDome) {
								Describe ("Rovers", Game.GameActiveDome.NumberOfRovers);
						} else
								ClearLabels ();
				}

				void ClearLabels ()
				{
						label.text = "";
						quantityLabel.text = "";
				}
		}
	
}