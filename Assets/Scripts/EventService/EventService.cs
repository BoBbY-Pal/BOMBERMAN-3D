using System;
using Utilities;


    public class EventService : MonoGenericSingleton<EventService>
    {
        public Action GamePaused;
        public Action GameResumed;

        private void Start()
        {
            
        }
    }
