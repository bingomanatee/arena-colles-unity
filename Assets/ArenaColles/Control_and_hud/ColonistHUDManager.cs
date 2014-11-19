using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ColonistHUDManager : HUDmanager
		{
				public static ColonistHUDManager manager;
				bool registered = false;

				// Use this for initialization
				void Start ()
				{
						manager = this;
						ReflectColonist (null);
						Game.ColonistChangedEvent += ReflectColonist;
				}
	
				// Update is called once per frame
				void Update ()
				{
			
				}

		#region colonist
		
				void ReflectColonist (Colonist c)
				{
						HideAllPanels ();
						
						if (!c) {
								hudCamera.gameObject.SetActive (false);
						} else {
								Title.text = c.ColonistName;
								hudCamera.gameObject.SetActive (true);
						}
				}

		#endregion

		}
}