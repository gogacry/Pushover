using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace Pushover
{
    internal partial class MainForm : Form
    {
        private readonly IEnumerable<INotificationService> _services;
        private readonly ILogger _logger;
        private static readonly Random _random = new();
        private static readonly string[] BannedWords = { "телеграмм", "ватсап", "инстаграмм", "тик ток", "ютуб", "фейсбук", "твиттер" };

        public MainForm(IEnumerable<INotificationService> services, ILogger logger)
        {
            InitializeComponent();
            _services = services;
            _logger = logger;
            _logger.MessageLogged += OnLog;
            comboBoxService.Items.AddRange(_services.Select(s => s.Name).ToArray());
            if (comboBoxService.Items.Count > 0)
            {
                comboBoxService.SelectedIndex = 0;
            }
        }

        private void OnLog(string text)
        {
            var message = $"{DateTime.Now:HH:mm:ss} {text}";
            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.Invoke(() => richTextBoxLog.AppendText(message + Environment.NewLine));
            }
            else
            {
                richTextBoxLog.AppendText(message + Environment.NewLine);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var message = textBoxMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                _logger.Log("Ошибка: пустое сообщение");
                MessageBox.Show("Нельзя отправить пустое сообщение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (BannedWords.Any(word => message.Equals(word, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.Log("Пасхалка найдена");
                MessageBox.Show("⚠️ Обнаружено запрещенное слово!\nШтраф: 5000 рублей", "🚨 Внимание 🚨", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var selectedServiceName = comboBoxService.SelectedItem as string;
            var service = _services.FirstOrDefault(s => s.Name == selectedServiceName);
            if (service == null)
            {
                _logger.Log("Ошибка: сервис не выбран");
                MessageBox.Show("Выберите сервис для отправки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _logger.Log($"Попытка отправки через {service.Name}: {message}");
            try
            {
                var notificationSender = new NotificationSender(service);
                notificationSender.Send(message);
                _logger.Log($"Успех: {service.Name} отправлено");
            }
            catch (Exception ex)
            {
                _logger.Log($"Ошибка при отправке: {ex.Message}");
                MessageBox.Show($"Ошибка при отправке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    internal class NotificationSender
    {
        private readonly INotificationService _service;
        public NotificationSender(INotificationService service) => _service = service;
        public void Send(string message) => _service.Send(message);
    }
}
