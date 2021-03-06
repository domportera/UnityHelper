using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DomsUnityHelper
{
    public static class FileHandler
    {
        #region Saving

        /// <summary>
        /// Saves json file of class object to file. Will overwrite an existing file. Uses Unity's JsonUtility.
        /// </summary>
        /// <returns>Returns true if successful</returns>
        public static bool SaveJsonObject<T>(T _save, FileInfo _fileInfo, bool _prettyPrint = true, bool _log = false)
        {
            string json = JsonUtility.ToJson(_save, _prettyPrint);
            return SaveTextFile(json, _fileInfo, _log);
        }

        /// <summary>
        /// Saves json file of class object to specified directory and file. Will overwrite an existing file. Uses Unity's JsonUtility.
        /// </summary>
        /// <returns>Returns true if successful</returns>
        public static bool SaveJsonObject<T>(T _save, string _directory, string _fileName, string _fileExtension = ".json", bool _prettyPrint = true, bool _log = false)
        {
            string json = JsonUtility.ToJson(_save, _prettyPrint);
            return SaveTextFile(json, _directory, _fileName, _fileExtension, _log);
        }

        /// <summary>
        /// Saves text file to specified file. Will overwrite an existing file
        /// </summary>
        /// <param name="_log">Log to console on successful write</param>
        /// <returns>Returns true if successful</returns>
        public static bool SaveTextFile(string _text, FileInfo _file, bool _log = false)
        {
            string directory = _file.DirectoryName;
            string fileName = Path.GetFileNameWithoutExtension(_file.Name);
            string extension = _file.Extension;

            return SaveTextFile(_text, directory, fileName, extension, _log);
        }

        /// <summary>
        /// Saves text file to specified directory and file. Will overwrite an existing file
        /// </summary>
        /// <param name="_log">Log to console on successful write</param>
        /// <returns>Returns true if successful</returns>
        public static bool SaveTextFile(string _text, string _directory, string _fileName, string _fileExtension, bool _log = false)
        {
            if(!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            _fileExtension = PrepareFileExtension(_fileExtension, _log);

            string fullFileName = _fileName.Trim() + _fileExtension;
            string path = Path.Combine(_directory, fullFileName);

            try
            {
                File.WriteAllText(path, _text);
                if(_log)
                {
                    Debug.Log($"Saved {fullFileName} to {path})");
                }
                return true;
            }
            catch(Exception e)
            {
                Debug.LogError($"Error saving file {_fileName} to {path}:\n{e}");
                return false;
            }
        }

        private static string PrepareFileExtension(string _fileExtension, bool _log)
        {
            if(!_fileExtension.StartsWith("."))
            {
                if(_log)
                {
                    Debug.LogWarning($"Adding '.' to start of file extension {_fileExtension}");
                }

                _fileExtension = '.' + _fileExtension;
            }

            if(_fileExtension.Contains(' '))
            {
                Debug.LogError($"File extension should not contain whitespace. Removing.");
                _fileExtension.Replace(" ", "");
            }

            return _fileExtension;
        }
        #endregion Saving

        #region Loading

        /// <summary>
        /// Loads all files in provided directory with provided file extension and attempts to convert them to provided object type
        /// </summary>
        public static List<T> LoadAllJsonObjects<T>(string _directory, string _fileExtension)
        {
            DirectoryInfo info = new DirectoryInfo(_directory);
            List<T> objects = new List<T>();

            if(!info.Exists)
            {
                return objects;
            }

            foreach(FileInfo f in info.GetFiles())
            {
                if(f.Extension == _fileExtension)
                {
                    objects.Add(LoadJsonObject<T>(_directory, f.Name));
                }
            }

            return objects;
        }

        /// <summary>
        /// Loads file in at provided directory with provided file name and extension and attempts to convert it to provided object type
        /// </summary>
        public static T LoadJsonObject<T>(string _directory, string _fileName, string _fileExtension)
        {
            string json = LoadTextFile(_directory, _fileName, _fileExtension);
            return ObjectFromJson<T>(json);
        }

        /// <summary>
        /// Loads file in at provided directory with provided file info and attempts to convert it to provided object type
        /// </summary>
        public static T LoadJsonObject<T>(FileInfo _file)
        {
            if(_file == null)
            {
                Debug.LogWarning($"Attempting to create a json object from null file info");
                return default(T);
            }

            string json = LoadTextFile(_file.DirectoryName, _file.Name);
            return ObjectFromJson<T>(json);
        }

        static T LoadJsonObject<T>(string _directory, string _fullFileName)
        {
            string json = LoadTextFile(_directory, _fullFileName);
            return ObjectFromJson<T>(json);
        }

        /// <summary>
        /// Converts json into an object of provided type
        /// </summary>
        public static T ObjectFromJson<T>(string _json)
        {
            if(string.IsNullOrWhiteSpace(_json))
            {
                Debug.LogWarning($"Attempting to create a json object from empty json text");
                return default(T);
            }

            return JsonUtility.FromJson<T>(_json);
        }
        /// <summary>
        /// Loads provided text file
        /// </summary>
        public static string LoadTextFile(string _directory, string _fileName, string _fileExtension)
        {
            string fullFileName = _fileName + _fileExtension;
            return LoadTextFile(_directory, fullFileName);
        }
        
        /// <summary>
        /// Loads provided text file from file info
        /// </summary>
        public static string LoadTextFile(FileInfo _info)
        {
            if(_info == null)
            {
                Debug.LogWarning($"Attempting to load text file on null FileInfo");
                return string.Empty;
            }
            
            return LoadTextFile(_info.DirectoryName, _info.Name);
        }

        static string LoadTextFile(string _directory, string _fullFileName)
        {
            if(!Directory.Exists(_directory))
            {
                Debug.LogWarning($"Directory not found at {_directory}");
                return string.Empty;
            }

            string path = Path.Combine(_directory, _fullFileName);
            if(!File.Exists(path))
            {
                Debug.LogWarning($"File not found at {path}");
                return string.Empty;
            }

            string text = File.ReadAllText(path);
            return text;
        }

        /// <summary>
        /// Gets file info of all files in provided directory - if no such directory exists, returns null
        /// </summary>
        public static FileInfo[] GetFilesInDirectory(string _directory, string _extension = "", bool _log = false)
        {
            if(Directory.Exists(_directory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_directory);

                if(string.IsNullOrWhiteSpace(_extension))
                {
                    return directoryInfo.GetFiles();
                }
                else
                {
                    string extension = PrepareFileExtension(_extension, _log);
                    return directoryInfo.GetFiles('*' + extension);
                }
            }
            else
            {
                if(_log)
                {
                    Debug.LogWarning($"Directory {_directory} was not found");
                }

                return null;
            }
        }

        /// <summary>
        /// Deletes file at provided location
        /// </summary>
        public static bool DeleteFile(string _filePath)
        {
            try
            {
                File.Delete(_filePath);
                return true;
            }
            catch(Exception e)
            {
                Debug.LogError($"File deletion error: {e}");
                return false;
            }
        }

        /// <summary>
        /// Deletes file at provided location
        /// </summary>
        public static bool DeleteFile(string _directory, string _fileNameSansExtension, string _fileExtension, bool _log = false)
        {
            string extension = PrepareFileExtension(_fileExtension, _log);
            string filePath = Path.Combine(_directory, _fileNameSansExtension + extension);

            return DeleteFile(filePath);
        }

        /// <summary>
        /// Deletes file at provided location
        /// </summary>
        public static bool DeleteFile(FileInfo _file)
        {
            return DeleteFile(_file.FullName);
        }

        #endregion Loading

        #region File Name Validity
        /// <summary>
        /// Returns whether a provided file name contains invalid characters
        /// </summary>
        /// <param name="_name">File name without its directory</param>
        public static bool ContainsInvalidFileNameCharacters(string _name)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            foreach(char c in invalidFileChars)
            {
                if(_name.Contains(c.ToString()))
                {
                    return true;
                }
            }

            if(_name.Contains('/') || _name.Contains('\\'))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns whether a provided file name contains invalid characters, then outputs the invalid characters
        /// </summary>
        /// <param name="_name">File name without its directory</param>
        public static bool ContainsInvalidFileNameCharacters(string _name, out List<char> _invalidCharacters)
        {
            _invalidCharacters = GetInvalidFileNameCharacters(_name);
            if(_invalidCharacters.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns invalid file name characters
        /// </summary>
        /// <param name="_name">File name without its directory</param>
        /// <param name="_additionalInvalidChars"></param>
        /// <returns></returns>
        public static List<char> GetInvalidFileNameCharacters(string _name, char[] _additionalInvalidChars = null)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            List<char> invalidChars = new List<char>();

            foreach(char c in invalidFileChars)
            {
                if(_name.Contains(c))
                {
                    invalidChars.Add(c);
                }
            }

            if(_additionalInvalidChars != null)
            {
                foreach(char c in _additionalInvalidChars)
                {
                    if(_name.Contains(c))
                    {
                        invalidChars.Add(c);
                    }
                }
            }

            return invalidChars;
        }
        #endregion File Name Validity
    }
}