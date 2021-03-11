using UnityEngine;
using System.Linq;
using Hell;
using System.Collections.Generic;
using System;
using Hell.Display;

public class SocketManager : MonoBehaviour {

    public static SocketManager s;
    public Action<Socket> OnSocketConnected = delegate { };
    public Action<Socket> OnCharacterInformationLoaded = delegate { };
    public List<Socket> sockets;

    public List<Socket> Sockets {
        set {
          //  Debug.Log("<color=blue>i was changed!</color>");
            sockets = value; }
        get {
           // Debug.Log("<color=red>"+sockets.Count+"</color>");
            return sockets;
        }
    }






    public List<Plan> GetAllPlans()
    {
        return Sockets.Where(s => s.PawnInfo.IsAlive).Select(s => s.MyPlan).ToList();
    }

    void Awake() {
        s = this;

        DontDestroyOnLoad(gameObject);
    }

    private Socket Instantiate<SocketType>() where SocketType : Socket
    {
        GameObject gameObject = new GameObject();
        Socket ppo = gameObject.AddComponent<SocketType>();
        Sockets.Add(ppo);
        return ppo;
    }

    public Socket GetDisconnectedSocket() {
        return Sockets.FirstOrDefault(socket => socket.TabletInfo == null);
    }

    public Socket GetCharacterlessSocket(){
        return Sockets.FirstOrDefault(socket => socket.CharacterInfo == null || !socket.CharacterInfo);
    }

    public Socket GetEmptySocket() {
        return Sockets.FirstOrDefault(socket => socket.CharacterInfo == null && socket.TabletInfo == null);
    }

    public void SpawnSockets<SocketType>(int amount) where SocketType : Socket
    {
        Sockets = new List<Socket>();

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        for (int i = 0; i < amount; i++)
            Instantiate<SocketType>().transform.parent = transform;
    }

    public void SpawnSockets(int amount) {
        SpawnSockets<Socket>(amount);
    }

    public void SpawnFakeSockets(int amount){
        SpawnSockets<FakeSocket>(amount);
    }

    public Socket this[int tabletId] {
        get {
            return Sockets.First(s => s.TabletInfo.tabletId == tabletId);
        }
    }
}

[Serializable]
public class CharacterInfo
{
    public int playerId;
    public int teamId;
    public string characterName;

    public override string ToString() {
        return ("PlayerID = " + playerId + " TeamID = " + teamId + " Character Name = " + characterName);
    }

    public CharacterInfo() { }

    public CharacterInfo(int playerId,  int teamId, string characterName) {
        this.playerId = playerId;
        this.teamId = teamId;
        this.characterName = characterName;
    }

    public CharacterInfo(int playerId, int teamId) {
        this.playerId = playerId;
        this.teamId = teamId;
    }

    public void SetIds(int player, int team) {
        team = teamId;
        player = playerId;
    }

    public static implicit operator bool(CharacterInfo c) {
        return c!=null && !string.IsNullOrEmpty(c.characterName);
    }
}

[Serializable]
public class TabletInfo
{
    public ClientProxyObject tablet;
    public PhotonPlayer player;
    public int tabletId;

    public TabletInfo(ClientProxyObject tablet, PhotonPlayer player, int tabletId)
    {
      this.tablet = tablet;
      this.player = player;
      this.tabletId = tabletId;
    }
}

public class PawnInfo : IPawn
{
    public Character MyPawn { get; set; }

    public bool IsAlive { get { return MyPawn.IsAlive; } }

    public int MaxLife { get { return MyPawn.MaxLife; } }

    public event Action<int> OnDamage {
        add { MyPawn.OnDamage += value; }
        remove { MyPawn.OnDamage -= value; }
    }
    public event Action<int> OnHeal {
        add{ MyPawn.OnHeal += value; }
        remove { MyPawn.OnHeal -= value; }
    }
    public event Action OnDeath {
        add { MyPawn.OnDeath += value; }
        remove { MyPawn.OnDeath -= value; }
    }

    public PawnInfo(Character myPawn) {
        this.MyPawn = myPawn;
    }
}
