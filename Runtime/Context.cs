using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder
{
    public class Context
    {
        public GameObject GameObject { get; set; }
        public Transform Transform { get; set; }

        public Context(GameObject gameObject)
        {
            GameObject = gameObject;
            Transform = gameObject.transform;
        }
    }
}