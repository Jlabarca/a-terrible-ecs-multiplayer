using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Events;

namespace LeoECS.Monobehaviours
{
    public class ActorView : MonoBehaviour
    {
        public EcsEntity Entity;
		public ActorTemplate template;

		//references
		private Animator animator;
		private SpriteRenderer selectionCircle;

		public UnityAction<ActorView> OnDie;

		void Awake ()
		{
			animator = GetComponent<Animator>();
		}
    }
}
