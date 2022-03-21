using System;
using System.Collections;

namespace ChaosMission.Player.Moving.Controller
{
    public interface ICoroutineActivator
    {
        public void ActivateCoroutine(Func<IEnumerator> coroutine);
        public void ActivateCoroutine<TParam>(Func<TParam, IEnumerator> coroutine, TParam paramValue);
        // public Func<IEnumerator> CoroutineAction { get; set; }
    }
}