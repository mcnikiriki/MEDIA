using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Media.src
{
    public interface IDownloadAudioService
    {
        public Task DownloadAudios();
    }
}