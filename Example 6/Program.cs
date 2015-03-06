using System;
using System.Collections.Generic;
using SimpleInjector;
namespace Example_6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Dependendency injection with an IOC container
            var container = new Container();
            container.RegisterSingle<ILogger, Logger>();
            container.RegisterSingle<IDataStore, DataStore>();
            container.RegisterSingle<Application>();
            //Resolving Application from the container takes care
            // of auto wiring the entire object graph of dependencies.
            var application = container.GetInstance<Application>(); 
            application.Register("data 1");
            application.Register("data 2");
            application.Register("data 3");
            Console.WriteLine(application + "\n\n\nPress enter");
            Console.ReadLine();
        }
    }

    public class Application
    {
        private readonly ILogger _logger;
        private readonly IDataStore _dataStore;
        // dataStore and logger are now injected in the constructor.
        public Application(IDataStore dataStore, ILogger logger)
        {
            _logger = logger;
            _dataStore = dataStore;
            _logger.Log("Application started");
        }
        public void Register(string data)
        {
            _logger.Log("Registering data");
            //logger parameter removed from Save method
            _dataStore.Save(data);
        }

        public override string ToString()
        {
            return _dataStore + "\n" + _logger.GetLog();
        }
    }

    public interface IDataStore
    {
        //logger parameter removed from Save method
        void Save(string data);
    }

    public class DataStore : IDataStore
    {
        // Logger is now injected.
        public DataStore(ILogger logger)
        {
            _logger = logger;
        }
        private readonly List<string> _data = new List<string>();
        private readonly ILogger _logger;
        //Logger parameter removed from Save method
        public void Save(string data)
        {
            _logger.Log("Saving data");
            _data.Add(data);
        }
        public override string ToString()
        {
            return "\nDATASTORE:\n" + string.Join("\n", _data);
        }
    }

    public interface ILogger
    {
        void Log(string message);
        string GetLog();
    }

    public class Logger : ILogger
    {
        private readonly List<string> _log = new List<string>();
        public void Log(string message)
        {
            _log.Add(message);
        }
        public string GetLog()
        {
            return "\nLOGGER:\n" + string.Join("\n", _log);
        }
    }
}