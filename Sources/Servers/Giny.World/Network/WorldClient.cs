using Giny.Core;
using Giny.Core.Network;
using Giny.Core.Network.Messages;
using Giny.Protocol.IPC.Types;
using Giny.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Giny.Protocol.Enums;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.Extensions;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Breeds;
using Giny.World.Records;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Characters;
using Giny.World.Records.Accounts;
using Giny.ORM;
using Giny.World.Managers.Items;
using Giny.World.Records.Items;
using Giny.World.Managers.Items.Collections;
using Giny.Core.DesignPattern;
using Giny.World.Logging;

namespace Giny.World.Network
{
    public class WorldClient : Client
    {
        public Account Account
        {
            get;
            set;
        }
        public WorldAccountRecord WorldAccount
        {
            get;
            set;
        }

        public List<CharacterRecord> Characters
        {
            get;
            private set;
        }
        public bool InGame
        {
            get
            {
                return CharacterSelected && Character.Record.InGameContext;
            }
        }

        public bool CharacterSelected => Character != null;

        public Character Character
        {
            get;
            set;
        }

        public WorldClient(Socket socket) : base(socket)
        {
            base.Send(new HelloGameMessage());
        }
        public WorldClient()
        {

        }

        public override void OnConnected()
        {
            throw new NotImplementedException();
        }

        public override void OnConnectionClosed()
        {
            try
            {
                OnDisconnected();
            }
            catch (Exception ex)
            {
                Logger.Write("Unable to disconnect World Client : " + ex, Channels.Warning);
            }
        }

        public override void OnFailToConnect(Exception ex)
        {
            throw new NotImplementedException();
        }
        public override void OnMessageReceived(NetworkMessage message)
        {
            if (ConfigFile.Instance.LogProtocol)
                Logger.Write("(World) Received " + message, Channels.Info);

            ProtocolMessageManager.HandleMessage(message, this);
        }

        public override void OnSended(IAsyncResult result)
        {
            if (ConfigFile.Instance.LogProtocol)
                Logger.Write("(World) Send " + result.AsyncState);
        }

        public override void OnMessageUnhandled(NetworkMessage message)
        {
            if (ConfigFile.Instance.LogProtocol)
                Logger.Write(string.Format("No Handler: ({0}) {1}", message.MessageId, message.ToString()), Channels.Warning);

        }

        public override void OnHandlingError(NetworkMessage message, Delegate handler, Exception ex)
        {
            Logger.Write(string.Format("Unable to handle message {0} {1} : '{2}'", message.ToString(), handler.Method.Name, ex.ToString()), Channels.Warning);
            LogManager.Instance.OnError(this, message, ex);
        }

        public void SendCharactersList()
        {
            CharacterBaseInformations[] characters = new CharacterBaseInformations[Characters.Count];

            for (int i = 0; i < Characters.Count; i++)
            {
                characters[i] = Characters[i].GetCharacterBaseInformations();
            }

            var message = new CharactersListMessage()
            {
                characters = characters,
                hasStartupActions = false,
            };
            Send(message);
        }

        public override void OnDisconnected()
        {
            WorldServer.Instance.RemoveClient(this);
            Dispose();
        }

        private void Dispose()
        {
            Character?.OnDisconnected();
        }
        private void LoadWorldAccount()
        {
            this.WorldAccount = WorldAccountRecord.GetWorldAccount(this.Account.Id);

            if (this.WorldAccount == null)
            {
                this.WorldAccount = WorldAccountRecord.Create(this.Account.Id);
                this.WorldAccount.AddElement();
            }
        }
        [WIP]
        public void OnAccountReceived()
        {
            Send(new AccountInformationsUpdateMessage(DateTime.Now.AddYears(3).GetUnixTimeStampDouble()));

            LoadWorldAccount();
            SendBasicTime();
            Characters = CharacterRecord.GetCharactersByAccountId(Account.Id);
            Send(new ServerSettingsMessage("fr", 0, 0, false, 1, 200, true));

            SendServerOptionalFeatures(OptionalFeaturesEnum.PVP_KIS);

            Send(new ServerSessionConstantsMessage(new ServerSessionConstant[]
           {
               new ServerSessionConstantInteger()
               {
                   id = (short)ServerConstantTypeEnum.KOH_DURATION,
                   value = 7200000,
               },
               new ServerSessionConstantInteger()
               {
                   id = (short)ServerConstantTypeEnum.UNKOWN_6,
                   value = 10,
               },
                new ServerSessionConstantInteger()
               {
                   id = (short)ServerConstantTypeEnum.UNKNOW_7,
                   value = 2000,
               }
           }));

            Send(new AccountCapabilitiesMessage(Account.Id, true, BreedManager.Instance.AvailableBreedsFlags,
                BreedManager.Instance.AvailableBreedsFlags, 0, true));

            Send(new TrustStatusMessage(true, true));

            if (Characters.Any(x => x.InGameContext))
            {
                this.Disconnect();
            }
            // client.Send(new HaapiSessionMessage());
        }

        public CharacterRecord GetCharacter(long characterId)
        {
            return Characters.FirstOrDefault(x => x.Id == characterId);
        }

        public void SendServerOptionalFeatures(params OptionalFeaturesEnum[] optionalFeaturesEnum)
        {
            Send(new ServerOptionalFeaturesMessage(optionalFeaturesEnum.Select(x => (int)x).ToArray()));
        }

        public void SendBasicTime()
        {
            var offset = (short)TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalMinutes;
            Send(new BasicTimeMessage(DateTime.Now.GetUnixTimeStampLong(), offset));
        }


    }
}
