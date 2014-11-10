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
						else 
								ReflectColony ();
				}
				
				void Register ()
				{
						if (Game.game)
								Game.game.ColonyChangedEvent += ReflectColony;
				}

				public void ReflectColony ()
				{
						ReflectColony (Game.game && Game.game.ActiveDome ? Game.game.ActiveDome : null);
				}

				void ReflectColony (Dome colony)
				{
						// delegated to labels at this point
				}
		}
}