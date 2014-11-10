using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class Flag : MonoBehaviour
		{
				public TextMesh label;

		#region loop
				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		#endregion


				Cell cell_;

				public Cell Cell {
						get { return cell_;}
						set { cell_ = value;
								if (label)
										label.text = "Cell " + value.ToString ();}
				}

		}
}