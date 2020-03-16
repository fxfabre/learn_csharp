 //[assembly: AssemblyTitle("TestClass")]
//[assembly: AssemblyDescription("Test for NS ReflexionLibs")]
//[assembly: AssemblyCompany("MargOconseil")]

namespace ReflexionLibs.Tests
{
    internal class TestClass
    {
        /*******************
         * Fields
         ********************/
        public int PublicField;

        protected int ProtectedField;

        #pragma warning disable 0414    // field never used
        private int privateField;
        #pragma warning restore 0414


        /*******************
         * Static Fields
         ********************/
        #pragma warning disable 0649    // field never used
        public static int PublicStaticField;
        #pragma warning restore 0649

        #pragma warning disable 0649    // field never used
        protected static int ProtectedStaticField;
        #pragma warning restore 0649

        #pragma warning disable 0169    // field never used
        private static int privateStaticField;
        #pragma warning restore 0169


        /************************
         * Properties
         ***********************/
        public int PublicProp { get; set; }

        protected int ProtectedProp { get; set; }

        private int PrivateProp { get; set; }


        /************************
         * Static properties
         ***********************/
        public static int PublicStaticProp { get; set; }

        protected static int ProtectedStaticProp { get; set; }

        private static int PrivateStaticProp { get; set; }

        /************************
         * Constructor
         ***********************/
        public TestClass()
        {
            PublicField = 0;
            ProtectedField = 0;
            privateField = 0;

            PublicProp = 0;
            ProtectedProp = 0;
            PrivateProp = 0;
        }

        public TestClass(int a)
        {
            
        }

        public TestClass(int a, int b)
        {
            
        }

        /************************
         * Methods d'instance
         ***********************/

        public void PublicMethod()
        {
        }

        public int PublicMethod(int n)
        {
            return n;
        }

        private void PrivateMethod()
        {
        }

        private int PrivateMethod(int n)
        {
            return n + 2;
        }


        /************************
         * Methods statiques
         ***********************/

        public static void PublicStaticMethod()
        {
        }

        private static void PrivateStaticMethod()
        {
        }
    }
}