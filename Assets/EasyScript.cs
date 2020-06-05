using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EasyScript : MonoBehaviour {

	public float eHiscore;

	public Text easHiscore;

    void Update() {
        
        easHiscore.text = "Easy: " + eHiscore;

        eHiscore += Time.deltaTime;
    }
}
