using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class NextDayButton : MonoBehaviour
		{

				public TMPro.TextMeshPro DayLabel;
				public ColonyHUDmanager manager;
		
#region loop
				// Use this for initialization
				void Start ()
				{
				}
	
				// Update is called once per frame
				void Update ()
				{
						UpdateDayLabel ();
				}
#endregion

				void OnMouseDown ()
				{
						Debug.Log ("Changing Day");
						Game.game.NextDay ();
				}

#region colonistDisplay
				void UpdateDayLabel ()
				{
						if (!Game.game)
								return;
						DayLabel.text = Game.game.Day.ToString ();
				}
#endregion

		}
}