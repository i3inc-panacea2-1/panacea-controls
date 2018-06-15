using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Controls
{
    public class AudioManager : IAudioManager
    {
        public float SpeakersVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float MicrophoneVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void MaxDevicesExceptDefault()
        {
            throw new NotImplementedException();
        }
    }
}
