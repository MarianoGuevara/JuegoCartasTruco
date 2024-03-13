using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class SerializadoraJson<T>
    {
        public static bool SerializarJson(T objeto, string pathSerializacion)
        {
            //string path = "../../../../media/perfiles/";

            try
            {
                JsonSerializerOptions serializadorJson = new JsonSerializerOptions(); // transforma algo a json
                serializadorJson.WriteIndented = true; // DA FORMATO JSON

                string objJson = JsonSerializer.Serialize(objeto, serializadorJson);

                using (StreamWriter escritorJson = new StreamWriter(pathSerializacion))
                {
                    escritorJson.WriteLine(objJson);
                }
                return true;
            }
            catch { return false; }
        }
        public static T DeserializarJson(string pathSerializacion)
        {
            StreamReader lectorJson = new StreamReader(pathSerializacion);
            string jsonString = lectorJson.ReadToEnd();
            T p = (T)JsonSerializer.Deserialize(jsonString, typeof(T));
            lectorJson.Close();
            return p;
        }
    }
}
