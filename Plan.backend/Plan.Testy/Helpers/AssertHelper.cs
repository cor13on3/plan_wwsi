using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Plan.Testy.Helpers
{
    public static class AssertHelper
    {
        public static void OczekiwanyWyjatek<T>(Action akcja, string oczekiwanyKomunikat)
            where T : Exception
        {
            var e = PobierzOczekiwanyWyjatek<T>(akcja);
            var komunikat = e.Message;
            Assert.AreEqual(oczekiwanyKomunikat, komunikat);
        }

        public static T PobierzOczekiwanyWyjatek<T>(Action akcja)
            where T : Exception
        {
            try
            {
                akcja();
            }
            catch (T e)
            {
                return e;
            }
            catch (Exception e)
            {
                Assert.Fail($"Oczekiwano wyjątku {typeof(T).Name} a wystąpił {e.GetType().Name}: {e.Message}");
            }

            Assert.Fail($"Nie wystąpił wyjątek {typeof(T).Name}");
            return null;
        }

    }
}
