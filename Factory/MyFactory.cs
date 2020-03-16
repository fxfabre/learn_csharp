using System;
using System.Collections;
using System.Collections.Generic;
using NLog;

namespace Factory
{
    public class MyFactory
    {
        private static readonly Logger logger = LogManager.GetLogger("_Factory _");
        private readonly Dictionary<int, Func<IList, Type, IList>> actionFromChoice;

        public MyFactory()
        {
            actionFromChoice = new Dictionary<int, Func<IList, Type, IList>>
            {
                {1, AddItem},
                {2, RemoveItem},
                {3, ResetList},
                {4, DisplayList},
                {5, End}
            };
        }

        public static string TextMenu()
        {
            return "Select operation :  \n"
                   + "1. Add            \n"
                   + "2. Remove         \n"
                   + "3. Reset list     \n"
                   + "4. Display list   \n"
                   + "5. End            \n";
        }

        public void MainMenu()
        {
            bool stayinLoop = true;
            IList list = null;
            Type currentType = null;

            while (stayinLoop)
            {
                logger.Info(" ");
                if (list == null)
                {
                    currentType = Tools.ReadType();
                    list = InitializeList(currentType);
                }
                else
                {
                    logger.Info("List not null : can modify le list");
                    stayinLoop = ModifyList(ref list, currentType);
                }
            }
        }

        public IList InitializeList(Type type)
        {
            IList list = null;
            if (type == null)
            {
                Console.WriteLine("unknown type");
            }
            else
            {
                Type genericType = typeof (List<>).MakeGenericType(type);
                list = (IList) Activator.CreateInstance(genericType);
                logger.Info("Created list of type " + type.Name);
            }
            Console.WriteLine();
            return list;
        }

        public bool ModifyList(ref IList list, Type type)
        {
            Console.WriteLine(TextMenu());
            int? choice = Tools.ReadInt();

            if (! choice.HasValue)
            {
                logger.Warn("Unable to cast choice to int : null choice");
                choice = -1;
            }

            logger.Info("Modify List with choice : " + choice);
            bool returnVal = true;

            if (actionFromChoice.ContainsKey(choice.Value))
            {
                actionFromChoice[choice.Value](list, type);
                if (choice.Value == 5)
                {
                    returnVal = false;
                }
            }
            else
            {
                logger.Error("Choice not valid " + choice);
                Console.WriteLine("Choice not valid " + choice);
            }

            return returnVal;
        }


        public IList AddItem(IList list, Type type)
        {
            logger.Info("Add item to the list");
            object item = Tools.ReadParameter(type);
            if (item != null)
            {
                logger.Info("Item added");
                list.Add(item);
            }
            return list;
        }

        public IList RemoveItem(IList list, Type type)
        {
            logger.Info("Removing an item from the list");
            object value = Tools.ReadParameter(type);
            list.Remove(value);
            return list;
        }

        public IList ResetList(IList list, Type type)
        {
            logger.Info("Reset the list");
            list.Clear();
            return null;
        }

        public IList DisplayList(IList list, Type type)
        {
            logger.Info("Display list");
            Console.WriteLine("** Display list **");

            IEnumerator enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                Console.WriteLine(current.ToString());
            }
            Console.WriteLine("******************\n");
            return list;
        }

        public IList End(IList list, Type type)
        {
            logger.Info("End !");
            return ResetList(list, type);
        }
    }
}