/* ==============================================================================
* 命名空间：Quick.Common.Systems 
* 类 名 称：Windows
* 创 建 者：Qing
* 创建时间：2019/06/23 19:41:17
* CLR 版本：4.0.30319.42000
* 保存的文件名：Windows
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Quick.Common.Systems
{
    /// <summary>
    /// Windows系统的系列方法
    /// </summary>
    public static class Windows
    {
        /// <summary>
        /// 跨平台调用C++的方法
        /// </summary>
        /// <param name="hwProc">程序句柄</param>
        /// <returns></returns>
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        /// <summary>
        /// 静默清理系统内存
        /// </summary>
        public static void ClearMemorySilent()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            foreach (Process p in Process.GetProcesses())
            {
                using (p)
                {
                    if ((p.ProcessName.Equals("System")) && (p.ProcessName.Equals("Idle")))
                    {
                        //两个系统的关键进程，不整理
                        continue;
                    }

                    try
                    {
                        EmptyWorkingSet(p.Handle);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        /// <summary>  
        /// 运行一个控制台程序并返回其输出参数。  
        /// </summary>  
        /// <param name="filename">程序名</param>  
        /// <param name="arguments">输入参数</param>
        /// <param name="recordLog">是否在控制台输出日志</param>
        /// <returns></returns>  
        public static string RunApp(string filename, string arguments, bool recordLog)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }

                Process proc = new Process
                {
                    StartInfo =
                    {
                        FileName = filename,
                        CreateNoWindow = true,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    }
                };
                proc.Start();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    //上面标记的是原文，下面是我自己调试错误后自行修改的  
                    Thread.Sleep(100); //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行  
                    //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应  
                    if (!proc.HasExited) //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行  
                    {
                        //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行  
                        proc.Kill();
                    }

                    string txt = sr.ReadToEnd();
                    if (recordLog)
                        Trace.WriteLine(txt);
                    return txt;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
