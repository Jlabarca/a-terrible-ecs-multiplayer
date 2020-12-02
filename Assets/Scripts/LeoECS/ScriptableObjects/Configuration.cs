using LeoECS.Monobehaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace LeoECS.ScriptableObjects
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [FormerlySerializedAs("actorView")] public UnitView unitView;
    }
}
