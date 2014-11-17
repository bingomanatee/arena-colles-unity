using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class TasksButton : MonoBehaviour
		{

				public static TasksButton button;
				public TMPro.TextMeshPro countLabel;
				public ColonyHUDmanager manager;

				// Use this for initialization
				void Start ()
				{
						button = this;
				}
	
				// Update is called once per frame
				void Update ()
				{
						UpdateCount ();
				}
		
				void OnMouseDown ()
				{
						Debug.Log ("CB MD");
						ShowTasks ();
				}

		#region colonistDisplay
		
				void ShowTasks ()
				{
						manager.ShowTasks ();
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
								countLabel.text = dome.NumberOfTasks.ToString ();
						} else {
								countLabel.text = "";
						}
				}

		#endregion

		}
}