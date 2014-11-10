using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class CloseColony : MonoBehaviour
		{

				// Use this for initialization
				void Start ()
				{

				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		
	
				void OnMouseDown ()
				{
						Game.game.ActiveDome = null;
				}
		}
}