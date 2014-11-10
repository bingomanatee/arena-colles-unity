using UnityEngine;
using System.Collections;

namespace WLLForms
{
		public class CheckedFeedbackTest : MonoBehaviour
		{
				public Checkbox cb;
				public TMPro.TextMeshPro flag;
				// Use this for initialization
				void Start ()
				{
						cb.CheckChangedEvent += OnCheckChanged;
						OnCheckChanged (cb.IsChecked);
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

				void OnCheckChanged (bool isChecked)
				{
						Debug.Log ("Check changed to " + isChecked);
						flag.gameObject.SetActive (isChecked);
				}
		}
}