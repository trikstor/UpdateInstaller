using System.IO;
using System.IO.Compression;

namespace UpdateInstaller
{
    public static class ZipWrapping
    {
        public static void Decompress(FileInfo package, DirectoryInfo extractDir)
        {
            ZipFile.ExtractToDirectory(package.FullName, extractDir.FullName);
        }
    }
}
