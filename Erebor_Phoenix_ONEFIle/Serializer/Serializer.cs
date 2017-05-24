using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Phoenix.EreborPlugin.Erebor
{
    public static class Serializer
    {
        static string path = Core.Directory + @"\Profiles\XML\";
        public static void SerializeObject(this List<string> list, string fileName)
        {
            try {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var serializer = new XmlSerializer(typeof(List<string>));
                File.Delete(path + fileName);
                using (var stream = File.OpenWrite(path + fileName))
                {
                    serializer.Serialize(stream, list);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool Deserialize(out List<string> list, string fileName)
        {
            list = new List<string>();
            try
            {
                var serializer = new XmlSerializer(typeof(List<string>));
                using (var stream = File.OpenRead(path + fileName))
                {
                    var other = (List<string>)(serializer.Deserialize(stream));
                    list.Clear();
                    list.AddRange(other);
                }
                return true;
            }
            catch(Exception ex)
            {
                UO.PrintError(fileName+" neexistuje");
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}

