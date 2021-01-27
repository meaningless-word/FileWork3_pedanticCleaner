using System;
using System.IO;
using static System.Environment;

namespace FileWork3_pedanticCleaner
{
	/// <summary>
	/// сканирование папки и удаление устаревших файлов с отображением освобожденного места в ходе оптимизации
	/// </summary>
	class Scan
	{
		private string fullPath;
		public int refreshInterval { get; set; }
		public string path
		{
			get { return fullPath; }
			set
			{
				if (value.Length == 0)
				{
					fullPath = Environment.GetFolderPath(SpecialFolder.Desktop) + "\\Testing";
				}
				else
				{
					fullPath = value;
				}
			}
		}

		public Scan(string path)
		{
			this.path = path;
			refreshInterval = 30;
		}

		public Scan(string path, int interval)
		{
			this.path = path;
			refreshInterval = interval;
		}

		public void Run()
		{
			if (Directory.Exists(path))
			{
				DirectoryInfo d = new DirectoryInfo(fullPath);
				Console.WriteLine("Начальный размер папки: {0,20:### ### ### ### ##0} байт", DirectoryExtension.DirSize(d));
				(long c, long v) result = DirectoryExtension.DeepClean(d, refreshInterval);
				Console.WriteLine("В процессе очистки\nудалено {0,20:### ### ### ### ##0} файлов\nосвобождено {1,20:### ### ### ### ##0} байт"
					, result.c, result.v);
				Console.WriteLine("Итоговый размер папки: {0,20:### ### ### ### ##0} байт", DirectoryExtension.DirSize(d));
			}
			else
			{
				Console.WriteLine("Папка {0} не обнаружена", fullPath);
			}
		}
	}
}
