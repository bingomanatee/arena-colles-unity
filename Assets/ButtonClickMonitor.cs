using UnityEngine;
using System.Collections;
using TMPro;

namespace WLLForms
{
		public class ButtonClickMonitor : MonoBehaviour
		{
		
				public Button button;
				public TextMeshPro feedback;
				int ClickCount = 0;

				// Use this for initialization
				void Start ()
				{
						button.ClickEvent += OnClick;
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

				void OnClick ()
				{
						feedback.text = string.Format ("button clicked {0} times", ++ClickCount);
				}
		}
}