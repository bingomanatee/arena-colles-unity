using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ColonyHUDmanager : MonoBehaviour
		{
				public static ColonyHUDmanager manager;
				public SpriteRenderer closeBox;
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
						ReflectColony (null);
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
								Game.game.ColonyChangedEvent += ReflectColony;
								registered = true;
						}
				}

				void ReflectColony (Dome c)
				{
						HideAllPanels ();
						
						if (!c) {
								closeBox.renderer.enabled = false;
								ColonyTitle.renderer.enabled = false;
								colonistButton.renderer.enabled = false;
								colonistButton.gameObject.SetActive (false);
								o2panel.gameObject.SetActive (false);
				
								hudCamera.enabled = false;
						} else {
								closeBox.renderer.enabled = true;
								ColonyTitle.renderer.enabled = true;
								colonistButton.renderer.enabled = true;
								colonistButton.gameObject.SetActive (true);
								ColonyTitle.text = c.ColonyName;
								o2panel.colony = c;
								o2panel.gameObject.SetActive (true);
								hudCamera.enabled = true;
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
										g.GetComponent<ColonistList> ().ReflectColony ();
								}
				}

				public void ShowEquipment ()
				{
						HideAllPanels ();
			
						foreach (GameObject g in panels)
								if (g.GetComponent<EquipmentList> ()) {
										g.SetActive (true);
										g.GetComponent<EquipmentList> ().ReflectColony ();
								}
				}
		}
}