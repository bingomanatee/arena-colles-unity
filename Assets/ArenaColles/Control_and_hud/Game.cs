using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Game : MonoBehaviour
		{
		
				// singleton
				public static Game game;
				public TerraGen terrain;
				public GameObject colonyTemplate;
				public Camera DomeCamera;
				const float CAMERA_ANGLE_Z_TILT = 20.0f;
				public bool StartAtCenter = false;
		
		#region selection 
				GameObject selection;

				public GameObject Selection {
						get {
								return selection;
						}
						set {
								selection = value;
								ActiveDome = null;
								ActiveColonist = null;
								if (!selection)
										return;
								else if (selection.GetComponent<Dome> ()) {
										ActiveDome = selection.GetComponent<Dome> ();
										ViewDome ();
								} else if (selection.GetComponent<Colonist> ()) {
										ActiveColonist = selection.GetComponent<Colonist> ();
										ViewColonist ();
								}
						}
						
				}
		#endregion
				
		#region day
				int day = 0;

				public int Day {
						get {
								return day;
						}
						set {
								Debug.Log (string.Format ("Day # = {0}", value));
								day = value;
								if (DayChangedEvent != null)
										DayChangedEvent (day);
						}
				}

				public void NextDay ()
				{
						++Day;
				}
		#endregion
		
		#region dome
		
				public static Dome GameActiveDome {
						get {
								if (!game)
										return null;
								return game.ActiveDome;
						}	
						set {
								if (game)
										game.ActiveDome = value;
						}			
				}

				public List<Dome> Domes = new List<Dome> ();
				Dome activeDome;

				public Dome ActiveDome {
						set {
								activeDome = value;
								Debug.Log ("Active Dome = " + value);
								if (DomeChangedEvent != null)
										DomeChangedEvent (value);
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
						ViewDome (); // should hide dome camera
						if (GameChosenEvent != null) {
								GameChosenEvent (this);
						}
				}

				public void Update ()
				{
						if ((!terrain.TestMode) && day == 0) {
								FirstTurn ();
								NextDay ();
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
						if (!DomeCamera)
								return;
						if (!ActiveDome) {
								DomeCamera.enabled = false;
						} else {
								DomeCamera.enabled = true;
								Vector3 ccPos = DomeCamera.transform.position;
								ccPos.x = ActiveDome.transform.position.x;
								ccPos.z = ActiveDome.transform.position.z + CAMERA_ANGLE_Z_TILT;
								DomeCamera.transform.position = ccPos;
								DomeCamera.transform.LookAt (ActiveDome.focus.transform.position);
						}
				}
		#endregion

		#region Events
		
				public delegate void ActiveDomeChangedDel (Dome colony);
		
				/// <summary>An event that gets fired </summary>
				public static event ActiveDomeChangedDel DomeChangedEvent;
				
				public delegate void DayChangedDel (int day);
				
				public static event DayChangedDel DayChangedEvent;
				
				public delegate void OnGameDel (Game game);
				
				public static event OnGameDel GameChosenEvent;
		
				public delegate void ActiveColonistChangedDel (Colonist colonist);

				public static event ActiveColonistChangedDel ColonistChangedEvent;
				
		#endregion

#region ActiveColonist

				void ViewColonist ()
				{
						
				}

				public static Colonist GameActiveColonist {
						get {
								if (!game)
										return null;
								return game.ActiveColonist;
						}	
						set {
								if (game)
										game.ActiveColonist = value;
						}	
				}

				Colonist activeColonist;

				public Colonist ActiveColonist {
						get {
								return activeColonist;
						}
						set {
								activeColonist = value;
								if (ColonistChangedEvent != null)
										ColonistChangedEvent (value);
						}
				}
				
		#endregion
		}
	
}
