using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Pushover
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = new SimpleContainer();
            var logger = new TextLogger();
            container.Register<ILogger>(() => logger);
            container.Register<INotificationService>(() => new EmailService(logger));
            container.Register<INotificationService>(() => new SmsService(logger));
            container.Register<INotificationService>(() => new PushNotificationService(logger));
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(container.Resolve<IEnumerable<INotificationService>>(), logger));
        }
    }

    internal class SimpleContainer
    {
        private readonly Dictionary<Type, List<Func<object>>> _registrations = new();

        public void Register<T>(Func<object> factory) => Register(typeof(T), factory);

        public void Register(Type serviceType, Func<object> factory)
        {
            if (!_registrations.TryGetValue(serviceType, out var list))
            {
                list = new List<Func<object>>();
                _registrations[serviceType] = list;
            }
            list.Add(factory);
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));

        public object Resolve(Type serviceType)
        {
            if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var itemType = serviceType.GetGenericArguments()[0];
                return ResolveAll(itemType);
            }
            if (_registrations.TryGetValue(serviceType, out var list) && list.Count == 1)
            {
                return list[0]();
            }
            throw new InvalidOperationException("Service not registered");
        }

        private object ResolveAll(Type itemType)
        {
            var listType = typeof(List<>).MakeGenericType(itemType);
            var result = (System.Collections.IList)Activator.CreateInstance(listType)!;
            foreach (var kv in _registrations)
            {
                if (itemType.IsAssignableFrom(kv.Key))
                {
                    foreach (var factory in kv.Value)
                    {
                        result.Add(factory());
                    }
                }
            }
            return result;
        }
    }

    internal interface ILogger
    {
        event Action<string>? MessageLogged;
        void Log(string text);
    }

    internal class TextLogger : ILogger
    {
        public event Action<string>? MessageLogged;
        public void Log(string text) => MessageLogged?.Invoke(text);
    }

    internal interface INotificationService
    {
        string Name { get; }
        void Send(string message);
    }

    internal class EmailService : INotificationService
    {
        private static readonly Random Random = new();
        private readonly ILogger _logger;
        public EmailService(ILogger logger) => _logger = logger;
        public string Name => "Email";
        public void Send(string message)
        {
            _logger.Log("Email готовит сообщение");
            TryMaybeThrow();
            _logger.Log("Email отправлено");
        }
        private static void TryMaybeThrow()
        {
            if (Random.Next(5) == 0)
            {
                throw new InvalidOperationException("Ошибка SMTP");
            }
        }
    }

    internal class SmsService : INotificationService
    {
        private static readonly Random Random = new();
        private readonly ILogger _logger;
        public SmsService(ILogger logger) => _logger = logger;
        public string Name => "SMS";
        public void Send(string message)
        {
            _logger.Log("SMS готовит сообщение");
            TryMaybeThrow();
            _logger.Log("SMS отправлено");
        }
        private static void TryMaybeThrow()
        {
            if (Random.Next(6) == 0)
            {
                throw new InvalidOperationException("Ошибка SMS-шлюза");
            }
        }
    }

    internal class PushNotificationService : INotificationService
    {
        private static readonly Random Random = new();
        private readonly ILogger _logger;
        public PushNotificationService(ILogger logger) => _logger = logger;
        public string Name => "Push";
        public void Send(string message)
        {
            _logger.Log("Push готовит сообщение");
            TryMaybeThrow();
            _logger.Log("Push уведомление отправлено");
        }
        private static void TryMaybeThrow()
        {
            if (Random.Next(4) == 0)
            {
                throw new InvalidOperationException("Ошибка Push-сервиса");
            }
        }
    }
}
