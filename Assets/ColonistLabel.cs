using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ColonistLabel : MonoBehaviour
		{
				public TextMesh nameLabel;
				Colonist colonist_;

				public Colonist colonist {
						set {
								colonist_ = value;
								Debug.Log ("Creating label for " + value.ColonistName);
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
						colonist = c;
				}
		}
}