using System;
using Core.SceneObjects;

public class InteractEvents
{
    public static Action<CubeMoney> OnMoneyInteract;

    public static Action<LetCube, CubeMoney> OnLetInteract;

}
