using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.AS3;
using Giny.AS3.Converter;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using Giny.Core.IO;

namespace Giny.ProtocolBuilder.Converters
{
    public abstract class SerializableConverter : DofusConverter
    {
        public abstract string BaseClassName
        {
            get;
        }
        public override string GetImplements()
        {
            return string.Empty;
        }

        private AS3Method[] GetCtors()
        {
            List<AS3Method> results = new List<AS3Method>();

            AS3Method ctor = File.CreateConstructor(AS3AccessorsEnum.@public);

            if (ctor.Parameters.Count > 0)
            {
                AS3Method emptyCtor = File.CreateEmptyConstructor();
                results.Add(emptyCtor);
            }
            results.Add(ctor);

            return results.ToArray();
        }

        private List<AS3Variable> GetCtorParametersInBase(AS3File current, Dictionary<string, AS3File> context)
        {
            var targetName = current.Extends;

            if (targetName == BaseClassName)
            {
                return new List<AS3Variable>();
            }

            var target = context[targetName];

            var targetCtor = target.GetMethods(x => x.Name == "init" + target.ClassName).FirstOrDefault();

            var results = GetCtorParametersInBase(target, context);

            if (targetCtor != null)
            {
                DofusHelper.DeductCtorFieldTypes(BaseClassName, current, targetCtor.Parameters, context);
                results.AddRange(targetCtor.Parameters);
            }

            return results;

        }
        public void BuildConstructor(Dictionary<string, AS3File> context)
        {
            if (File.Extends != BaseClassName)
            {
                var ctor = MethodsToWrite.FirstOrDefault(x => x.IsConstructor && x.Parameters.Count > 0);

                bool addCtor = ctor == null;

                if (addCtor)
                {
                    ctor = File.CreateEmptyConstructor();
                }
                var additionalParameters = GetCtorParametersInBase(this.File, context);

                if (additionalParameters.Count > 0)
                {
                    ctor.Parameters.AddRange(additionalParameters);
                }


                foreach (var parameter in ctor.Parameters.ToArray().Reverse())
                {
                    var count = ctor.Parameters.Count(x => x.Name == parameter.Name);

                    if (count > 1)
                    {
                        ctor.Parameters.Remove(parameter);
                    }
                }

                ctor.Expressions.Clear();

                foreach (var param in ctor.Parameters)
                {
                    VariableNameExpression expr = new VariableNameExpression(param.Name);
                    ctor.Expressions.Add(new AssignationExpression("this." + param.Name, expr));
                }

                if (addCtor && ctor.Parameters.Count > 0)
                {
                    MethodsToWrite.Insert(1, ctor);
                }

            }
        }

        private void GlobalVariableRename(string oldName, string newName)
        {
            File.RenameField(oldName, newName);

            foreach (var method in MethodsToWrite)
            {
                method.RenameVariable(oldName, newName);
            }
        }
        public override void PostPrepare()
        {
            GlobalVariableRename("lock", "@lock");
            GlobalVariableRename("base", "@base");

            var serializeMethod = GetMethodToWrite("serializeAs_" + GetClassName());
            var deserializeMethod = GetMethodToWrite("deserializeAs_" + GetClassName());

            serializeMethod.Rename("Serialize");
            serializeMethod.RenameVariable("NaN", "double.NaN");
            serializeMethod.RenameVariable("super", "base");
            serializeMethod.RenameVariable("output", "writer");
            serializeMethod.RenameVariable("length", "Length");
            serializeMethod.RenameType("ICustomDataOutput", "IDataWriter");
            serializeMethod.RenameMethodCall("writeUTF", "WriteUTF");
            serializeMethod.RenameMethodCall("writeVarInt", "WriteVarInt");
            serializeMethod.RenameMethodCall("writeByte", "WriteByte");
            serializeMethod.RenameMethodCall("writeVarLong", "WriteVarLong");
            serializeMethod.RenameMethodCall("writeShort", "WriteShort");
            serializeMethod.RenameMethodCall("writeVarShort", "WriteVarShort");
            serializeMethod.RenameMethodCall("writeBoolean", "WriteBoolean");
            serializeMethod.RenameMethodCall("writeDouble", "WriteDouble");
            serializeMethod.RenameMethodCall("writeInt", "WriteInt");
            serializeMethod.RenameMethodCall("writeFloat", "WriteFloat");
            serializeMethod.RenameMethodCall("writeUnsignedInt", "WriteUInt");
            serializeMethod.RenameMethodCall("setFlag", "SetFlag");
            serializeMethod.RenameMethodCall("serialize", "Serialize");

            serializeMethod.ReplaceUnchangedExpression("getTypeId()", "TypeId");

            serializeMethod.SetModifiers(AS3ModifiersEnum.@override);

            DofusHelper.IOWriteCastRecursively(serializeMethod.Expressions);
            DofusHelper.DeductFieldTypes(File, serializeMethod.Expressions); // order is importants!
            DofusHelper.RenameDofusTypesSerializeMethodsRecursively(serializeMethod.Expressions);
            DofusHelper.RenameSerializeAs_(serializeMethod.Expressions);
            DofusHelper.ChangeTypeIdToProperty(serializeMethod.Expressions);

            deserializeMethod.Rename("Deserialize");

            deserializeMethod.RenameMethodCall("readByte", "ReadByte");

            deserializeMethod.RenameVariable("NaN", "double.NaN");
            deserializeMethod.RenameVariable("super", "base");
            deserializeMethod.RenameVariable("input", "reader");
            deserializeMethod.RenameVariable("length", "Length");
            deserializeMethod.RenameType("ICustomDataInput", "IDataReader");
            deserializeMethod.RenameMethodCall("getInstance", "GetInstance");
            deserializeMethod.RenameMethodCall("readUTF", "ReadUTF");
            deserializeMethod.RenameMethodCall("readVarInt", "ReadVarInt");
            deserializeMethod.RenameMethodCall("readVarLong", "ReadVarLong");
            deserializeMethod.RenameMethodCall("readShort", "ReadShort");
            deserializeMethod.RenameMethodCall("readVarShort", "ReadVarShort");
            deserializeMethod.RenameMethodCall("readBoolean", "ReadBoolean");
            deserializeMethod.RenameMethodCall("readDouble", "ReadDouble");
            deserializeMethod.RenameMethodCall("readInt", "ReadInt");
            deserializeMethod.RenameMethodCall("readUnsignedInt", "ReadUInt");
            deserializeMethod.RenameMethodCall("readUnsignedShort", "ReadUShort");
            deserializeMethod.RenameMethodCall("readVarUhShort", "ReadVarUhShort");
            deserializeMethod.RenameMethodCall("readVarUhInt", "ReadVarUhInt");
            deserializeMethod.RenameMethodCall("readVarUhLong", "ReadVarUhLong");
            deserializeMethod.RenameMethodCall("readUnsignedByte", "ReadSByte");
            deserializeMethod.RenameMethodCall("readFloat", "ReadFloat");
            deserializeMethod.RenameMethodCall("getFlag", "GetFlag");
            deserializeMethod.RenameMethodCall("deserialize", "Deserialize");

            deserializeMethod.SetModifiers(AS3ModifiersEnum.@override);
            DofusHelper.IOReadCastRecursively(this, File, deserializeMethod, deserializeMethod.Expressions);
            DofusHelper.InstantiateArrays(this, File, deserializeMethod);
            DofusHelper.TransformVectorPushIntoCSharpArrayIndexer(File, this, deserializeMethod);
            DofusHelper.CreateGenericTypeForProtocolInstance(deserializeMethod.Expressions);
            DofusHelper.CastEnumsComparaisons(deserializeMethod.Expressions);
        }
        public override void Prepare(Dictionary<string, AS3File> context)
        {
            AS3Method.DecapsulateMethod(File, "deserializeAs_" + GetClassName());
            BuildConstructor(context);
        }
        protected override List<AS3Method> SelectMethodsToWrite()
        {
            var results =
                GetCtors().
                Concat(new AS3Method[]
                {
                   File.GetMethod("serializeAs_" + GetClassName()),
                   File.GetMethod("deserializeAs_" + GetClassName())
                })
                ;

            return results.ToList();
        }

        protected override List<AS3Field> SelectFieldsToWrite()
        {
            return base.SelectFieldsToWrite();
        }
        public SerializableConverter(AS3File file) : base(file)
        {

        }
    }
}
