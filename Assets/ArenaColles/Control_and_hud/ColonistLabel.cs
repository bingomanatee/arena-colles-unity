using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ColonistLabel : MonoBehaviour
		{
				public TextMesh nameLabel;
				Colonist colonist_;
				public SpriteRenderer hoverPanel;
				static Color hoverColor = Color.black;
				static Color textColor = Color.white;

				public Colonist Colonist {
						set {
								colonist_ = value;
								nameLabel.text = value.ColonistName;
						}
						get { return colonist_; }
				}

				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

				public void SetColonist (Colonist c)
				{
						Colonist = c;
				}
				
				void OnMouseUpAsButton ()
				{
						if (Game.game)
								Game.game.Selection = Colonist.gameObject;
				}
				
				void OnMouseEnter ()
				{
						nameLabel.color = hoverColor;
						hoverPanel.enabled = true;
				}
				
				void OnMouseExit ()
				{
						nameLabel.color = textColor;
						hoverPanel.enabled = false;
				}
		}
}