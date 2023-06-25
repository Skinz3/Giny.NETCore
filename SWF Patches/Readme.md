# Instructions de mise à niveau 

## Patch

Exporter les scripts .as a partir du Dofus invoker:
```
com.ankamagames.dofus.datacenter
com.ankamagames.dofus.network.messages
com.ankamagames.dofus.network.enums
com.ankamagames.dofus.network.types
```

Patch ( déprécié) : https://www.youtube.com/watch?v=iYpeW4VqFw0

* Kernel.as  (optionel) 
```actionscript
if(buildType != -1 && buildType > -1)
{
    BuildInfos.VERSION.buildType = buildType;
}
```
* ServerControlFrame.as
Remplacer le paramètre content par rdMsg.content
``` l.loadBytes(rdMsg.content,lc);```
```actionscript
if (Kernel.getWorker().contains(AuthentificationFrame)) // a remplacer par if (false)
{
    _log.error("Impossible de traiter le paquet RawDataMessage durant cette phase.");
    return (false);
};
```   
                
* Signature.as
```actionscript
public function verify(input:IDataInput, output:ByteArray) : Boolean
{
     return true;
}
```

* AuthentificationManager.as
```actionscript
public function initAESKey() : void
{
    this._AESKey = this.generateRandomAESKey();
}
``` 
transformer en

```actionscript
public function initAESKey() : ByteArray
{
    this._AESKey = this.generateRandomAESKey();
     return this._AESKey;
}
```

## Protocol

* Fixer les éventuels nom de variables invalide en C# (@base)
* Apres la génération des D2O Classes utiliser Giny.D2O pour vérifiez les éventuels field manquant après la génération.
* Verifier l'id du Raw data message dans MessageReceiver.


* Dans CharacterCreationRequestMessage
```csharp
 colors = new int[5];
```

* Dans ObjectFeedMessage
```csharp
 uint _mealLen = (uint)reader.ReadUShort();
            meal = new ObjectItemQuantity[_mealLen];  <------------
            for (uint _i2 = 0;_i2 < _mealLen;_i2++)
```

* Dans GameRolePlayGroupMonsterInformations
 
static infos a rajouter (non static)

## Database

* Une fois les erreurs de compilation de Giny.World résolues, utiliser le database synchroniser.
* Utiliser le module database patcher


## Edit client to get protocol values
````pcode
 getlex QName(PackageNamespace("com.ankamagames.jerakine.logger"),"Log")
pushstring "test"
callproperty QName(PackageNamespace(""),"getLogger"), 1
setlocal2
getlocal2
pushstring "Value is "
getlocal0
getproperty QName(PackageNamespace(""),"total")
add
pushstring ", length "
add
pushstring "..."
add
callpropvoid QName(Namespace("com.ankamagames.jerakine.logger:Logger"),"warn"), 1
````