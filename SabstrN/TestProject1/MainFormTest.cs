using SabstrN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    
    
    /// <summary>
    ///Это класс теста для MainFormTest, в котором должны
    ///находиться все модульные тесты MainFormTest
    ///</summary>
    [TestClass()]
    public class MainFormTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Тест для CutStrBeforN
        ///</summary>
        [TestMethod()]
        public void CutStrBeforNTest()
        {
            MainForm target = new MainForm(); // TODO: инициализация подходящего значения
            string inputStr = "Хе хе"; // TODO: инициализация подходящего значения
            int maxChars = 0; // TODO: инициализация подходящего значения
            target.CutStrBeforN(inputStr, maxChars);
            //Assert.Inconclusive("Невозможно проверить метод, не возвращающий значение.");
        }
    }
}
