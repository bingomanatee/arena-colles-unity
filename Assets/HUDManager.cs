using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class HUDManager : MonoBehaviour
		{
				public static HUDManager manager;
				public SpriteRenderer closeBox;
				public TextMesh ColonyTitle;
				public SpriteRenderer colonistButton;
				bool registered = false;

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

				void ReflectColony (Colony c)
				{
						if (c == null) {
								closeBox.renderer.enabled = false;
								ColonyTitle.renderer.enabled = false;
								colonistButton.renderer.enabled = false;
								colonistButton.gameObject.SetActive (false);
						} else {
								closeBox.renderer.enabled = true;
								ColonyTitle.renderer.enabled = true;
								colonistButton.renderer.enabled = true;
								colonistButton.gameObject.SetActive (true);
								ColonyTitle.text = c.ColonyName;
						}
				}

		#endregion

		}
}