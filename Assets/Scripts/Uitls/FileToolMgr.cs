using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


namespace Uitls
{
    public class FileToolMgr
    {

        public static void WriteBinaryString(BinaryWriter bw, string str)
        {
            bw.Write(Encoding.UTF8.GetByteCount(str));
            bw.Write(Encoding.UTF8.GetBytes(str));
        }

        public static string ReadBinaryString(BinaryReader br)
        {
            int length = br.ReadInt32();
            return Encoding.UTF8.GetString(br.ReadBytes(length));
        }


        public static void WriteToBinaryFile(string binaryFilePath, Action<BinaryWriter> onRecordWrite)
        {
            if (File.Exists(binaryFilePath))
                File.Delete(binaryFilePath);


            using (FileStream fs = new FileStream(binaryFilePath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    onRecordWrite(bw);
                }
            }
        }

        /// <summary>
        /// 写入二进制文件
        /// </summary>
        /// <param name="binaryFilePath"></param>
        /// <param name="content"></param>
        public static void WriteToBinaryFile(string binaryFilePath, string fileName, string content)
        {
            string path = binaryFilePath + fileName;
            if (!Directory.Exists(binaryFilePath))
                Directory.CreateDirectory(binaryFilePath);
            if (File.Exists(path))
                File.Delete(path);
                

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Encoding.UTF8.GetByteCount(content));
                    bw.Write(Encoding.UTF8.GetBytes(content));
                }
            }
        }

        /// <summary>
        /// 读取二进制文件
        /// </summary>
        /// <param name="binaryFilePath"></param>
        /// <returns></returns>
        public static string ReadFromBinaryFile(string binaryFilePath, string fileName)
        {
            string path = binaryFilePath + fileName;
            //if(Directory.Exists(binaryFilePath))
            if (!File.Exists(path))
                return string.Empty;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    return ReadBinaryString(br);
                }
            }
        }


        /// <summary>
        /// 读取json文件
        /// </summary>
        public static string ReadJsonConfig(string filePath)
        {
            string result = string.Empty;
            if (!File.Exists(filePath)) return result;
            //已生成配置文件
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                //获取文件大小
                long size = fs.Length;
                byte[] data = new byte[size];
                //将文件读到byte数组中
                fs.Read(data, 0, data.Length);
                fs.Close();
                //将byte数组转为string
                result = Encoding.UTF8.GetString(data);

                return result;
            }
        }

    }
}