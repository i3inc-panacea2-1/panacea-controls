using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Controls
{
    public interface IAudioManager
    {

        void MaxDevicesExceptDefault();

        /// <summary>
        /// Set speakers volume. Minimum 0, maximum 1
        /// </summary>
        float SpeakersVolume { get; set; }

        /// <summary>
        /// Set microphones volume. Minimum 0, maximum 1
        /// </summary>
        float MicrophoneVolume{ get; set; }
    }
}
