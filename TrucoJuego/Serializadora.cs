using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Serializadora<T>
    {
        public static bool SerializarJson(T objeto, string pathSerializacion)
        {
            //string path = "media/perfiles/";

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
            using(StreamReader lectorJson = new StreamReader(pathSerializacion))
            {
                string jsonString = lectorJson.ReadToEnd();
                T p = (T)JsonSerializer.Deserialize(jsonString, typeof(T));
                return p;
            }
        }

        public static string DeserializarStr(string ruta)
        {
            using (StreamReader lector = new StreamReader(ruta))
            {
                return lector.ReadToEnd();
            }
        }

        public static void SerializarStr(string aSerializar, string ruta)
        {
            using (StreamWriter escritor = new StreamWriter(ruta, true))
            {
                escritor.WriteLine(aSerializar);
            }   
        }
    }
}
