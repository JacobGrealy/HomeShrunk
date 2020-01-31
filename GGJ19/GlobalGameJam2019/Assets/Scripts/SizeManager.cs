using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SizeManager : MonoBehaviour {
    public GameObject player;
    public Transform smallStartTransform;
    public Transform bigStartTransform;
    public SteamVR_Input_Sources Hand; //TODO allow either hand
    public SteamVR_Action_Boolean changeSizeAction;

    private Vector3 bigPlayerScale = new Vector3(1f, 1f, 1f);
    private Vector3 smallPlayerScale = new Vector3(.1f, .1f, .1f);


    private bool previousChangeSizeActive;
    private bool playerIsSmall;

    // Start is called before the first frame update
    void Start() {
        previousChangeSizeActive = false;
        playerIsSmall = false;
    }

    // Update is called once per frame
    void Update() {
        bool changeSizeActive = changeSizeAction.GetState(Hand);
        //If the action was initated this turn
        if(changeSizeActive != previousChangeSizeActive && changeSizeActive) {
            transformSize();
        }
        previousChangeSizeActive = changeSizeActive;
    }

    private void transformSize() {
        if(playerIsSmall) {
            playerIsSmall = false;
            player.transform.position = bigStartTransform.position;
            player.transform.rotation = bigStartTransform.rotation;
            player.transform.localScale = bigPlayerScale;
        } else {
            playerIsSmall = true;
            player.transform.position = smallStartTransform.position;
            player.transform.rotation = smallStartTransform.rotation;
            player.transform.localScale = smallPlayerScale;
        }
    }

    public bool isPlayerSmall() {
        return playerIsSmall;
    }
}
