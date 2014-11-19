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
						Game.DomeChangedEvent += ReflectDome;
				}
	
				// Update is called once per frame
				void Update ()
				{
				}

				public void ReflectDome (Dome colony)
				{
						foreach (ColonistLabel label in labels)
								label.gameObject.SetActive (false);
								
						if (Game.game.ActiveDome) {
								for (int i = 0; i < colony.ColonistsInColony.Count; i++) {
										Colonist c = colony.ColonistsInColony [i];
										labels [i].gameObject.SetActive (true);
										labels [i].Colonist = c;
								}
						}
				}
				
				public void ReflectDome ()
				{
						ReflectDome (Game.game.ActiveDome);
				}
		}
}