using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HardScript : MonoBehaviour {

	public float hHiscore;

	public Text harHiscore;

    void Update() {
        
        harHiscore.text = "Hard: " + hHiscore;

        hHiscore += Time.deltaTime;
    }
}
