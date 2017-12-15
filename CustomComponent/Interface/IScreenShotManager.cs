using System;
using System.Threading.Tasks;

namespace CustomComponent.Interface
{
    public interface IScreenShotManager
    {

        Task<byte[]> CaptureAsync();

    }
}
