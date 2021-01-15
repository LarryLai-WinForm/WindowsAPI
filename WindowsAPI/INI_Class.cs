using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WindowsAPI
{
    using static dlls.kernel32;

    /// <summary>
    /// INI檔讀寫操作
    /// </summary>
    public abstract class INI_Class
    {
        /// <summary>
        /// 定義Section結構
        /// </summary>
        [Serializable]
        public class SectionData : Dictionary<string, string> { }

        /// <summary>
        /// 定義INI_Data結構
        /// </summary>
        [Serializable]
        public class Data : Dictionary<string, SectionData> { }

        /// <summary>
        /// 定義檔案大小(載入檔案時須定義之固定參數)
        /// </summary>
        const int nSize = 65535;

        /// <summary>
        /// 完整檔案路徑
        /// </summary>
        protected string FullFilePath
        {
            private set;
            get;
        }
        /// <summary>
        /// 資料分割字元
        /// </summary>
        const char spiltChar = '\0';
        const char spiltChar_Equal = '=';
        readonly char[] spiltCharArray = { spiltChar };
        readonly char[] spiltCharArray_Equal = { spiltChar_Equal };

        /// <summary>
        /// 資料夾路徑
        /// </summary>
        abstract protected string DirectoryPath { get; }
        /// <summary>
        /// 檔案名稱
        /// </summary>
        abstract protected string FileName { get; }
        /// <summary>
        /// 定義預設資料
        /// </summary>
        abstract public Data DataDefault { get; }

        /// <summary>check directory is exist,if not create it</summary>
        /// <param name="path">directory path</param>
        /// <returns>return DirectoryInfo </returns>
        DirectoryInfo CreateDirectoryIfNoExist(string path)
        {
            if (!Directory.Exists(path))
            {
                return Directory.CreateDirectory(path);
            }
            return new DirectoryInfo(path);
        }

        /// <summary>
        /// 建構式
        /// </summary>
        public INI_Class()
        {
            //檢查目錄是否存在,不存在則建立
            CreateDirectoryIfNoExist(DirectoryPath);
            //設定完整檔案路徑
            FullFilePath = DirectoryPath + FileName;

            //載入資料
            Load();
        }


        /// <summary>
        /// 回傳檔案所有SectionName的字串陣列;檔案不存在,檔案中無SectionName或異常發生時回傳空陣列
        /// </summary>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>回傳檔案所有SectionName的字串陣列;
        /// 檔案不存在,檔案中無SectionName或異常發生時回傳空陣列</returns>
        private string[] ReadSectionNames(string lpFileName)
        {
            string[] tmp = new string[0];

            try
            {

                IntPtr lpszReturnBuffer = Marshal.AllocCoTaskMem(nSize);

                //取得資料
                uint returnSize = GetPrivateProfileSectionNames(lpszReturnBuffer, nSize, lpFileName);

                if (returnSize > 0)
                {
                    string local = Marshal.PtrToStringAnsi(lpszReturnBuffer, (int)returnSize).ToString();
                    Marshal.FreeCoTaskMem(lpszReturnBuffer);
                    tmp = local.Substring(0, local.Length - 1).Split(spiltCharArray);
                }
            }
            catch
            {
                //異常回傳空陣列不做任何處理
            }

            return tmp;
        }
        /// <summary>
        /// ReadSectionNames
        /// </summary>
        /// <returns>回傳檔案所有SectionName的字串陣列;檔案不存在,檔案中無SectionName或異常發生時回傳空陣列</returns>
        protected string[] ReadSectionNames()
        {
            return ReadSectionNames(FullFilePath);
        }

        /// <summary>
        /// ReadSection
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>回傳指定Section的資料;
        /// 檔案不存在,Section中無資料或異常發生時回傳空陣列</returns>
        private string ReadSection(string Section, string lpFileName)
        {
            string tmp = string.Empty;

            try
            {
                IntPtr lpszReturnBuffer = Marshal.AllocCoTaskMem(nSize);

                //取得資料
                uint returnSize = GetPrivateProfileSection(Section, lpszReturnBuffer, nSize, lpFileName);

                if (returnSize > 0)
                {
                    string local = Marshal.PtrToStringAnsi(lpszReturnBuffer, (int)returnSize).ToString();
                    Marshal.FreeCoTaskMem(lpszReturnBuffer);
                    //tmp = local.Substring(0, local.Length - 1).Split(spiltChar);
                    tmp = local;
                }
            }
            catch
            {
                //異常回傳空陣列不做任何處理
            }

            return tmp;
        }
        /// <summary>
        /// ReadSection
        /// </summary>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>回傳指定Section的資料;檔案不存在,Section中無資料或異常發生時回傳空陣列</returns>
        protected string ReadSection(string Section)
        {
            return ReadSection(Section, FullFilePath);
        }

        /// <summary>
        /// 寫入指定section資料內容;資料格式為"key1=value1\nkey2=value2\nkey3=value3\n";
        /// lpString傳入""或null可清空或刪除指定section
        /// </summary>
        /// <param name="SectionName">section</param>
        /// <param name="lpString">data傳入""或null可清空或刪除指定section</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>是否寫入成功</returns>
        private bool WriteSection(string SectionName, string lpString, string lpFileName)
        {
            return WritePrivateProfileSection(SectionName, lpString, lpFileName);
        }
        /// <summary>
        /// 清空指定的section
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <param name="filePath">filePath</param>
        /// <returns>回傳是否成功</returns>
        private bool ClearSection(string SectionName, string filePath)
        {
            return WriteSection(SectionName, string.Empty, filePath);
        }
        /// <summary>
        /// 刪除指定的section
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <param name="filePath">filePath</param>
        /// <returns>回傳是否成功</returns>
        private bool DeleteSection(string SectionName, string filePath)
        {
            return WriteSection(SectionName, null, filePath);
        }
        /// <summary>
        /// 寫入指定section完整資料;資料格式為"key1=value1\nkey2=value2\nkey3=value3\n";
        /// lpString傳入""或null可清空或刪除指定section
        /// </summary>
        /// <param name="lpAppName">SectionName</param>
        /// <param name="lpString">data傳入""或null可清空或刪除指定section</param>
        /// <returns>是否寫入成功</returns>
        protected bool WriteSection(string lpAppName, string lpString)
        {
            return WriteSection(lpAppName, lpString, FullFilePath);
        }
        /// <summary>
        /// 清空指定的section
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <returns>回傳是否成功</returns>
        protected bool ClearSection(string SectionName)
        {
            return WriteSection(SectionName, string.Empty);
        }
        /// <summary>
        /// 刪除指定的section
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <returns>回傳是否成功</returns>
        protected bool DeleteSection(string SectionName)
        {
            return WriteSection(SectionName, null);
        }

        /// <summary>
        /// 取得字串資料
        /// </summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpKeyName">key</param>
        /// <param name="lpDefault">預設的回傳值(發生錯誤時回傳此參數)</param>
        /// <param name="lpReturnedString">接收暫存(StringBuilder)</param>
        /// <param name="nSize">接收暫存大小</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的字串長度</returns>
        private static uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName)
        {
            return GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName);
        }
        /// <summary>
        /// 寫入資料到指定的section與key中
        /// </summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpKeyName">key</param>
        /// <param name="lpString">value</param>
        /// <param name="lpFileName">path</param>
        /// <returns>是否寫入成功</returns>
        private static bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {
            return WritePrivateProfileString(lpAppName, lpKeyName, lpString, lpFileName);
        }
        /// <summary>
        /// 讀取指定位置的string,讀取失敗回傳
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <param name="Key">Key</param>
        /// <returns>回傳讀取string</returns>
        protected string ReadValue(string SectionName, string Key)
        {
            StringBuilder sb = new StringBuilder(256);
            uint res = GetPrivateProfileString(SectionName, Key, string.Empty, sb, (uint)sb.Capacity, FullFilePath);
            return sb.ToString();
        }
        /// <summary>
        /// 寫入指定位置的VALUE
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <param name="Key">Key</param>
        /// <param name="Value">Value</param>
        /// <returns>是否寫入成功</returns>
        protected bool WriteValue(string Section, string Key, string Value)
        {
            if (WritePrivateProfileString(Section, Key, Value, FullFilePath))
            {
                //Data[Section][Key] = Value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得數值資料
        /// </summary>
        /// <param name="lpAppName">section</param>
        /// <param name="lpKeyName">key</param>
        /// <param name="nDefault">預設的回傳值(發生錯誤時回傳此參數)</param>
        /// <param name="lpFileName">檔案路徑</param>
        /// <returns>接收的數值資料</returns>
        protected static int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName)
        {
            return GetPrivateProfileInt(lpAppName, lpKeyName, nDefault, lpFileName);
        }

        #region Load
        /// <summary>
        /// 從SectionData物件轉DataString字串
        /// </summary>
        /// <param name="sectionData">SectionData物件</param>
        /// <returns>SectionData物件的DataString字串</returns>
        protected string GetStringFromData(SectionData sectionData)
        {
            string DataString = string.Empty;

            foreach (string Key in sectionData.Keys)
            {
                string Value = sectionData[Key];

                //逐筆取出key,value,並加入section資料字串
                DataString += Key + spiltChar_Equal + Value + spiltChar;
            }

            return DataString;
        }
        /// <summary>
        /// 從DataString字串轉SectionData物件
        /// </summary>
        /// <param name="DataString">DataString字串</param>
        /// <returns>SectionData物件</returns>
        protected SectionData GetDataFromString(string DataString)
        {
            SectionData sectionData = new SectionData();

            string[] data = DataString.Split(spiltCharArray, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in data)
            {
                string[] KeyValue = item.Split(spiltCharArray_Equal, StringSplitOptions.RemoveEmptyEntries);

                if (KeyValue.Length > 1)
                {
                    sectionData.Add(KeyValue[0], KeyValue[1]);
                }
            }

            return sectionData;
        }

        /// <summary>Dictionary.Keys To string[]</summary>
        /// <param name="keys">Dictionary.Keys</param>
        /// <returns>string[]</returns>
        string[] DictKeysToStringArray<T>(Dictionary<string, T>.KeyCollection keys)
        {
            string[] Sections = new string[keys.Count];
            keys.CopyTo(Sections, 0);

            return Sections;
        }

        /// <summary>
        /// 刪除非預期的Section
        /// </summary>
        /// <param name="SectionNames">傳入從檔案取出的所有Section;與預設Section做檢查,若非預設Section則刪除該Section</param>
        /// <returns>回傳是否成功</returns>
        protected bool CheckSections(string[] SectionNames)
        {

            string[] Sections = DictKeysToStringArray(DataDefault.Keys);

            foreach (string SectionName in SectionNames)
            {
                int index = Array.IndexOf(Sections, SectionName);

                if (index < 0)
                {
                    if (!DeleteSection(SectionName))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 讀取檔案到INI_Data
        /// </summary>
        /// <param name="data">INI_Data</param>
        /// <returns>回傳讀取是否成功</returns>
        protected bool INI_DataReadFromFile(ref Data data)
        {
            //直接用DataDefault.Keys跑foreach會有問題先轉string[]

            string[] Sections = DictKeysToStringArray(DataDefault.Keys);

            //搜尋所有Section
            foreach (string Section in Sections)
            {
                //取得Section參照
                SectionData section = DataDefault[Section];

                //取得所有KEY
                string[] keys = new string[section.Keys.Count];
                section.Keys.CopyTo(keys, 0);

                //搜尋所有KEY
                foreach (string key in keys)
                {
                    //假如讀回來錯誤或空值時使用預設值
                    string value = ReadValue(Section, key);
                    if (value.Length == 0)
                    {
                        data[Section][key] = section[key];
                    }
                    else
                    {
                        data[Section][key] = value;
                    }
                }
                //清除整個Section,可清除多餘的Key,再重新寫入整個Section
                ClearSection(Section);
                WriteSection(Section, GetStringFromData(data[Section]));
            }

            return true;
        }
        /// <summary>
        /// 寫入INI_Data到檔案
        /// </summary>
        /// <param name="data">INI_Data</param>
        /// <returns>回傳寫入是否成功</returns>
        protected bool INI_DataWriteToFile(Data data)
        {
            foreach (string Section in data.Keys)
            {
                //取得此Section資料字串
                string DataString = GetStringFromData(data[Section]);

                if (!WriteSection(Section, DataString))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 檔案初始化,讀取檔案資料,不存在時以預設值建立檔案
        /// </summary>
        /// <returns>回傳初始化動作是否成功</returns>
        protected bool Load()
        {
            bool Success = true;

            Data _Data = DataDefault;

            try
            {
                //從檔案中取得所有Section
                string[] SectionNames = ReadSectionNames();

                //清理多餘Section
                Success = CheckSections(SectionNames);

                //檢查檔案是否有資料
                if (SectionNames.Length > 0)
                {
                    //讀檔
                    INI_DataReadFromFile(ref _Data);

                    //清除重寫,以維持順序統一
                    File.Delete(FullFilePath);
                    INI_DataWriteToFile(_Data);

                }
                else
                {
                    //以預設值建立檔案
                    Success = INI_DataWriteToFile(_Data);
                }
            }
            catch
            {
                //異常回傳錯誤
                return false;
            }

            return Success;
        }
        #endregion
    }

    /// <summary>
    /// INI 範例
    /// </summary>
    class EX_INI : INI_Class
    {
        const string directoryPath = "C:\\";
        const string fileName = "ex.ini";

        const string sectionName = "section";
        const string Key_EX = "Key_EX";
        const string defultValue_EX = "defultValue_EX";


        protected override string FileName { get { return fileName; } }
        protected override string DirectoryPath { get { return directoryPath; } }
        override public Data DataDefault
        {
            get
            {
                return new Data()
                {
                    {
                        sectionName,new SectionData()
                        {
                            { Key_EX,defultValue_EX},
                        }
                    }
                };
            }
        }

        public string EX_Value
        {
            set
            {
                WriteValue(sectionName, Key_EX, value.ToString());
            }
            get
            {
                return ReadValue(sectionName, Key_EX);
            }
        }
    }
}
