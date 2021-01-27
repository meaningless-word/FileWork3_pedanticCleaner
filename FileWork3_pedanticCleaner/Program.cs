using System;

namespace FileWork3_pedanticCleaner
{
	/// <summary>
	/// При запуске программа должна:
	/// Показать, сколько весит папка до очистки.Использовать метод из задания 2. 
	/// Выполнить очистку.
	/// Показать сколько файлов удалено и сколько места освобождено.
	/// Показать, сколько папка весит после очистки.
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Необходимо указать путь к рабочей папке.");
			Console.WriteLine("При вводе пустой строки будет исползьоваться папка Testing на рабочем столе");
			Console.Write(": ");
			string workPath = Console.ReadLine();

			Scan doIt = new Scan(workPath, 5);
			doIt.Run();
		}
	}
}
