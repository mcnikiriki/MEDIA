using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Media.src
{
    public interface IEditingVideoService
    {
        public Task CutVideos();
    }
}