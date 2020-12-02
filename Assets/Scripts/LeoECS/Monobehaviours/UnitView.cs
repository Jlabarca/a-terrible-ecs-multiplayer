using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Events;

namespace LeoECS.Monobehaviours
{
    public class UnitView : MonoBehaviour
    {
        public EcsEntity Entity;
		public ActorTemplate template;

		//references
		private Animator animator;
		private SpriteRenderer selectionCircle;

		public UnityAction<UnitView> OnDie;

		void Awake ()
		{
			animator = GetComponent<Animator>();
		}
    }
}
