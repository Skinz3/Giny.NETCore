using Giny.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using Giny.ORM.Cyclic;
using Giny.Core.DesignPattern;

namespace Giny.ORM
{
    public static class ORMExtensions
    {
        public static void AddElement<T>(this T table) where T : IRecord
        {
            CyclicSaveTask.Instance.AddElement(table);
            TableManager.Instance.AddToContainer(table);
        }
        public static void RemoveElement<T>(this T table) where T : IRecord
        {
            CyclicSaveTask.Instance.RemoveElement(table);
            TableManager.Instance.RemoveFromContainer(table);
        }
        public static void UpdateElement<T>(this T table) where T : IRecord
        {
            CyclicSaveTask.Instance.UpdateElement(table);
        }
        public static void AddInstantElement<T>(this T table) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new IRecord[] { table }, DatabaseAction.Add);
            TableManager.Instance.AddToContainer(table);
        }
        public static void AddInstantElements(this IEnumerable<IRecord> tables, Type type)
        {
            TableManager.Instance.GetWriter(type).Use(tables.ToArray(), DatabaseAction.Add);

            foreach (var table in tables)
            {
                TableManager.Instance.AddToContainer(table);
            }
        }
        public static void UpdateInstantElement<T>(this T table) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new IRecord[] { table }, DatabaseAction.Update);

        }
        public static void UpdateInstantElements(this IEnumerable<IRecord> records, Type type)
        {
            TableManager.Instance.GetWriter(type).Use(records.ToArray(), DatabaseAction.Update);
        }

        public static void RemoveInstantElement<T>(this T table) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new IRecord[] { table }, DatabaseAction.Remove);
            TableManager.Instance.RemoveFromContainer(table);

        }
        public static void RemoveInstantElements<T>(this IEnumerable<T> tables) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(tables.Cast<IRecord>().ToArray(), DatabaseAction.Remove);

            foreach (var table in tables)
            {
                TableManager.Instance.RemoveFromContainer(table);
            }
        }


    }
}
