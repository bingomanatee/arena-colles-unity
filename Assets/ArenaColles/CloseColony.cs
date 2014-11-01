using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class CloseColony : MonoBehaviour
		{

				public GameObject label;
				Color origColor;
				SpriteRenderer sr;

				// Use this for initialization
				void Start ()
				{
						sr = GetComponent<SpriteRenderer> ();
						origColor = sr.renderer.material.color;
						OnMouseExit ();

				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

				void OnMouseOver ()
				{
						if (sr.enabled) {
								label.SetActive (true);
								sr.renderer.material.color = Color.yellow;
						}
				}

				void OnMouseExit ()
				{
						{
								label.SetActive (false);
								sr.renderer.material.color = origColor;
						}
				}

				void OnMouseDown ()
				{
						Game.game.ActiveColony = null;
						OnMouseExit ();
				}
		}
}