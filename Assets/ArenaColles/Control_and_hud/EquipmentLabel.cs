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
						if (!colony) {
								Debug.Log (string.Format ("Label {0} does not have colony", name));
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

				Dome colony {
						get {
								if (Game.game && Game.game.ActiveDome)
										return Game.game.ActiveDome;
								else 
										return null;
						}
				}

				void Describe (string text, int value)
				{
						label.text = text;
						quantityLabel.text = value.ToString ();
				}
		
				void ReflectPlants ()
				{
						if (colony) {
								Describe ("SuperPlants", colony.NumberOfPlants);
						} else {
								ClearLabels ();
						}
				}

				void ReflectPlastics ()
				{
						if (colony) {
								Describe ("Construction Plastics", colony.QuantityOfPlastics);
						} else {
								ClearLabels ();
						}
				}

				void ReflectRovers ()
				{
						if (colony) {
								Describe ("Rovers", colony.NumberOfRovers);
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