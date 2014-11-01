using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class ColonistButton : MonoBehaviour
		{

				public static ColonistButton button;
				public ColonistLabel labelBase;
				List<ColonistLabel> labels = new List<ColonistLabel> ();
				const float Y_OFFSET = -0.75f;
				const float Y_SCALE = 0.4f;
				const float X_OFFSET = -1;
				bool registered = false;

				// Use this for initialization
				void Start ()
				{
						button = this;
				}
	
				// Update is called once per frame
				void Update ()
				{
						if (!registered)
								Register ();
				}
		
				void OnMouseDown ()
				{
						Debug.Log ("CB MD");
						ToggleColonists (Game.game.ActiveColony);
				}

		#region colonistDisplay

				void Register ()
				{
						if (Game.game) {
								Game.game.ColonyChangedEvent += ToggleColonists;
						}
				}

				void ToggleColonists (Colony colony)
				{
						if (colony == null) {
								HideColonists ();
								return;
						}

						colony.showColonists = !colony.showColonists;

						if (colony.showColonists)
								ShowColonists ();
						else 
								HideColonists ();
				}

				void HideColonists ()
				{
			
						Debug.Log ("Hiding c");
						foreach (ColonistLabel l in labels)
								if (l)
										Destroy (l.gameObject);
						labels.Clear ();
				}

				void ShowColonists ()
				{
						HideColonists ();
						Debug.Log ("Showing c");
						int i = 0;
						if (Game.game.ActiveColony) {
								foreach (Colonist c in Game.game.ActiveColony.ColonistsInColony) {
										GameObject g = (GameObject)Instantiate (labelBase.gameObject);
										g.transform.parent = transform;
										g.transform.localPosition = new Vector3 (X_OFFSET, Y_OFFSET + i * - Y_SCALE);
										g.transform.localRotation = Quaternion.identity;
					
										ColonistLabel l = g.GetComponent<ColonistLabel> ();
										l.SetColonist (c);
										labels.Add (l);
										++i;
								}
						}
				}

		#endregion

		}
}