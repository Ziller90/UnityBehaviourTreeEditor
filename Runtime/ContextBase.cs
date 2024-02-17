using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder
{
    public class ContextBase
    {
        public GameObject GameObject { get; set; }
        public Transform Transform { get; set; }

        public ContextBase(GameObject gameObject)
        {
            GameObject = gameObject;
            Transform = gameObject.transform;
            AddAddictiveContext(gameObject);
        }

        public virtual void AddAddictiveContext(GameObject gameObject) { }
    }
}