using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Game : MonoBehaviour
		{
		
				public static Game game;
				public int Turn = 0;
				public TerraGen terrain;
				public List<Dome> colonies = new List<Dome> ();
				public GameObject colonyTemplate;
				public Camera ColonyCamera;
				Dome activeColony_;
				const float CAMERA_ANGLE_Z_TILT = 20.0f;
				public bool StartAtCenter = false;

				public Dome ActiveDome {
						set {
								activeColony_ = value;
								if (ColonyChangedEvent != null)
										ColonyChangedEvent (value);
								FocusOnColony ();
						}
						get { return activeColony_;}
				}

				#region loop

				public void Start ()
				{
						terrain = GetComponent<TerraGen> ();
						game = this;
						FocusOnColony (); // should hide camera
				}

				public void Update ()
				{
						if ((!terrain.TestMode) && Turn == 0) {
								FirstTurn ();
								++Turn;
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
						c.QuantityOfPlastics = 100;
						
						colonies.Add (c);
						c.SetCell (cell);
				}
#endregion

		#region Camera

				void FocusOnColony ()
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
				public event ActiveColonyChangedDelegate ColonyChangedEvent;

		
		#endregion

		}
}
