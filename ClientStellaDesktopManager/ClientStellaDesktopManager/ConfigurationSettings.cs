using System;
using System.Xml;
using System.Configuration;
using System.Reflection;

/*---------------------------------------------------------------------------------------------------------
http://www.coding4.net/post/create-read-update-delete-configuration-in-app-config-file.aspx
Можно было бы предположить, что под Net Framework будет удобно записывать конфигурацию в файл.Но.Net Framework
предоставляет простые методы для чтения из файла конфигурации и не дает ничего для записи значения обратно
в App.Config.На самом деле достаточно легко записать значения обратно в файл.Ведь App.Config это всего лишь XML.
Когда необходимо работать с конфигурацией, удобно было бы использовать один класс, который бы умел создавать,
читать, писать, стирать части конфигурации в App.Config.

Предупреждение !!! Обычно App.Config используется для записи данных вручную или при начальном конфигурировании
приложения и только для чтения во время работы программы.Если программа начинает писать в файл App.Config сама,
то теперь от нее требуется гораздо больше самоконтроля. Для того чтобы она не записала некорректную информацию
и смогла стартовать в следующий раз. Также программа не должна по ошибке затереть или неверно изменить
системные части App.Config.При обычном использовании App.Config ответственность за правильное составление
конфигурационного файла лежит на конфигурирующем ПО или пользователе/администраторе.

Конфигурационный файл для приложений NET представляет собой текстовый файл, который имеет название
myapplication.exe.config. 
Visual Studio упрощает жизнь программистам и позволяет добавить файл с именем "App.config" в проект.
При компиляции он будет скопирован в соответствующий каталог Bin и переименован в myapplication.exe.config.
Этот конфигурационный файл предназначен для хранения статических значений (настройки для вашего приложения).
Конфигурационный файл это не более, чем обычный XML файл.

Net Framework делает жизнь проще при чтении значений из App.Config.
Класс ConfigurationSettings в пространстве имен System.Configuration имеет статическое
индексированное свойство AppSettings. Это свойство возвращает NameValueCollection.
Чтобы прочитать значение с ключем "Test1" из конфигурационного файла можно сделать так:

string test1 = ConfigurationSettings.AppSettings["Test1"];

Следует отметить, что это позволит вам только читать настройки и только из раздела AppSettings.
Если у вас добавлены другие разделы, то вы не сможете прочитать их таким образом.
Для записи значения нужно открыть конфигурационный файл как XmlDocument и писать.
Через XmlDocument вы можете добавлять пары имя/значение, удалять элементы, изменять и, при желании, читать.
Дальше идет исходник класса на C#, который позволяет читать, записывать и 
удалять настройки в разделе AppSettings файла App.Config. 
-------------------------------------------------------------------------------------------------------------*/

namespace ClientStellaDesktopManager
{
	public static class ConfigSettings
	{
		public static string ReadSetting(string key)
		{
			return ConfigurationSettings.AppSettings[key];
		}

		public static void WriteSetting(string key, string value)
		{
			XmlDocument doc = loadConfigDocument();
			XmlNode node = doc.SelectSingleNode("//appSettings");

			if (node == null)
				throw new InvalidOperationException("appSettings section not found in config file.");

			try
			{
				XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

				if (elem != null)
				{
					elem.SetAttribute("value", value);
				}
				else
				{
					elem = doc.CreateElement("add");
					elem.SetAttribute("key", key);
					elem.SetAttribute("value", value);
					node.AppendChild(elem);
				}
				doc.Save(getConfigFilePath());
			}
			catch
			{
				throw;
			}
		}

		public static void RemoveSetting(string key)
		{
			XmlDocument doc = loadConfigDocument();
			XmlNode node = doc.SelectSingleNode("//appSettings");

			try
			{
				if (node == null)
					throw new InvalidOperationException("appSettings section not found in config file.");
				else
				{
					node.RemoveChild(node.SelectSingleNode(string.Format("//add[@key='{0}']", key)));
					doc.Save(getConfigFilePath());
				}
			}
			catch (NullReferenceException e)
			{
				throw new Exception(string.Format("The key {0} does not exist.", key), e);
			}
		}

		private static XmlDocument loadConfigDocument()
		{
			XmlDocument doc = null;
			try
			{
				doc = new XmlDocument();
				doc.Load(getConfigFilePath());
				return doc;
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new Exception("No configuration file found.", e);
			}
		}

		private static string getConfigFilePath()
		{
			return Assembly.GetExecutingAssembly().Location + ".config";
		}
	}
}

/*-------------------------------------------------------------------------------------------------------------
Пример использования этого класса:

string test1 = ConfigSettings.ReadSetting("Test1");
ConfigSettings.WriteSetting("Test1", "This is my new value");
ConfigSettings.RemoveSetting("Test1");

И на последок.Помните.что если вы изменили что-нибудь в конфигурации, то...Во-первых, изменения вступят в силу 
только при следующем запуске программы(видимо конфигурация закэширована).
И во-вторых, изменения производятся в текущем конфигурационном файле программы myapplication.exe.config,
а не в файле App.Config прицепленом к проекту в Visual Studio.
-----------------------------------------------------------------------------------------------------------------*/
