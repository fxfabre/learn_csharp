using System;
using System.Reflection;
using NLog;

namespace Factory
{
    public static class Tools
    {
        private static readonly Logger logger = LogManager.GetLogger("_Factory tools_");

        public static Type ReadType()
        {
            Console.Write("Enter the type of the list : ");
            logger.Info("Reading a type");
            string typeName = Console.ReadLine();
            Type type;

            try
            {
                type = Type.GetType(typeName.Trim());
                logger.Info("Type read : " + type.Name);
            }
            catch (Exception ex)
            {
                type = null;
                logger.Error("Can't read type " + typeName, ex);
            }

            return type;
        }

        public static int? ReadInt()
        {
            Console.WriteLine("Please enter a number");
            logger.Info("Trying to read int value");
            string s = Console.ReadLine();
            int result;

            if (!int.TryParse(s, out result))
            {
                logger.Warn("Cannot parse");
                return null;
            }
            return result;
        }

        public static object CreateInstance(Type type)
        {
            logger.Info("Trying to instanciate type " + type.Name);
            object item;
            try
            {
                item = Activator.CreateInstance(type);
            }
            catch (Exception)
            {
                Console.WriteLine("Can't create instance of type " + type.Name);
                item = null;
            }
            return item;
        }

        public static object ReadParameter(Type type)
        {
            Console.WriteLine("Please enter a parameter of type " + type.Name);
            string read = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(read))
            {
                logger.Warn("Empty string, can't create object");
                return null;
            }
            if (type.Name == "String")
            {
                return read.Trim();
            }

            object obj = CreateInstance(type);
            MethodInfo mi = type.GetMethod("Parse", new[] {typeof (string)});
            if (mi == null)
            {
                logger.Error("Can't find method Parse");
                Console.WriteLine("Can't find method");
                obj = null;
            }
            else
            {
                obj = ParseObject(mi, read);
            }
            return obj;
        }

        public static object ParseObject(MethodInfo mi, string read)
        {
            object obj;
            try
            {
                obj = mi.Invoke(null, new[] {read});
            }
            catch (Exception e)
            {
                logger.Info("Parsing failed. Exception : " + e.Message);
                Console.WriteLine("Exception : " + e.Message);
                obj = null;
            }

            logger.Info("Parsing success, return value = " + obj);
            return obj;
        }
    }
}