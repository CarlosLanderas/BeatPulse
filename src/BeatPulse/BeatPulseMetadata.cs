using BeatPulse.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatPulse
{
    internal class BeatPulseMetadata
    {
        public string BeatPulsePath { get; set; }
        public IEnumerable<IBeatPulseLivenessRegistration> Livenesses { get; set; }
    }
}
