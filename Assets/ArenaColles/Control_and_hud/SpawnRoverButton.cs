using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class SpawnRoverButton : MonoBehaviour
		{
				public TaskPanel RoverPanel;
	
	#region spawn on click

				void OnMouseUpAsButton ()
				{
						if (RoverPanel.state.state == TaskPanel.STATE_WORKABLE) {
								RoverPanel.AddTask ();
								RoverPanel.gameObject.SetActive (false);
						} else {
								// @TODO: beep? Dialog?
						}
				}
		
	#endregion
		}
}