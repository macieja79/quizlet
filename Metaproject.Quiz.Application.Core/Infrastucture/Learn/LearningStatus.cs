﻿namespace Metaproject.Quiz.Application.Core
{
    public class LearningStatus
    {
        public int New { get; set; }
        public int Learning { get; set; }
        public int Memorized { get; set; }

        public bool IsAnythingToLearn => New > 0 || Learning > 0;
    }
}