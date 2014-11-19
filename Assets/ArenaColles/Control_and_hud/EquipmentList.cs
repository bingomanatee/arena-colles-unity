using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class EquipmentList : MonoBehaviour
		{
				public EquipmentLabel[] labels = new EquipmentLabel[6];
				bool isRegistered = false;

				// Use this for initialization
				void Start ()
				{
						labels [0].item = "plants";
						labels [1].item = "plastics";
						labels [2].item = "rovers";
				}
	
				// Update is called once per frame
				void Update ()
				{
						if (!isRegistered)
								Register ();
						//	else 
						//ReflectColony ();
				}
				
				void Register ()
				{
						if (Game.game)
								Game.DomeChangedEvent += ReflectDome;
				}

				public void ReflectDome (Dome dome)
				{
						// delegated to labels at this point
				}
		}
}