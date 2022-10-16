using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LOK1game
{
    public class CSVLoader : MonoBehaviour
    {
        private TextAsset _csvFile;
        private readonly char _lineSeperator = '\n';
        private readonly char _surround = '"';
        private readonly string[] _fieldSeperator = { "\",\"" };

        public void LoadCSV()
        {
            _csvFile = Resources.Load<TextAsset>("UTF8/localisation");
        }

        public Dictionary<string, string> GetDictionaryValues(string attributeId)
        {
            var dictionary = new Dictionary<string, string>();

            string[] lines = _csvFile.text.Split(_lineSeperator);

            var attribute_idnex = -1;

            string[] headers = lines[0].Split(_fieldSeperator, System.StringSplitOptions.None);

            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(attributeId))
                {
                    attribute_idnex = i;

                    break;
                }
            }

            var csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                string[] fields = csvParser.Split(line);

                for (int f = 0; f < fields.Length; f++)
                {
                    fields[f] = fields[f].TrimStart(' ', _surround);
                    fields[f] = fields[f].TrimEnd(_surround);
                }

                if (fields.Length > attribute_idnex)
                {
                    var key = fields[0];

                    if (dictionary.ContainsKey(key)) { continue; }

                    var value = fields[attribute_idnex];

                    dictionary.Add(key, value);
                }
            }

            return dictionary;
        }
    }
}