using Giny.Core.IO.Configuration;
using Giny.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Uplauncher
{
    public enum UpdateStatusEnum
    {
        None,
        VersionCheck,
        Downloading,
        Ready,
        Error,
    }
    public class ClientUpdater
    {
        public event Action<UpdateStatusEnum, object?> OnStatusUpdate;

        private UplConfig Config => ConfigManager<UplConfig>.Instance;

        public void Update()
        {
            try
            {
                OnStatusUpdate?.Invoke(UpdateStatusEnum.VersionCheck, null);

                var remoteVersion = AuthApi.GetRemoteVersion();

                if (remoteVersion != Config.LocalVersion)
                {

                }
                else
                {
                    OnStatusUpdate?.Invoke(UpdateStatusEnum.Ready, null);
                }
            }
            catch (Exception ex)
            {
                OnStatusUpdate?.Invoke(UpdateStatusEnum.Error, ex);
            }
        }
    }
}
