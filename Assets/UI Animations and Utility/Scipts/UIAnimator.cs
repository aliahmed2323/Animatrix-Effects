using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UIAnimatrix
{
    public class UIAnimator : MonoBehaviour
    {
        Action ghfg;
            AnimatrixButton bb;
        private void Start()
        {
            ghfg = test;
            bb = GetComponent<AnimatrixButton>();
            bb.onClick.AddListener(ghfg);
            bb.onClick.AddListener(()=> Debug.Log("fdfsdf"));
        }

        void test()
        {
            /*Debug.Log("button works");*/
        }
    }
}
