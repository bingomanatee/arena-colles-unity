using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class ColonistButton : MonoBehaviour
		{

				public static ColonistButton button;
				public ColonistLabel labelBase;
				public TMPro.TextMeshPro countLabel;
				List<ColonistLabel> labels = new List<ColonistLabel> ();
				const float Y_OFFSET = -0.75f;
				const float Y_SCALE = 0.4f;
				const float X_OFFSET = -1;
				public ColonyHUDmanager manager;
				public Color countColor = new Color (0.8f, 1, 1, 0.75f);
				public Color countColorActive = new Color (0.9f, 1, 1);

				// Use this for initialization
				void Start ()
				{
						button = this;
						OnMouseExit ();
				}
	
				// Update is called once per frame
				void Update ()
				{
						UpdateCount ();
				}
		
				void OnMouseDown ()
				{
						Debug.Log ("CB MD");
						ShowColonists ();
				}

		#region colonistDisplay
		
				void ShowColonists ()
				{
						manager.ShowColonists ();
				}

				Dome dome { get {
								if (Game.game)
										return Game.game.ActiveDome;
								else 
										return null;
						} }

				void UpdateCount ()
				{
						if (dome) {
								countLabel.text = dome.NumberOfColonists.ToString ();
						} else {
								countLabel.text = "";
						}
				}

				void OnMouseOver ()
				{
						countLabel.color = countColorActive;
				}
		
				void OnMouseExit ()
				{
						countLabel.color = countColor;
				}
		#endregion

		}
}