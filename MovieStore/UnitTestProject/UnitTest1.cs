using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStore;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        //defining an object with in the class Database from MovieStore
        DatabaseManager Obj_Test = new DatabaseManager();
        [TestMethod]
        public void TestDBConection()
        {
            // Variable of actual and expected connection String 
            var ActualDBCon = Obj_Test.constring;
            var ExpexctedDBCon = @"Data Source = WT135-826LSW\SQLEXPRESS;Initial Catalog = MOVIE_STORE; Integrated Security = True";
            //Assert - checking the output is which expected
            Assert.AreEqual(ExpexctedDBCon, ActualDBCon);
        }
    }
}
