using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Colonist : MonoBehaviour
		{
		
				public Task myTask = null;

				// Use this for initialization
				void Start ()
				{
						if (names.Count < 1)
								InitNames ();

						ColonistName = UseName ();
						name = ColonistName;
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

		#region names

				public string ColonistName;

				public string UseName ()
				{
						int index = Random.Range (0, names.Count);
						string n = names [index];
						names.RemoveAt (index);
						return n;
				}

				public static List<string> names = new List<string> ();

				string astronautFile{ get { return Application.dataPath + "/Resources/astronauts.txt"; } }

				void InitNames ()
				{
						string line;
						Debug.Log ("reading " + astronautFile);
						// Read the file and display it line by line.
						System.IO.StreamReader file = new System.IO.StreamReader (astronautFile);
						while ((line = file.ReadLine()) != null) {
								//Debug.Log ("read astro " + line);
								if (line.Length > 2)
										names.Add (line);
						}
      
						file.Close ();
				}

		#endregion
		
				public bool IsIdle {
						get {	
								return myTask == null;
						}
				}

		}
}