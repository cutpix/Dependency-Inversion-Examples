using System;
using System.Collections.Generic;
namespace Example_2 {
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
        private readonly ILogger _logger;
        private readonly IDataStore _dataStore;
        public Application() {
            _logger = new Logger();
            _dataStore = new DataStore();
            _logger.Log("Application started");
        }
        public void Register(string data) {
            _logger.Log("Registering data");
            _dataStore.Save(data, _logger);
        }

        #region Overrides of Object
        public override string ToString() {
            return _dataStore + "\n" + _logger.GetLog();
        }
        #endregion
    }

    public interface IDataStore {
        void Save(string data, ILogger logger);
    }

    public class DataStore : IDataStore {
        private readonly List<string> _data = new List<string>();
        public void Save(string data, ILogger logger) {
            logger.Log("Saving data");
            _data.Add(data);
        }
        public override string ToString() {
            return "\nDATASTORE:\n" + string.Join("\n", _data);
        }
    }

    public interface ILogger {
        void Log(string message);
        string GetLog();
    }

    public class Logger : ILogger {
        private readonly List<string> _log = new List<string>();
        public void Log(string message) {
            _log.Add(message);
        }
        public string GetLog() {
            return "\nLOGGER:\n" + string.Join("\n", _log);
        }
    }
}