using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController {

    void Init (PlayerIds player);
    void Stop();

    void InitPoint ();
    PlayerIds GetPlayer ();
    float GetPosition ();
    void AddSmashCharge();
    int GetSmashCharge();
    bool IsSmashAvailable ();
    bool IsSmashTriggered ();
    void ConsumeSmash();
    
    
	
}
