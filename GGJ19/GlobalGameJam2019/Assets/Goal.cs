using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject playerCollider;
    public SizeManager sizeManager;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(playerCollider) && sizeManager.isPlayerSmall())
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
