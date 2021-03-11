using System.Collections;
using Hell;
using Hell.Display;
using UnityEngine;

public class FakeSocket : Socket
{
    void Start()
    {
        name = "FakeSocket";
    }
    public override IEnumerator WaitForPlan() {
        MyPlan = PlanGen.BuildPlan(PawnInfo.MyPawn);
        yield return null;
    }
}

