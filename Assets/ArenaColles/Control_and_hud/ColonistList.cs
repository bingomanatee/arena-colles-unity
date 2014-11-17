using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class ColonistList : MonoBehaviour
		{
				public List<ColonistLabel> labels = new List<ColonistLabel> ();
				bool registered = false;
		
				// Use this for initialization
				void Start ()
				{
				}
	
				// Update is called once per frame
				void Update ()
				{
						if (!registered)
								Register ();
				}

				void Register ()
				{
						if (Game.game) {
								Game.game.DomeChangedEvent += ReflectColony;
								registered = true;
						}
				}

				public void ReflectColony (Dome colony)
				{
						foreach (ColonistLabel label in labels)
								label.gameObject.SetActive (false);
								
						if (Game.game.ActiveDome) {
								for (int i = 0; i < colony.ColonistsInColony.Count; i++) {
										Colonist c = colony.ColonistsInColony [i];
										labels [i].gameObject.SetActive (true);
										labels [i].colonist = c;
								}
						}
				}
				
				public void ReflectColony ()
				{
						ReflectColony (Game.game.ActiveDome);
				}
		}
}