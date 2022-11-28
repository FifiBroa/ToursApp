using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6App
{
    internal class Program
    {
        private static DirectoryInfo _rootDirectory;
        private static string[] _specDirectory = new string[] {"Изображения" , "Документы" , "Прочее"};
        private static int _imagesCount = 0 , _documentsCount = 0 , _otherCount = 0 ;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к диску : ");
            string DirectoryPatch = Console.ReadLine();
            _rootDirectory = new DirectoryInfo( DirectoryPatch);
            var driveInfo = new DriveInfo(DirectoryPatch);
            Console.WriteLine($"Информация о диске : {driveInfo.VolumeLabel} , всего {driveInfo.TotalSize / 1024  / 1024} МБ ,"+
                $"свободно{ driveInfo.AvailableFreeSpace / 1024 / 1024} Мб ");
            SearchDirectories(_rootDirectory);
            foreach (var item in _rootDirectory.GetDirectories())
            {
                if (!_specDirectory.Contains(item.Name ))
                {
                    item.Delete(true);
                }
              
            }
            var ResultText = $"Всего обработано {_imagesCount + _documentsCount + _otherCount} файлов. " +
                $"Из них {_imagesCount} изображений , {_documentsCount} документов ,{_otherCount} прочих файлов ";
            Console.WriteLine(ResultText);
            File.WriteAllText(_rootDirectory + "\\Инфо.txt", ResultText);
            Console.ReadLine();
        }
        private static void SearchDirectories(DirectoryInfo currentDirectory)
        {
            if (!_specDirectory.Contains(currentDirectory.Name))
            {
                FilterFiles(currentDirectory);
                foreach (var item in currentDirectory.GetDirectories())
                {
                    SearchDirectories(item);
                }
            }
        } 
        private static void FilterFiles(DirectoryInfo currentDirectory)
        {
            var currentFiles = currentDirectory.GetFiles();
            foreach (var fileifo in currentFiles)
            {
                if (new string[] { ".jpg" , ".jpeg", ".png", ".gif", ".tiff", ".bmp",".svg" }.Contains(fileifo.Extension.ToLower()))
                {
                    var photoDirectory = new DirectoryInfo(_rootDirectory + $"{_specDirectory[0]}\\");
                    if (!photoDirectory.Exists)
                        photoDirectory.Create();


                    var YearDirectory = new DirectoryInfo(photoDirectory + $"{fileifo.LastWriteTime.Date.Year}\\");
                    if (!YearDirectory.Exists)
                        YearDirectory.Create();
                    MoveFile(fileifo, YearDirectory);
                    _imagesCount ++;
                }
                else if (new string[] { ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".ppt", ".pptx" }.Contains(fileifo.Extension.ToLower()))
                {

                    var documentDirectory = new DirectoryInfo(_rootDirectory + $"{_specDirectory[0]}\\");
                    if (!documentDirectory.Exists)
                        documentDirectory.Create();
                    DirectoryInfo lenghtDirectory = null;
                    if (fileifo.Length / 1024 / 1024 < 1)
                        lenghtDirectory = new DirectoryInfo(documentDirectory + "менее 1 мб \\");
                    else if (fileifo.Length / 1024 / 1024 > 10)
                        lenghtDirectory = new DirectoryInfo(documentDirectory + "менее 10 мб \\");
                    else
                        lenghtDirectory = new DirectoryInfo(documentDirectory + " От 1 до 10 мб \\");
                    if (lenghtDirectory.Exists) 
                        lenghtDirectory.Create();
                    MoveFile(fileifo, lenghtDirectory) ;
                    _documentsCount++;
                }
                else
                {
                    var otherDirectory = new DirectoryInfo(_rootDirectory + $"{_specDirectory[2]}\\");
                    if (!otherDirectory.Exists)
                        otherDirectory.Create();
                    MoveFile(fileifo,otherDirectory) ;
                    _otherCount++; 
                }
            }
        }
        private static void MoveFile(FileInfo fileInfo , DirectoryInfo directoryInfo)
        {
            var newFileInfo = new FileInfo(directoryInfo + $"\\{fileInfo.Name}");
            while (newFileInfo.Exists)
                 newFileInfo = new FileInfo(directoryInfo + $"\\{Path.GetFileNameWithoutExtension(fileInfo.Name)} (1)" +
                    $"{newFileInfo.Extension}");
            fileInfo.MoveTo(newFileInfo.FullName);
        }
    }
}
