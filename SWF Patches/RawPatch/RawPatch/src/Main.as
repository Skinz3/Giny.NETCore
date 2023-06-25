package
{
   import flash.display.Sprite;
   import flash.utils.*;
   
   public class Main extends Sprite
   {
      public function Main()
      {
		 var connectionsHandler:* = getDefinitionByName("com.ankamagames.dofus.kernel.net::ConnectionsHandler");

         var version:* = new (getDefinitionByName("com.ankamagames.dofus.network.types.version::Version") as Class)();
		 
         version.initVersion(2, 5, 8, 0, 0);
		 
		 var BuildInfos:* = getDefinitionByName("com.ankamagames.dofus::BuildInfos");
		
		 var xmlConfig:* = getDefinitionByName("com.ankamagames.jerakine.data::XmlConfig");
		 
		 var authManager:* = getDefinitionByName("com.ankamagames.dofus.logic.connection.managers::AuthentificationManager").getInstance();
		 
		 var password:* = authManager.loginValidationAction.password;
		 
         var username:* = authManager.loginValidationAction.username;
		 
         var serverId:* = authManager.loginValidationAction.serverId;
		 
         var autoSelect:* = authManager.loginValidationAction.autoSelectServer;
		 
		 var aes:* = authManager.initAESKey();
		 
	 
         var array:ByteArray = new ByteArray();
         array.writeUTF(username);
         array.writeUTF(password);
		 array.writeBytes(aes, 0, 32);
         
		
         var cred:Vector.<int> = new Vector.<int>();
		 array.position = 0;
         while(array.bytesAvailable != 0)
         {
            cred.push(array.readByte());
         }
       
         var identificationMessage:* = new (getDefinitionByName("com.ankamagames.dofus.network.messages.connection::IdentificationMessage") as Class)();
         identificationMessage.initIdentificationMessage(version,"fr",cred,serverId,autoSelect,false,false,0,new Vector.<uint>());
         connectionsHandler.getConnection().send(identificationMessage);
      }
   }
}
