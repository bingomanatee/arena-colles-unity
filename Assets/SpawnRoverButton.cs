using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class SpawnRoverButton : MonoBehaviour
		{
				public TaskPanel RoverPanel;

				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
	
	#region spawn on click

				void OnMouseUpAsButton ()
				{
						if (RoverPanel.state.state == TaskPanel.STATE_WORKABLE) {
								RoverPanel.AddTask ();
								Game.game.ActiveDome.SpawnRover ();
						}
				}
		
	#endregion
		}
}