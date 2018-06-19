using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Controls
{
    public class AudioManager : IAudioManager
    {
        //todo add implementation
        public float SpeakersVolume { get ; set; }
        public float MicrophoneVolume { get; set; }

        public void MaxDevicesExceptDefault()
        {
            
        }
    }
}
