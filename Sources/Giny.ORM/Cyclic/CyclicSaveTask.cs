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
    [WIP("threadsafe? using List?")]
    public class CyclicSaveTask : Singleton<CyclicSaveTask>
    {
        private ConcurrentDictionary<Type, List<ITable>> _newElements = new ConcurrentDictionary<Type, List<ITable>>();
        private ConcurrentDictionary<Type, List<ITable>> _updateElements = new ConcurrentDictionary<Type, List<ITable>>();
        private ConcurrentDictionary<Type, List<ITable>> _removeElements = new ConcurrentDictionary<Type, List<ITable>>();

        public void AddElement(ITable element)
        {
            var type = element.GetType();

            if (_newElements.ContainsKey(type))
            {
                if (!_newElements[type].Contains(element))
                    _newElements[type].Add(element);
            }
            else
            {
                _newElements.TryAdd(type, new List<ITable> { element });
            }
        }

        public void UpdateElement(ITable element)
        {
            var type = element.GetType();

            if (_newElements.ContainsKey(type) && _newElements[type].Contains(element))
                return;

            if (_updateElements.ContainsKey(type))
            {
                if (!_updateElements[type].Contains(element))
                    _updateElements[type].Add(element);
            }
            else
            {
                _updateElements.TryAdd(type, new List<ITable> { element });
            }
        }

        public void RemoveElement(ITable element)
        {
            if (element == null)
                return;

            var type = element.GetType();

            if (_newElements.ContainsKey(type) && _newElements[type].Contains(element))
            {
                _newElements[type].Remove(element);
                return;
            }

            if (_updateElements.ContainsKey(type) && _updateElements[type].Contains(element))
                _updateElements[type].Remove(element);

            if (_removeElements.ContainsKey(type))
            {
                if (!_removeElements[type].Contains(element))
                    _removeElements[type].Add(element);
            }
            else
            {
                _removeElements.TryAdd(type, new List<ITable> { element });
            }
        }


        public void Save()
        {
            var types = _removeElements.Keys.ToList();
            foreach (var type in types)
            {
                List<ITable> elements;
                elements = _removeElements[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), DatabaseAction.Remove);
                        _removeElements[type] = new List<ITable>(_removeElements[type].Skip(elements.Count));
                    }
                    catch (Exception e)
                    {
                        Logger.Write(e.Message, Channels.Critical);
                    }
                }


            }

            types = _newElements.Keys.ToList();
            foreach (var type in types)
            {
                List<ITable> elements;

                elements = _newElements[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), DatabaseAction.Add);
                        _newElements[type] = new List<ITable>(_newElements[type].Skip(elements.Count));
                    }
                    catch (Exception e)
                    {
                        Logger.Write(e.Message, Channels.Critical);
                    }
                }



            }

            types = _updateElements.Keys.ToList();
            foreach (var type in types)
            {
                List<ITable> elements;

                elements = _updateElements[type];

                if (elements.Count > 0)
                {
                    try
                    {
                        TableManager.Instance.GetWriter(type).Use(elements.ToArray(), DatabaseAction.Update);
                        _updateElements[type] = new List<ITable>(_updateElements[type].Skip(elements.Count));
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
