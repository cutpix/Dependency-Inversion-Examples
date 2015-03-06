using System;
using System.Collections.Generic;
using SimpleInjector;
namespace Example_5 {
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
            _logger = ServiceLocator.Current.Resolve<Logger>();
            _dataStore = ServiceLocator.Current.Resolve<DataStore>();
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
        private readonly Container _container = new Container();
        public ServiceLocator() {
            _container.RegisterSingle<ILogger,Logger>();
            _container.RegisterSingle<IDataStore, DataStore>();
        }
        public T Resolve<T>() {
            return (T) _container.GetInstance(typeof (T));
        }
        public static ServiceLocator Current;
    }

    public interface IDataStore {
        //logger parameter removed from Save method
        void Save(string data);
    }

    public class DataStore : IDataStore {
        private readonly List<string> _data = new List<string>();
        //Logger parameter removed from Save method
        public void Save(string data) {
            ServiceLocator.Current.Resolve<Logger>().Log("Saving data");
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