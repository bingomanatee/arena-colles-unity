using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Game : MonoBehaviour
		{
		
				public static Game game; // singleton
				
				int day = 0;

				public int Day {
						get {
								return day;
						}
						set {
								day = value;
						}
				}

				public TerraGen terrain;
				public GameObject colonyTemplate;
				public Camera ColonyCamera;
				const float CAMERA_ANGLE_Z_TILT = 20.0f;
				public bool StartAtCenter = false;

		#region dome
		
				public static Dome GameActiveDome {
						get {
								if (!game)
										return null;
								return game.ActiveDome;
						}				
				}

				public List<Dome> Domes = new List<Dome> ();
				Dome activeDome;

				public Dome ActiveDome {
						set {
								activeDome = value;
								if (DomeChangedEvent != null)
										DomeChangedEvent (value);
								ViewDome ();
						}
						get { 
								return activeDome;
						}
				}
		
#endregion

				#region loop

				public void Start ()
				{
						terrain = GetComponent<TerraGen> ();
						game = this;
						ViewDome (); // should hide camera
				}

				public void Update ()
				{
						if ((!terrain.TestMode) && day == 0) {
								FirstTurn ();
								++day;
						}
				}

				#endregion

#region turns

				public void FirstTurn ()
				{
						Debug.Log ("First Turn");
						Cell cell = StartAtCenter ? terrain.GetCell (0, 0) : terrain.GetCellNearCenter ();
						GameObject colony = (GameObject)Instantiate (colonyTemplate);
						Dome c = colony.GetComponent<Dome> ();
						c.NumberOfColonists = 2;
						c.NumberOfPlants = 2;
						c.MaxStorage ();
						
						Domes.Add (c);
						c.SetCell (cell);
				}
#endregion

		#region Camera

				void ViewDome ()
				{
						if (!ColonyCamera)
								return;
						if (!ActiveDome) {
								ColonyCamera.enabled = false;
						} else {
								ColonyCamera.enabled = true;
								Vector3 ccPos = ColonyCamera.transform.position;
								ccPos.x = ActiveDome.transform.position.x;
								ccPos.z = ActiveDome.transform.position.z + CAMERA_ANGLE_Z_TILT;
								ColonyCamera.transform.position = ccPos;
								ColonyCamera.transform.LookAt (ActiveDome.focus.transform.position);
						}
				}
		#endregion

		#region Events
		
				public delegate void ActiveColonyChangedDelegate (Dome colony);
		
				/// <summary>An event that gets fired </summary>
				public event ActiveColonyChangedDelegate DomeChangedEvent;

		
		#endregion

		}
}
