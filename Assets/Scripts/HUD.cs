using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] Image barkIcon;

    public void barked(){
        barkIcon.enabled = false;
    }

    public void canBark(){
        barkIcon.enabled=true;
    }
    
    void Awake(){
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
