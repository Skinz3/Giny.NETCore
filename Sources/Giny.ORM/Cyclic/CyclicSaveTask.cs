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
    [Annotation("threadsafe? using List?")]
    public class CyclicSaveTask : Singleton<CyclicSaveTask>
    {
        private ConcurrentDictionary<Type, List<IRecord>> ElementsToAdd = new ConcurrentDictionary<Type, List<IRecord>>();
        private ConcurrentDictionary<Type, List<IRecord>> ElementsToUpdate = new ConcurrentDictionary<Type, List<IRecord>>();
        private ConcurrentDictionary<Type, List<IRecord>> ElementsToRemove = new ConcurrentDictionary<Type, List<IRecord>>();

        public void AddElement(IRecord element)
        {
            var type = element.GetType();

            if (ElementsToAdd.ContainsKey(type))
            {
                if (!ElementsToAdd[type].Contains(element))
                    ElementsToAdd[type].Add(element);
            }
            else
            {
                ElementsToAdd.TryAdd(type, new List<IRecord> { element });
            }
        }

        public void UpdateElement(IRecord element)
        {
            var type = element.GetType();

            if (ElementsToAdd.ContainsKey(type) && ElementsToAdd[type].Contains(element))
                return;

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

        public void RemoveElement(IRecord element)
        {
            if (element == null)
                return;

            var type = element.GetType();

            if (ElementsToAdd.ContainsKey(type) && ElementsToAdd[type].Contains(element))
            {
                ElementsToAdd[type].Remove(element);
                return;
            }

            if (ElementsToUpdate.ContainsKey(type) && ElementsToUpdate[type].Contains(element))
                ElementsToUpdate[type].Remove(element);

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


        public void Save()
        {
            var types = ElementsToRemove.Keys.ToList();
            foreach (var type in types)
            {
                List<IRecord> elements;
                elements = ElementsToRemove[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), DatabaseAction.Remove);
                        ElementsToRemove[type] = new List<IRecord>(ElementsToRemove[type].Skip(elements.Count));
                    }
                    catch (Exception e)
                    {
                        Logger.Write(e.Message, Channels.Critical);
                    }
                }


            }

            types = ElementsToAdd.Keys.ToList();
            foreach (var type in types)
            {
                List<IRecord> elements;

                elements = ElementsToAdd[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), DatabaseAction.Add);
                        ElementsToAdd[type] = new List<IRecord>(ElementsToAdd[type].Skip(elements.Count));
                    }
                    catch (Exception e)
                    {
                        Logger.Write(e.Message, Channels.Critical);
                    }
                }



            }

            types = ElementsToUpdate.Keys.ToList();

            foreach (var type in types)
            {
                List<IRecord> elements;

                elements = ElementsToUpdate[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), DatabaseAction.Update);
                        ElementsToUpdate[type] = new List<IRecord>(ElementsToUpdate[type].Skip(elements.Count));
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
