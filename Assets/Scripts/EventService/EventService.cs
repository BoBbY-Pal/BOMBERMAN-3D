using System;
public class EventService 
    {
        public static Action GamePaused;
        public static Action GameResumed;
        public static Action GameOver;
        public static Action GameWon;
        public static Action<int> UpdateScore;
        
    }
