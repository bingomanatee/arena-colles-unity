using UnityEngine;
using System.Collections;
using WLLForms;

namespace ArenaColles
{
		public class ShowAddRover : MonoBehaviour
		{

				public Button button;
				public TaskPanel makeRover;
	
				// Use this for initialization
				void Start ()
				{
						button.ClickEvent += OnClick;
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
	
				public void ShowRoverAddPanel ()
				{
	
				}

				void OnClick ()
				{
						makeRover.gameObject.SetActive (true);
				}
		}
}