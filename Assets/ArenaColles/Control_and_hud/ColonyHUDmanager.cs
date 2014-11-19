using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ColonyHUDmanager : MonoBehaviour
		{
				public static ColonyHUDmanager manager;
				public GameObject closeButton;
				public TextMesh ColonyTitle;
				public SpriteRenderer colonistButton;
				public O2Panel o2panel;
				bool registered = false;
				public Camera hudCamera;
				public GameObject[] panels = new GameObject[1];

				// Use this for initialization
				void Start ()
				{
						manager = this;
						ReflectDome (null);
						Game.DomeChangedEvent += ReflectDome;
				}
	
				// Update is called once per frame
				void Update ()
				{
						if (!registered)
								register ();
				}

		#region colony

				void register ()
				{
						if (Game.game) {
								Game.DomeChangedEvent += ReflectDome;
								registered = true;
						}
				}

				void ReflectDome (Dome c)
				{
						HideAllPanels ();
						Debug.Log ("ColonyHUDManager reflecting dome " + c);
						if (!c) {
								closeButton.SetActive (false);
								ColonyTitle.renderer.enabled = false;
								colonistButton.renderer.enabled = false;
								colonistButton.gameObject.SetActive (false);
								o2panel.gameObject.SetActive (false);
								hudCamera.gameObject.SetActive (false);
								Debug.Log ("Hiding Dome HUD camera");
						} else {
								closeButton.SetActive (true);
								ColonyTitle.renderer.enabled = true;
								colonistButton.renderer.enabled = true;
								colonistButton.gameObject.SetActive (true);
								ColonyTitle.text = c.DomeName;
								o2panel.gameObject.SetActive (true);
								hudCamera.gameObject.SetActive (true);
								Debug.Log ("Showing Dome HUD camera");
						}
				}

		#endregion
		
				void HideAllPanels ()
				{
						foreach (GameObject g in panels)
								g.SetActive (false);
				}

				public void ShowColonists ()
				{
						HideAllPanels ();
			
						foreach (GameObject g in panels)
								if (g.GetComponent<ColonistList> ()) {
										g.SetActive (true);
										g.GetComponent<ColonistList> ().ReflectDome ();
								}
				}

				public void ShowEquipment ()
				{
						HideAllPanels ();
			
						foreach (GameObject g in panels)
								if (g.GetComponent<EquipmentList> ()) {
										g.SetActive (true);
										g.GetComponent<EquipmentList> ().ReflectDome (Game.GameActiveDome);
								}
				}

				public void ShowTasks ()
				{
						HideAllPanels ();
			
						foreach (GameObject g in panels)
								if (g.GetComponent<TaskList> ()) {
										g.SetActive (true);
										g.GetComponent<TaskList> ().ReflectDome ();
								}
				}
		}
}