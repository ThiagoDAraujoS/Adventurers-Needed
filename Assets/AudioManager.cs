using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    

    //TODO: Make this a singleton

    void PlaySpellSound(int spellNum, GameObject whoIsCasting)
    {
        switch(spellNum)
        {
            case 0:
                AkSoundEngine.PostEvent("Play_Spell_0", gameObject);
                break;
            case 1:
                AkSoundEngine.PostEvent("Play_Spell_1", gameObject);
                break;
        }
    }
}
