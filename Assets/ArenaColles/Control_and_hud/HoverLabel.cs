using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class HoverLabel : MonoBehaviour
		{
				public GameObject label;
				Color origColor;
				public SpriteRenderer sr;
		
				// Use this for initialization
				void Start ()
				{
						if (sr)
								origColor = sr.renderer.material.color;
						OnMouseExit ();	
				}
	
				// Update is called once per frame
				void Update ()
				{
			
				}

				void OnMouseOver ()
				{
						label.SetActive (true);
						if (sr)
								sr.renderer.material.color = Color.yellow;
				}
	
				void OnMouseExit ()
				{
						label.SetActive (false);
						if (sr)
								sr.renderer.material.color = origColor;
				}
				
				void OnMouseDown ()
				{
						OnMouseExit ();	
				}
				
		}
}