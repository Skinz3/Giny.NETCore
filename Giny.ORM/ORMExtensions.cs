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
        public static void AddElement<T>(this T table) where T : ITable
        {
            CyclicSaveTask.Instance.AddElement(table);
            TableManager.Instance.AddToContainer(table);
        }
        public static void RemoveElement<T>(this T table) where T : ITable
        {
            CyclicSaveTask.Instance.RemoveElement(table);
            TableManager.Instance.RemoveFromContainer(table);
        }
        public static void UpdateElement<T>(this T table) where T : ITable
        {
            CyclicSaveTask.Instance.UpdateElement(table);
        }
        public static void AddInstantElement<T>(this T table) where T : ITable
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new ITable[] { table }, DatabaseAction.Add);
            TableManager.Instance.AddToContainer(table);
        }
        public static void AddInstantElements(this IEnumerable<ITable> tables, Type type)
        {
            TableManager.Instance.GetWriter(type).Use(tables.ToArray(), DatabaseAction.Add);

            foreach (var table in tables)
            {
                TableManager.Instance.AddToContainer(table);
            }
        }
        public static void UpdateInstantElement<T>(this T table) where T : ITable
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new ITable[] { table }, DatabaseAction.Update);

        }
        public static void UpdateInstantElements(this IEnumerable<ITable> records, Type type)
        {
            TableManager.Instance.GetWriter(type).Use(records.ToArray(), DatabaseAction.Update);
        }

        public static void RemoveInstantElement<T>(this T table) where T : ITable
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(new ITable[] { table }, DatabaseAction.Remove);
            TableManager.Instance.RemoveFromContainer(table);

        }
        public static void RemoveInstantElements<T>(this IEnumerable<T> tables) where T : ITable
        {
            TableManager.Instance.GetWriter(typeof(T)).Use(tables.Cast<ITable>().ToArray(), DatabaseAction.Remove);

            foreach (var table in tables)
            {
                TableManager.Instance.RemoveFromContainer(table);
            }
        }


    }
}
