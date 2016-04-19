using System.IO;

namespace Goofy.Component.ControllersAndRoutes
{
    public class FileSystemWriter: IWriter
    {
        public void Write(string message)
        {
            var file = File.Create(Directory.GetCurrentDirectory() + "\\FileSystemWriter.txt");
            using (var sw = new StreamWriter(file))
            {
                sw.Write(message);
                sw.Dispose();
            }
        }
    }
}
