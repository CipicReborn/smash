using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController {

    void Init(Players player);
    void InitPosition();
    Players GetPlayer();
    float GetPosition ();
    bool GetSmash ();
    
	
}
