using System;
using System.Collections.Generic;
namespace Example_1 {
    internal class Program {
        private static void Main(string[] args) {
            var application = new Application();
            application.Register("data 1");
            application.Register("data 2");
            application.Register("data 3");
            Console.WriteLine(application + "\n\n\nPress enter");
            Console.ReadLine();
        }
    }

    public class Application {
        private readonly DataStore _dataStore;
        public Application() {
            _dataStore = new DataStore();
            Logger.Log("Application started");
        }
        public void Register(string data) {
            Logger.Log("Registering data");
            _dataStore.Save(data);
        }

        #region Overrides of Object
        public override string ToString() {
            return _dataStore + "\n" + Logger.GetLog();
        }
        #endregion
    }

    public class DataStore {
        private readonly List<string> _data = new List<string>();
        public void Save(string data) {
            Logger.Log("Saving data");
            _data.Add(data);
        }
        public override string ToString() {
            return "\nDATASTORE:\n" + string.Join("\n", _data);
        }
    }

    public class Logger {
        private static readonly List<string> _log = new List<string>();
        public static void Log(string message) {
            _log.Add(message);
        }
        public static string GetLog() {
            return "\nLOGGER:\n" + string.Join("\n", _log);
        }
    }
}