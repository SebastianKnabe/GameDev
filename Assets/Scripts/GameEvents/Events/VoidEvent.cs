using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}
