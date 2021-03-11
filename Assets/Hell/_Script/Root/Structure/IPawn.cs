using System;
using System.Collections;
using Hell.Display;

namespace Hell
{
    public interface IPawn
    {
        bool IsAlive { get; }
        int MaxLife { get; }
        event Action<int> OnDamage;
        event Action<int> OnHeal;
        event Action OnDeath;
    }
}