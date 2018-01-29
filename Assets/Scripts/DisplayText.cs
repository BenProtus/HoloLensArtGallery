using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class DisplayText : MonoBehaviour {

    public GameObject painting;
    public GameObject text;
    public GameObject panel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (painting.activeInHierarchy)
        {
            panel.SetActive(true);    
            text.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
            text.SetActive(false);
        }
	}
}
