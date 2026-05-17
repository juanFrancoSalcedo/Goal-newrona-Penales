using B_Extensions;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    [Serializable]
    public class AdminListenerReference
    {
        [RequireBadInterface(typeof(IAdminListener))]
        [SerializeField] private MonoBehaviour listener;

        public IAdminListener Interface => listener as IAdminListener;
    }

    public class AdminManager : Singleton<AdminManager>
    {
        [SerializeField] private List<AdminListenerReference> listeners = new List<AdminListenerReference>();

        private List<IAdminListener> _adminListeners = new List<IAdminListener>();

        private new void Awake()
        {
            base .Awake();
            foreach (var reference in listeners)
            {
                if (reference.Interface != null)
                {
                    _adminListeners.Add(reference.Interface);
                }
            }
        }

        private void OnEnable() => FileSelectorService.OnFileLoaded += NotifyAll;

        private void OnDisable() => FileSelectorService.OnFileLoaded -= NotifyAll;

        public void NotifyAll()
        {
            foreach (var listener in _adminListeners)
            {
                listener.UpdateBehaviour();
            }
        }
    }
}