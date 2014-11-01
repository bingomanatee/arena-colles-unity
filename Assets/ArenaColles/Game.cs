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
				public List<Colony> colonies = new List<Colony> ();
				public GameObject colonyTemplate;
				public Camera ColonyCamera;
				Colony activeColony_;
				const float CAMERA_ANGLE_Z_TILT = 20.0f;

				public Colony ActiveColony {
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
						if (Turn == 0) {
								FirstTurn ();
								++Turn;
						}
				}

				#endregion

#region turns

				void FirstTurn ()
				{
						TerraGen.Cell cell = terrain.GetCellNearCenter ();
						GameObject colony = (GameObject)Instantiate (colonyTemplate);
						Colony c = colony.GetComponent<Colony> ();
						c.Colonists = 3;
						colonies.Add (c);
						c.SetCell (cell);
				}
#endregion

		#region Camera

				void FocusOnColony ()
				{
						if (!ActiveColony) {
								ColonyCamera.enabled = false;
						} else {
								ColonyCamera.enabled = true;
								Vector3 ccPos = ColonyCamera.transform.position;
								ccPos.x = ActiveColony.transform.position.x;
								ccPos.z = ActiveColony.transform.position.z + CAMERA_ANGLE_Z_TILT;
								ColonyCamera.transform.position = ccPos;
								ColonyCamera.transform.LookAt (ActiveColony.focus.transform.position);
						}
				}
		#endregion

		#region Events
		
				public delegate void ActiveColonyChangedDelegate (Colony colony);
		
				/// <summary>An event that gets fired </summary>
				public event ActiveColonyChangedDelegate ColonyChangedEvent;

		
		#endregion

		}
}
