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
        
        public static void AddLater<T>(this T table) where T : IRecord
        {
            CyclicSaveTask.Instance.AddElement(table);
            TableManager.Instance.AddToContainer(table);
        }
        public static void RemoveLater<T>(this T table) where T : IRecord
        {
            CyclicSaveTask.Instance.RemoveElement(table);
            TableManager.Instance.RemoveFromContainer(table);
        }
        public static void UpdateLater<T>(this T table) where T : IRecord
        {
            CyclicSaveTask.Instance.UpdateElement(table);
        }
        public static void AddNow<T>(this T table) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new IRecord[] { table }, DatabaseAction.Add);
            TableManager.Instance.AddToContainer(table);
        }
        public static void AddNow(this IEnumerable<IRecord> tables, Type type)
        {
            TableManager.Instance.GetWriter(type).Use(tables.ToArray(), DatabaseAction.Add);

            foreach (var table in tables)
            {
                TableManager.Instance.AddToContainer(table);
            }
        }
        public static void UpdateNow<T>(this T table) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new IRecord[] { table }, DatabaseAction.Update);

        }
        public static void UpdateNow(this IEnumerable<IRecord> records, Type type)
        {
            TableManager.Instance.GetWriter(type).Use(records.ToArray(), DatabaseAction.Update);
        }

        public static void RemoveNow<T>(this T table) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new IRecord[] { table }, DatabaseAction.Remove);
            TableManager.Instance.RemoveFromContainer(table);

        }
        public static void RemoveNow<T>(this IEnumerable<T> tables) where T : IRecord
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(tables.Cast<IRecord>().ToArray(), DatabaseAction.Remove);

            foreach (var table in tables)
            {
                TableManager.Instance.RemoveFromContainer(table);
            }
        }


    }
}
