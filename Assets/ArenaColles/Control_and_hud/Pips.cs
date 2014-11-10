using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Pips : MonoBehaviour
		{
				public SpriteRenderer[] images = new SpriteRenderer[9];
				public Dictionary<int, SpriteRenderer> map = new Dictionary<int, SpriteRenderer> ();
				int value_ = 0;
				public	TextMesh valueText;
				public const int EXTENT = 4;
				bool mapSet = false;

				public int pipValue {
						set {
								if (valueText)
										valueText.text = value.ToString ();
										
								value_ = Mathf.Clamp (value, -EXTENT, EXTENT);
								if (!mapSet)
										InitMap ();
								foreach (int offNum in Utils.Range (-EXTENT, EXTENT)) {
										if (map.ContainsKey (offNum))
												map [offNum].enabled = false;
								}
								if (map.ContainsKey (value_))
										map [value_].enabled = true;
						}
						
						get { return value_; }
				}

				// Use this for initialization
				void Start ()
				{
						InitMap ();
						pipValue = 0;
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

				void InitMap ()
				{
						for (int i = 0; i < images.Length; i++) {
								SpriteRenderer image = images [i];
								int key = i - EXTENT;
								map [key] = image;
						}
						mapSet = true;
				}
		}
		
}