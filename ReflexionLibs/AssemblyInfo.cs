using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;

namespace ReflexionLibs
{
    public static class AssemblyInfo
    {
        private static readonly Logger logger = LogManager.GetLogger("_Reflexion Lib__");

        /*********************
         * Get Types
         *********************/

        public static List<Type> GetAssemblyFromFilePath(string filePath)
        {
            logger.Info("Loading DLL " + filePath);
            return GetAssemblyTypes(s => Assembly.LoadFrom(s), filePath);
        }

        public static List<Type> GetAssemblyFromFileName(string fileName)
        {
            logger.Info("Loading DLL " + fileName);
            return GetAssemblyTypes(s => Assembly.Load(s), fileName);
        }

        public static List<Type> GetCurrentassembly()
        {
            logger.Info("Loading current assembly");
            return GetAssemblyTypes(_ => Assembly.GetExecutingAssembly(), string.Empty);
        }

        private static List<Type> GetAssemblyTypes(Func<string, Assembly> predicate, string assemblyName)
        {
            var returnTypes = new List<Type>();

            try
            {
                Assembly objAssembly = predicate(assemblyName);
                logger.Info("Assembly loaded");

                foreach (Type objType in objAssembly.GetTypes())
                {
                    returnTypes.Add(objType);
                    logger.Info("Type " + objType.Name + " found in assembly " + assemblyName);
                }
            }
            catch (Exception e)
            {
                logger.Error("Can't load assembly " + assemblyName);
                logger.Error("Error message : " + e.Message);
            }

            return returnTypes;
        }


        /********************
         * Get methods
         ********************/

        public static List<MemberInfo> GetAllItems(Type t)
        {
            logger.Info("Get all items");
            var list = new List<MemberInfo>();

            list.AddRange(GetMethod(t));
            list.AddRange(GetConstructors(t));
            list.AddRange(GetProperties(t));
            list.AddRange(GetFields(t));

            return list;
        }

        public static List<MethodInfo> GetMethod(Type t)
        {
            logger.Info("Get Methods");
            var returnList = new List<MethodInfo>();

            returnList = AddMethodsToList(returnList, t, BindingFlags.Public | BindingFlags.Instance);
            returnList = AddMethodsToList(returnList, t, BindingFlags.NonPublic | BindingFlags.Instance);
            returnList = AddMethodsToList(returnList, t, BindingFlags.Public | BindingFlags.Static);
            returnList = AddMethodsToList(returnList, t, BindingFlags.NonPublic | BindingFlags.Static);

            return returnList;
        }

        private static List<MethodInfo> AddMethodsToList(List<MethodInfo> returnList, Type t, BindingFlags flags)
        {
            MethodInfo[] list = t.GetMethods(flags);
            foreach (MethodInfo method in list)
            {
                if (method.IsSpecialName)
                {
                    // Pas de liste pour toi
                    logger.Info(method.Name + " is a special class");
                }
                else
                {
                    if (!returnList.Contains(method))
                    {
                        logger.Info("Adding to list : " + method.Name);
                        returnList.Add(method);
                    }
                }
            }
            return returnList.OrderBy(info => info.ToString()).ToList();
        }

        public static List<ConstructorInfo> GetConstructors(Type t)
        {
            logger.Info("Get constructors");
            return t.GetConstructors().ToList();
        }

        public static List<PropertyInfo> GetProperties(Type t)
        {
            logger.Info("Get properties");
            return t.GetRuntimeProperties().ToList();
        }

        public static List<FieldInfo> GetFields(Type t)
        {
            logger.Info("Get fields");
            var returnList = new List<FieldInfo>();

            returnList = AddFieldsToList(returnList, t, BindingFlags.Public | BindingFlags.Instance);
            returnList = AddFieldsToList(returnList, t, BindingFlags.NonPublic | BindingFlags.Instance);
            returnList = AddFieldsToList(returnList, t, BindingFlags.Public | BindingFlags.Static);
            returnList = AddFieldsToList(returnList, t, BindingFlags.NonPublic | BindingFlags.Static);

            return returnList;
        }

        private static List<FieldInfo> AddFieldsToList(List<FieldInfo> returnList, Type t, BindingFlags flags)
        {
            FieldInfo[] list = t.GetFields(flags);
            foreach (FieldInfo field in list)
            {
                if (field.Name[0] == '<')
                {
                    logger.Info(field.Name + " is a special field");
                    // Property
                }
                else
                {
                    if (!returnList.Contains(field))
                    {
                        logger.Info("Adding field to list : " + field.Name);
                        returnList.Add(field);
                    }
                }
            }
            return returnList.OrderBy(info => info.ToString()).ToList();
        }
    }
}