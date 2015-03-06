using System;
using System.Collections.Generic;
namespace Example_3 {
    internal class Program {
        private static void Main(string[] args) {
            //ServiceLocator is initialized.
            ServiceLocator.Current = new ServiceLocator();
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
            //ServiceLocator used instead of calling constructor.
            _logger = ServiceLocator.Current.Logger;
            //ServiceLocator used instead of calling constructor.
            _dataStore = ServiceLocator.Current.DataStore;
            _logger.Log("Application started");
        }
        public void Register(string data) {
            _logger.Log("Registering data");
            //logger parameter removed from Save method
            _dataStore.Save(data);
        }

        public override string ToString() {
            return _dataStore + "\n" + _logger.GetLog();
        }
    }

    //Poor mans Service locator introduced.
    public class ServiceLocator {
        public ServiceLocator() {
            Logger = new Logger();
            DataStore = new DataStore();
        }
        public static ServiceLocator Current;
        public ILogger Logger { get; private set; }
        public IDataStore DataStore { get; private set; }
    }

    public interface IDataStore {
        //logger parameter removed from Save method
        void Save(string data);
    }

    public class DataStore : IDataStore {
        private readonly List<string> _data = new List<string>();
        //Logger parameter removed from Save method
        public void Save(string data) {
            ServiceLocator.Current.Logger.Log("Saving data");
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