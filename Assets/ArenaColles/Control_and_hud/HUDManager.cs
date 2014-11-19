using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class HUDmanager : MonoBehaviour
		{
				public GameObject closeButton;
				public TextMesh Title;
				bool registered = false;
				public Camera hudCamera;
				public GameObject[] panels = new GameObject[0];

				// Use this for initialization
				void Start ()
				{
				}
	
				// Update is called once per frame
				void Update ()
				{
				}
		
				protected void HideAllPanels ()
				{
						foreach (GameObject g in panels)
								g.SetActive (false);
				}

		}
}