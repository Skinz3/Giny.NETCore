using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Giny.ORM.Cyclic
{
    public class CyclicSaveTask : Singleton<CyclicSaveTask>
    {
        private object InsertLock = new object();
        private object DeleteLock = new object();
        private object UpdateLock = new object();

        private Dictionary<Type, List<IRecord>> ElementsToInsert = new Dictionary<Type, List<IRecord>>();
        private Dictionary<Type, List<IRecord>> ElementsToUpdate = new Dictionary<Type, List<IRecord>>();
        private Dictionary<Type, List<IRecord>> ElementsToRemove = new Dictionary<Type, List<IRecord>>();

        public void AddElement(IRecord element)
        {
            var type = element.GetType();

            lock (InsertLock)
            {
                if (ElementsToInsert.ContainsKey(type))
                {
                    if (!ElementsToInsert[type].Contains(element))
                    {

                        ElementsToInsert[type].Add(element);
                    }
                }
                else
                {
                    ElementsToInsert.TryAdd(type, new List<IRecord> { element });
                }
            }
        }

        public void UpdateElement(IRecord element)
        {
            var type = element.GetType();

            lock (InsertLock)
            {
                if (ElementsToInsert.ContainsKey(type) && ElementsToInsert[type].Contains(element))
                    return;
            }

            lock (UpdateLock)
            {
                if (ElementsToUpdate.ContainsKey(type))
                {
                    if (!ElementsToUpdate[type].Contains(element))
                        ElementsToUpdate[type].Add(element);
                }
                else
                {
                    ElementsToUpdate.TryAdd(type, new List<IRecord> { element });
                }
            }
        }

        public void RemoveElement(IRecord element)
        {
            if (element == null)
                return;

            var type = element.GetType();

            lock (InsertLock)
            {
                if (ElementsToInsert.ContainsKey(type) && ElementsToInsert[type].Contains(element))
                {
                    ElementsToInsert[type].Remove(element);
                    return;
                }
            }

            lock (UpdateLock)
            {
                if (ElementsToUpdate.ContainsKey(type) && ElementsToUpdate[type].Contains(element))
                    ElementsToUpdate[type].Remove(element);
            }

            lock (DeleteLock)
            {
                if (ElementsToRemove.ContainsKey(type))
                {
                    if (!ElementsToRemove[type].Contains(element))
                        ElementsToRemove[type].Add(element);
                }
                else
                {
                    ElementsToRemove.TryAdd(type, new List<IRecord> { element });
                }
            }
        }

        public void Save()
        {
            Dictionary<Type, List<IRecord>> removeElements;

            lock (DeleteLock)
            {
                removeElements = CopyElementsDictionary(ElementsToRemove);
            }

            Dictionary<Type, List<IRecord>> addElements;

            lock (InsertLock)
            {
                addElements = CopyElementsDictionary(ElementsToInsert);
            }

            Dictionary<Type, List<IRecord>> updateElements;

            lock (UpdateLock)
            {
                updateElements = CopyElementsDictionary(ElementsToUpdate);
            }


            SaveElements(removeElements, DatabaseAction.Remove);
            SaveElements(addElements, DatabaseAction.Add);
            SaveElements(updateElements, DatabaseAction.Update);

            lock (DeleteLock)
            {
                foreach (var key in removeElements.Keys)
                {
                    foreach (var element in removeElements[key])
                    {
                        ElementsToRemove[key].Remove(element);
                    }
                }
            }

            lock (InsertLock)
            {
                foreach (var key in addElements.Keys)
                {
                    foreach (var element in addElements[key])
                    {
                        ElementsToInsert[key].Remove(element);
                    }
                }
            }

            lock (UpdateLock)
            {
                foreach (var key in updateElements.Keys)
                {
                    foreach (var element in updateElements[key])
                    {
                        ElementsToUpdate[key].Remove(element);
                    }
                }
            }
        }

        private Dictionary<Type, List<IRecord>> CopyElementsDictionary(Dictionary<Type, List<IRecord>> original)
        {
            var copy = new Dictionary<Type, List<IRecord>>();

            foreach (var kvp in original)
            {
                var type = kvp.Key;
                var elements = kvp.Value.ToList();  // Create a copy of the list
                copy[type] = elements;
            }

            return copy;
        }


        private void SaveElements(Dictionary<Type, List<IRecord>> elementsDictionary, DatabaseAction action)
        {
            foreach (var type in elementsDictionary.Keys.ToList())
            {
                var elements = elementsDictionary[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), action);



                    }
                    catch (Exception e)
                    {
                        Logger.Write(e.Message, Channels.Critical);
                    }
                }
            }
        }
    }
}
