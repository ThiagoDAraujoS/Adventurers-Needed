using Hell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class TbCharacterSelectManager : MonoBehaviour
{
    public static TbCharacterSelectManager s;
    public TeamColor teamColorList;
    public CharactersPrefabList modelPrefabList;
    public float swipeLimit;
    private int index = 0;
    private int Team { get; set; }
    public Transform[] positions;
    private CharacterSelectTracker[] characters;
    public Text playerTag;
    public Image[] flags;
    public Button readyButton;
    public GameObject[] arrow;
    public ParticleSystem fire;

    public Text teamSign;

    /// <summary>
    /// The index of the player in the middle, when this variable is changed the pawns move around
    /// </summary>
    public int Index
    {
        get { return index; }
        set
        {
            index = (int)Mathf.Repeat(value, characters.Length);
            playerTag.text = characters[index].name;
        }
    }

    /// <summary>
    /// The a certain Index is avaliable or not to be picked
    /// </summary>
    public bool[] characterAvaliabilityList;

    /// <summary>
    /// Return a valid index number
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private int GetIndex(int index) {
        return (int)Mathf.Repeat(index, modelPrefabList.list.Length);
    }

   void Awake()
    {
        s = this;
    }

    /// <summary>
    /// Then this is set the arrows in the screen vanishes
    /// </summary>
    public void BlockArrows() {
        foreach (var item in arrow)
            item.SetActive(false);
    }

    /// <summary>
    /// Set if that character is ok to be picked or not
    /// </summary>
    /// <param name="characterName">the character</param>
    /// <param name="value">if its ok to be picked</param>
    public void SetAvaliability(string characterName, bool value)
    {
        int? resultIndex = null;
        for (int i = 0; i < characters.Length && resultIndex == null; i++)
            if(characterName == characters[i].name)
                resultIndex = i;
        if (resultIndex != null && index == resultIndex)
        {
            readyButton.interactable = value;
        }
        characterAvaliabilityList[resultIndex.Value] = value;
    }

    /// <summary>
    /// Return if a character id is avaliable to be picked
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public bool GetAvaliability(int ID){
        return characterAvaliabilityList[ID];
    }

    /// <summary>
    /// Start method
    /// </summary>
    void Start() {

        characters = new CharacterSelectTracker[modelPrefabList.list.Length];
        characterAvaliabilityList = new bool[characters.Length];
        for (int i = 0; i < characterAvaliabilityList.Length; i++)
            characterAvaliabilityList[i] = true;

        for (int i = 0; i < modelPrefabList.list.Length; i++) {
            characters[i] = Instantiate(modelPrefabList.list[i]).GetComponent<CharacterSelectTracker>();
            characters[i].name = modelPrefabList.list[i].name;

            if (i > 4)
            {
                characters[i].transform.position = positions[4].position;
                characters[i].target = positions[4];
            }
            else
            {
                characters[i].transform.position = positions[i].position;
                characters[i].target = positions[i];
            }
        }

        Index = 2;

        Team =  0;
        SetMainIndex();
        if(Team == 0)
            ToggleTeam();

    }

    /// <summary>
    /// Toggle the team colors
    /// </summary>
    public void ToggleTeam()
    {
        if(ServerProxyObject.s != null)
            ServerProxyObject.s.photonView.RPC("ToggleTeam", PhotonTargets.MasterClient);
    }

    /// <summary>
    /// Set the team colors
    /// </summary>
    /// <param name="value"></param>
    public void SetTeam(int value, int teamsize)
    {
        if(teamsize == 1)
            teamSign.text = "FFA";
        else
            teamSign.text = teamsize.ToString() + "-" + teamsize.ToString(); 

        Team = value;
        foreach (var item in flags)
            item.color = teamColorList.list[Team];
    }

    /// <summary>
    /// an object to match another objext in scale and position
    /// </summary>
    /// <param name="target"></param>
    /// <param name="baseT"></param>
    public void TransformObject(Transform target, Transform baseT) {
        target.position = baseT.position;
        target.rotation = baseT.rotation;
        target.localScale = baseT.localScale;
    }

    /// <summary>
    /// Set index
    /// </summary>
    public void SetMainIndex() {

        for (int i = 0, ci = GetIndex(index - 2 + i); i < 5; i++, ci = GetIndex(index - 2 + i)) {
            if((characters[ci].target == positions[4] && i == 0) ||
               (characters[ci].target == positions[0] && i == 4))
                   characters[ci].transform.position = positions[i].position;
            characters[ci].target = positions[i];

        }
    }
    public void NextCharacter()
    {
        Index--;
        readyButton.interactable = GetAvaliability(index);
        SetMainIndex();
        
    }
    public void PrevCharacter()
    {
        Index++;
        readyButton.interactable = GetAvaliability(index);
        SetMainIndex();
    }

    /// <summary>
    /// Select that character
    /// </summary>
    public void SelectCharacter()
    {
        int playerID = PhotonNetwork.player.ID - 2;
        float h, s, v;
        Color.RGBToHSV(teamColorList.list[Team], out h, out s, out v);
        v *= 1.1f;
        fire.startColor =  Color.HSVToRGB(h,s,v);
        fire.Play();
        characters[index].playerBase.GetComponent<Renderer>().material.SetColor("_Color", teamColorList.list[Team]);
        if (ServerProxyObject.s != null)
            ServerProxyObject.s.photonView.RPC("ChooseCharacter", PhotonTargets.MasterClient, playerID, characters[index].name);

    }

   

}
