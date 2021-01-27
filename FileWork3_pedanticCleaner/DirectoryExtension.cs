using System;
using System.IO;

namespace FileWork3_pedanticCleaner
{
	class DirectoryExtension
	{
		public static string exceptedFolders = "";
		/// <summary>
		/// рекурсивный подсчёт размера файлов в текущей папке и всех её подкаталогах
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static long DirSize(DirectoryInfo d)
		{
			long size = 0;
			try
			{
				FileInfo[] files = d.GetFiles();
				foreach (FileInfo f in files)
				{
					size += f.Length;
				}

				DirectoryInfo[] dirs = d.GetDirectories();
				foreach (DirectoryInfo dir in dirs)
				{
					size += DirSize(dir);
				}
			}
			catch (Exception e)
			{
				exceptedFolders += d.FullName + "\n";
			}
			return size;
		}

		/// <summary>
		/// очистка файлов и подкаталогов, чьё время последнего обращения превышает указанный интервал 
		/// </summary>
		/// <param name="di">исследуемая папка</param>
		/// <param name="i">срок жизни файла в минутах</param>
		public static (long c, long v) DeepClean(DirectoryInfo di, int i)
		{
			long count = 0; //подсчёт количества удалённых файлов в ходе выполнения очистки
			long volume = 0; //подсчёт освобождаемого дискового пространства в ходе очистки

			//поскольку очистка файлов поменяет время последнего обращения папки на текущее, 
			//необходимо вычислить время жизни каталога до начала операций
			TimeSpan t = DateTime.Now - di.LastAccessTime;

			//сначала исследуются файлы
			FileInfo[] fs = di.GetFiles();
			foreach (FileInfo f in fs)
			{
				try
				{
					long fl = f.Length;
					if (DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(i))
					{
						f.Delete();
						count++;
						volume += fl;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("файл {0} не удаляется по причине: {1}", f.FullName, e.Message);
				}
			}

			DirectoryInfo[] dirs = di.GetDirectories();
			foreach (DirectoryInfo dir in dirs)
			{
				//сначала погружение
				(long c, long v) result = DeepClean(dir, i);
				count += result.c;
				volume += result.v;

				//далее попытка удаления папки без дополнительных проверок - положимся на try..catch
				try
				{
					if (t > TimeSpan.FromMinutes(i))
						dir.Delete();
				}
				catch (Exception e)
				{
					Console.WriteLine("папка {0} не удаляется по причине: {1}", dir.FullName, e.Message);
				}
			}

			return (count, volume);
		}
	}
}
