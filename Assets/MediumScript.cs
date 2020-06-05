using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MediumScript : MonoBehaviour {

	public float mHiscore;

	public Text medHiscore;

    void Update() {
        
        medHiscore.text = "Medium: " + mHiscore;

        mHiscore += Time.deltaTime;
    }
}
